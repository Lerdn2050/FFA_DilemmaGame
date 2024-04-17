using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Fair_Vindictive
    {
        public static string Description()
        {
            return "первый ход сотрудничество, ищет последний ход где оппонент ответил на сотрудничество и повторяет его ход ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    if(GameInfo.Rounds[i].Player2_Choice != false)
                        return GameInfo.Rounds[i].Player1_Choice;
                }
                else if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                {
                    if (GameInfo.Rounds[i].Player1_Choice != false)
                        return GameInfo.Rounds[i].Player2_Choice;
                }
            }
            return true;
        }
    }
}
