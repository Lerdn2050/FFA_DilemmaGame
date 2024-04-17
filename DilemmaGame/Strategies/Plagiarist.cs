using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Plagiarist
    {
        public static string Description()
        {
            return "первый ход сотрудничество, дальше повторение предыдущего хода опонента текущего опонента ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    return GameInfo.Rounds[i].Player2_Choice;
                }
                else if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                {
                    return GameInfo.Rounds[i].Player1_Choice;
                }
            }
            return true;
        }
    }
}
