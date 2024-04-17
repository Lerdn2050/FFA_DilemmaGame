using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Educating
    {
        public static string Description()
        {
            return "первый ход сотрудничество, предаёт если оппонент отвечал сотрудничеством на предательство или предательством на сотрудничество в прошлом ходу  ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            bool HaveChoice = false;
            bool Choice = true;
            int LastOpponentNumber = -1;
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == OpponentNumber && (LastOpponentNumber == -1 || LastOpponentNumber == GameInfo.Rounds[i].Player2))
                {
                    if (HaveChoice)
                    {
                        return Choice ? GameInfo.Rounds[i].Player2_Choice : !GameInfo.Rounds[i].Player2_Choice;
                    }
                    else
                    {
                        LastOpponentNumber = GameInfo.Rounds[i].Player2;
                        HaveChoice = true;
                        Choice = GameInfo.Rounds[i].Player1_Choice;
                    }
                }
                else if (GameInfo.Rounds[i].Player2 == OpponentNumber && (LastOpponentNumber == -1 || LastOpponentNumber == GameInfo.Rounds[i].Player1))
                {
                    if (HaveChoice)
                    {
                        return Choice ? GameInfo.Rounds[i].Player1_Choice : !GameInfo.Rounds[i].Player1_Choice;
                    }
                    else
                    {
                        LastOpponentNumber = GameInfo.Rounds[i].Player1;
                        HaveChoice = true;
                        Choice = GameInfo.Rounds[i].Player2_Choice;
                    }
                }
            }
            return Choice;
        }
    }
}
