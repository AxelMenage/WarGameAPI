using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarGameAPI.Enum
{
    public enum GameStateEnum
    {
        WaitingAcceptance = 1,
        WaitingPlacements,
        InProgress,
        Finished,
        Suspended
    }
}
