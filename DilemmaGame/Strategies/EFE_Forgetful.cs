using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DilemmaGame.Strategies
{
    class EFE_Forgetful
    {
        public static string Description()
        {
            return "первый ход сотрудничество, дальше повторение предыдущего хода опонента против себя, с низким шансом не повторяет предательство";
        }
        
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                
                if (GameInfo.Rounds[i].Player1 == MyNumber)
                {
                    if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                    {
                        if (GameInfo.Rounds[i].Player2_Choice)
                        {
                            return true;
                        }
                        else if (Game.j.Next(10) == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                }
                else if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    if (GameInfo.Rounds[i].Player2 == MyNumber)
                    {
                        if (GameInfo.Rounds[i].Player1_Choice)
                        {
                            return true;
                        }
                        else if (Game.j.Next(10) == 1)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
