using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WarGameAPI.Services
{
    public static class GenericService
    {
        /// <summary>
        /// Encrypter un texte en utilisant SHA-256.
        /// </summary>
        /// <param name="text">Le texte en clair.</param>
        /// <returns>Le texte crypté.</returns>
        public static string EncryptText(string texte, string hashAlgorythm)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(texte);
            byte[] inArray = HashAlgorithm.Create(hashAlgorythm).ComputeHash(bytes);
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        /// Retourner le Guid de l'utilisateur courant.
        /// </summary>
        /// <param name="claimPrincipal">L'identité.</param>
        /// <returns>Retourne le Guid.</returns>
        public static Guid GetGuid(IIdentity claimPrincipal)
        {
            ClaimsIdentity claimsIdentity = claimPrincipal as ClaimsIdentity;
            Guid userId = Guid.Parse(claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);

            return userId;
        }

        /// <summary>
        /// Trace in file system.
        /// </summary>
        /// <param name="text">The Text.</param>
        /// <param name="logCategory">The category of log.</param>
        /// <returns>Task</returns>
        public async static Task Trace(string text)
        {
            var buffer = Encoding.UTF8.GetBytes($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - {text}{Environment.NewLine}");

            using (var fs = new FileStream(@"C:/Users/axelm/Documents/DEV/WarGameAPI/WarGameAPI/Logs", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, buffer.Length, true))
            {
                await fs.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        /// <summary>
        /// Remove special chars.
        /// </summary>
        /// <param name="text">The text to replace.</param>
        /// <returns>Text without special chars.</returns>
        public static string Sanitized(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            // 1. Remove diactrics.
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var result = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    result.Append(c);
                }
            }

            text = result.ToString().Normalize(NormalizationForm.FormC);

            // 2. Remove special chars.
            HashSet<char> removeChars = new HashSet<char>(" ?&^$#@!()+-,:;<>’\'-_* ");
            result = new StringBuilder();
            foreach (char c in text)
            {
                if (!removeChars.Contains(c))
                {
                    result.Append(c);
                }
            }

            return result.ToString().ToLower();
        }
    }
}
