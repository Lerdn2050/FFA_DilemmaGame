using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Karma
    {
        public static string Description()
        {
            return "Предаёт тех, кто часто предаёт  ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            int Coops = 0;
            int Betrays = 0;

            Game.GameInfo MyGI = new Game.GameInfo(GameInfo.QuantityPlayers, GameInfo.inf);
            for (int i = GameInfo.Rounds.Length - 1; i >= 0; i--)
            {
                if (GameInfo.Rounds[i].Player1 == OpponentNumber)
                {
                    Game.GameInfo.Round round = new Game.GameInfo.Round(GameInfo.Rounds[i].Player1, GameInfo.Rounds[i].Player2, GameInfo.Rounds[i].Player1_Choice, GameInfo.Rounds[i].Player2_Choice);
                    MyGI += round;
                }
                else if (GameInfo.Rounds[i].Player2 == OpponentNumber)
                {
                    Game.GameInfo.Round round = new Game.GameInfo.Round(GameInfo.Rounds[i].Player2, GameInfo.Rounds[i].Player1, GameInfo.Rounds[i].Player2_Choice, GameInfo.Rounds[i].Player1_Choice);
                    MyGI += round;
                }
            }
            for (int i = MyGI.Rounds.Length - 1; i >= 0; i--)
            {
                if (MyGI.Rounds[i].Player1_Choice)
                {
                    Coops++;
                }
                else
                {
                    Betrays++;
                }
            }

            return Betrays <= Coops;
        }
    }
}
