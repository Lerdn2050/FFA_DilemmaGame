using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Nazi
    {
        public static string Description()
        {
            return "сотрудничает только с частью игроков игроков ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            return OpponentNumber % 3 != (GameInfo.QuantityPlayers + 1) % 3;
        }
    }
}
