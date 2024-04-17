using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class EyeForEye
    {
        public static string Description()
        {
            return "первый ход сотрудничество, дальше повторение предыдущего хода опонента против себя ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == MyNumber)
                {
                    if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                    {
                        return GameInfo.Rounds[i].Player2_Choice;
                    }
                }
                else if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    if (GameInfo.Rounds[i].Player2 == MyNumber)
                    {
                        return GameInfo.Rounds[i].Player1_Choice;
                    }
                }
            }
            return true;
        }
    }
}
