using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Unpredictable
    {
        public static string Description()
        {
            return "всегда рандом ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            return Game.j.Next(2) == 1;
        }
    }
}
