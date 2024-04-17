using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Fair
    {
        public static string Description()
        {
            return "первый ход сотрудничество, предаёт только если оппонент в прошлом своём ходу ответил предательством на сотрудничество ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    if (GameInfo.Rounds[i].Player2_Choice != false)
                        return GameInfo.Rounds[i].Player1_Choice;
                    else
                        return true;
                }
                else if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                {
                    if (GameInfo.Rounds[i].Player1_Choice != false)
                        return GameInfo.Rounds[i].Player2_Choice;
                    else
                        return true;
                }
            }
            return true;
        }
    }
}
