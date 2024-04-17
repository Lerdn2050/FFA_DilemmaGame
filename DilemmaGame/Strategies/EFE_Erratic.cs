using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class EFE_Erratic
    {
        public static string Description()
        {
            return "первый ход сотрудничество, дальше повторение предыдущего хода опонента против себя, с большим шансом не повторяет ход опонента";
        }
        
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == MyNumber)
                {
                    if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                    {
                        if (Game.j.Next(10) < 3)
                        {
                            return !GameInfo.Rounds[i].Player2_Choice;
                        }
                        return GameInfo.Rounds[i].Player2_Choice;
                    }
                }
                else if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    if (GameInfo.Rounds[i].Player2 == MyNumber)
                    {
                        if (Game.j.Next(10) < 3)
                        {
                            return !GameInfo.Rounds[i].Player1_Choice;
                        }
                        return GameInfo.Rounds[i].Player1_Choice;
                    }
                }
            }
            if (Game.j.Next(10) < 3)
            {
                return false;
            }
            return true;
        }
    }
}
