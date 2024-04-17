using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Liberal
    {
        public static string Description()
        {
            return "Предаёт слишком непредвзятых  ";
        }

        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            double deviation = 0;
            int[] coops = new int[GameInfo.QuantityPlayers];
            int[] betrays = new int[GameInfo.QuantityPlayers];
            double[] ratio = new double[GameInfo.QuantityPlayers];
            double arithmeticMean = 0;
            
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
                    coops[MyGI.Rounds[i].Player2]++;
                    if (betrays[MyGI.Rounds[i].Player2] > 0)
                    {
                        ratio[MyGI.Rounds[i].Player2] = coops[MyGI.Rounds[i].Player2] / betrays[MyGI.Rounds[i].Player2];
                    }
                }
                else
                {
                    betrays[MyGI.Rounds[i].Player2]++;
                    if (coops[MyGI.Rounds[i].Player2] > 0)
                    {
                        ratio[MyGI.Rounds[i].Player2] = coops[MyGI.Rounds[i].Player2] / betrays[MyGI.Rounds[i].Player2];
                    }
                }
            }
            for (int i = 0; i < GameInfo.QuantityPlayers; i++)
            {
                arithmeticMean += ratio[i];
            }
            arithmeticMean /= GameInfo.QuantityPlayers;
            for (int i = 0; i < GameInfo.QuantityPlayers; i++)
            {
                deviation += (arithmeticMean - ratio[i]) * (arithmeticMean - ratio[i]);
            }
            deviation /= GameInfo.QuantityPlayers;
            return deviation > 20;
        }
    }
}
