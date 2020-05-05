using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WarGameAPI.Entities.Views;

namespace WarGameAPI.Entities
{
    public partial class wargameContext : DbContext
    {
        public wargameContext(DbContextOptions<wargameContext> options)
            : base(options)
        {
        }

        //Tables
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<DeckCards> DeckCards { get; set; }
        public virtual DbSet<Deck> Deck { get; set; }
        public virtual DbSet<Friends> Friends { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameState> GameState{ get; set; }
        public virtual DbSet<GameType> GameType { get; set; }
        public virtual DbSet<IngameDeckCards> IngameDeckCards { get; set; }
        public virtual DbSet<IngameDeck> IngameDeck { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Ship> Ship { get; set; }
        public virtual DbSet<ShipState> ShipState { get; set; }
        public virtual DbSet<Shot> Shot { get; set; }
        public virtual DbSet<User> User { get; set; }

        //Views
        public virtual DbSet<UserStatsView> UserStatsView { get; set; }
        public virtual DbSet<GamesView> GamesView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=wargame;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__CARD_CA__705A85D4B4FA7E60");

                entity.ToTable("CARD_CA");

                entity.Property(e => e.Id).HasColumnName("CA_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("CA_DESCRIPTION")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("CA_NAME")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DeckCards>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DECK_CARDS_DC");

                entity.HasIndex(e => new { e.DeckId, e.CardId })
                    .HasName("AK_DE_CA_ID")
                    .IsUnique();

                entity.Property(e => e.CardId).HasColumnName("CA_ID");

                entity.Property(e => e.Nb).HasColumnName("DC_NB");

                entity.Property(e => e.DeckId).HasColumnName("DE_ID");

                entity.HasOne(d => d.Card)
                    .WithMany()
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_DECK_CARDS_CARD");

                entity.HasOne(d => d.Deck)
                    .WithMany()
                    .HasForeignKey(d => d.DeckId)
                    .HasConstraintName("FK_DECK_CARDS_DECK");
            });

            modelBuilder.Entity<Deck>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__DECK_DE__F2DD4687C969DF7C");

                entity.ToTable("DECK_DE");

                entity.Property(e => e.Id).HasColumnName("DE_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("DE_NAME")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("US_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Decks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_DECK_USER");
            });

            modelBuilder.Entity<Friends>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FRIENDS_FR");

                entity.HasIndex(e => new { e.UserId1, e.UserId2 })
                    .HasName("AK_US_ID")
                    .IsUnique();

                entity.Property(e => e.UserId1).HasColumnName("US_ID1");

                entity.Property(e => e.UserId2).HasColumnName("US_ID2");

                entity.HasOne(d => d.User1)
                    .WithMany()
                    .HasForeignKey(d => d.UserId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FRIENDS_USER1");

                entity.HasOne(d => d.User2)
                    .WithMany()
                    .HasForeignKey(d => d.UserId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FRIENDS_USER2");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__GAME_GA__97A04DAAAEA28F44");

                entity.ToTable("GAME_GA");

                entity.Property(e => e.Id).HasColumnName("GA_ID");

                entity.Property(e => e.Enddate)
                    .HasColumnName("GA_ENDDATE")
                    .HasColumnType("datetime");

                entity.Property(e => e.NbTurn).HasColumnName("GA_NB_TURN");

                entity.Property(e => e.PosPlayer1Ok).HasColumnName("GA_POS_PLAYER1_OK");

                entity.Property(e => e.PosPlayer2Ok).HasColumnName("GA_POS_PLAYER2_OK");

                entity.Property(e => e.Startdate)
                    .HasColumnName("GA_STARTDATE")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserIdWinner).HasColumnName("GA_WINNER");

                entity.Property(e => e.GameStateId).HasColumnName("GS_ID");

                entity.Property(e => e.GameTypeId).HasColumnName("GT_ID");

                entity.Property(e => e.IngameDeckId).HasColumnName("IND_ID");

                entity.Property(e => e.Player1Id).HasColumnName("US_ID_PLAYER1");

                entity.Property(e => e.Player2Id).HasColumnName("US_ID_PLAYER2");

                entity.Property(e => e.UserIdTurn).HasColumnName("US_ID_PLAYER_TURN");

                entity.HasOne(d => d.GameState)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.GameStateId)
                    .HasConstraintName("FK_GAME_GAME_STATE");

                entity.HasOne(d => d.IngameDeck)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.IngameDeckId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_GAME_INGAME_DECK");
            });

            modelBuilder.Entity<GameState>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__GAME_STA__0D287C356846493E");

                entity.ToTable("GAME_STATE_GS");

                entity.Property(e => e.Id).HasColumnName("GS_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("GS_NAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GameType>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__GAME_TYP__094CC00ACFA8684E");

                entity.ToTable("GAME_TYPE_GT");

                entity.HasIndex(e => e.Name)
                    .HasName("AK_GT_NAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("GT_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("GT_NAME")
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IngameDeckCards>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__INGAME_D__EA2668469C2A46C1");

                entity.ToTable("INGAME_DECK_CARDS_INC");

                entity.Property(e => e.Id).HasColumnName("INC_ID");

                entity.Property(e => e.CardId).HasColumnName("CA_ID");

                entity.Property(e => e.IngameDeckId).HasColumnName("IND_ID");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.IngameDeckCards)
                    .HasForeignKey(d => d.CardId)
                    .HasConstraintName("FK_INGAME_DECK_CARDS_CARD");

                entity.HasOne(d => d.IngameDeck)
                    .WithMany(p => p.IngameDeckCards)
                    .HasForeignKey(d => d.IngameDeckId)
                    .HasConstraintName("FK_INGAME_DECK_GAME");
            });

            modelBuilder.Entity<IngameDeck>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__INGAME_D__10BE4BB136D09BDC");

                entity.ToTable("INGAME_DECK_IND");

                entity.Property(e => e.Id).HasColumnName("IND_ID");

                entity.Property(e => e.NbCards).HasColumnName("IND_NB_CARDS");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__MESSAGE___FCC95DEB8CECDACE");

                entity.ToTable("MESSAGE_ME");

                entity.Property(e => e.Id).HasColumnName("ME_ID");

                entity.Property(e => e.GameId).HasColumnName("GA_ID");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("ME_CONTENT")
                    .HasColumnType("text");

                entity.Property(e => e.UserIdSender).HasColumnName("US_ID_SENDER");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_MESSAGE_GAME");

                entity.HasOne(d => d.UserSender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserIdSender)
                    .HasConstraintName("FK_MESSAGE_USER1");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__POSITION__5ECDB69DAF742C7D");

                entity.ToTable("POSITION_PO");

                entity.Property(e => e.Id).HasColumnName("PO_ID");

                entity.Property(e => e.GameId).HasColumnName("GA_ID");

                entity.Property(e => e.CoordX).HasColumnName("PO_COORD_X");

                entity.Property(e => e.CoordY).HasColumnName("PO_COORD_Y");

                entity.Property(e => e.Touche).HasColumnName("PO_TOUCHE");

                entity.Property(e => e.ShipId).HasColumnName("SHI_ID");

                entity.Property(e => e.UserId).HasColumnName("US_ID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_POSITION_GAME");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.ShipId)
                    .HasConstraintName("FK_POSITION_SHIP");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_POSITION_USER");
            });

            modelBuilder.Entity<Ship>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__SHIP_SHI__326BBA32CC840499");

                entity.ToTable("SHIP_SHI");

                entity.Property(e => e.Id).HasColumnName("SHI_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("SHI_NAME")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SizeX).HasColumnName("SHI_SIZE_X");

                entity.Property(e => e.SizeY).HasColumnName("SHI_SIZE_Y");
            });

            modelBuilder.Entity<ShipState>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__SHIP_STA__456F9462091FC64B");

                entity.ToTable("SHIP_STATE_SS");

                entity.Property(e => e.Id).HasColumnName("SS_ID");

                entity.Property(e => e.GameId).HasColumnName("GA_ID");

                entity.Property(e => e.ShipId).HasColumnName("SHI_ID");

                entity.Property(e => e.Life).HasColumnName("SS_LIFE");

                entity.Property(e => e.UserId).HasColumnName("US_ID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.ShipStates)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_SHIP_STATE_GAME");

                entity.HasOne(d => d.Ship)
                    .WithMany(p => p.ShipStates)
                    .HasForeignKey(d => d.ShipId)
                    .HasConstraintName("FK_SHIP_STATE_SHIP");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShipStates)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SHIP_STATE_USER");
            });

            modelBuilder.Entity<Shot>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__SHOT_SH__08A44726EDF00363");

                entity.ToTable("SHOT_SH");

                entity.Property(e => e.Id).HasColumnName("SH_ID");

                entity.Property(e => e.GameId).HasColumnName("GA_ID");

                entity.Property(e => e.CoordX).HasColumnName("SH_COORD_X");

                entity.Property(e => e.CoordY).HasColumnName("SH_COORD_Y");

                entity.Property(e => e.UserId).HasColumnName("US_ID");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Shots)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_SHOT_GAME");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shots)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_SHOT_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__USER_US__DE473D81594312AF");

                entity.ToTable("USER_US");

                entity.HasIndex(e => e.Email)
                    .HasName("AK_US_EMAIL")
                    .IsUnique();

                entity.HasIndex(e => e.Nickname)
                    .HasName("AK_US_NICKNAME")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("US_ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("US_EMAIL")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("US_NICKNAME")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasColumnName("US_PASSWORD")
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Points).HasColumnName("US_POINTS");
            });

            modelBuilder.Entity<UserStatsView>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("USER_STATS");

                entity.Property(e => e.Id).HasColumnName("US_ID");

                entity.Property(e => e.Nickname).HasColumnName("US_NICKNAME");

                entity.Property(e => e.Victories).HasColumnName("VICTORIES");

                entity.Property(e => e.Defeats).HasColumnName("DEFEATS");
            });

            modelBuilder.Entity<GamesView>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("GAMES");

                entity.Property(e => e.Id).HasColumnName("GA_ID");

                entity.Property(e => e.StateId).HasColumnName("GS_ID");

                entity.Property(e => e.StateName).HasColumnName("GS_NAME");

                entity.Property(e => e.Player1Id).HasColumnName("US_ID_PLAYER1");

                entity.Property(e => e.Player1Nickname).HasColumnName("US_NICKNAME1");

                entity.Property(e => e.Player2Id).HasColumnName("US_ID_PLAYER2");

                entity.Property(e => e.Player2Nickname).HasColumnName("US_NICKNAME2");

                entity.Property(e => e.PosPlayer1Ok).HasColumnName("GA_POS_PLAYER1_OK");

                entity.Property(e => e.PosPlayer2Ok).HasColumnName("GA_POS_PLAYER2_OK");

                entity.Property(e => e.PlayerTurnId).HasColumnName("US_ID_PLAYER_TURN");

                entity.Property(e => e.PlayerTurnNickname).HasColumnName("US_NICKNAME3");

                entity.Property(e => e.WinnerId).HasColumnName("GA_WINNER");

                entity.Property(e => e.WinnerNickname).HasColumnName("US_NICKNAME4");

                entity.Property(e => e.nbTurn).HasColumnName("GA_NB_TURN");

                entity.Property(e => e.ingameDeckId).HasColumnName("IND_ID");

                entity.Property(e => e.startDate).HasColumnName("GA_STARTDATE");

                entity.Property(e => e.endDate).HasColumnName("GA_ENDDATE");


            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
