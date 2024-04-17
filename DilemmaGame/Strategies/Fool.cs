using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Fool
    {
        public static string Description()
        {
            return "Вычисляет среднее значение получаемое от данного оппонента при сотрудничестве и предательстве и выбирает то, которое приносит в среднем меньше  ";
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            int Coops = 0;
            int CoopsPoints = 0;
            decimal CoopsImpact = 0;
            int Betrays = 0;
            int BetraysPoints = 0;
            decimal BetraysImpact = 0;

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
                if (MyGI.Rounds[i].Player2_Choice)
                {
                    Coops++;
                    if (MyGI.Rounds[i].Player1_Choice)
                    {
                        CoopsPoints += MyGI.inf.CoopCoop;
                    }
                    else
                    {
                        CoopsPoints += MyGI.inf.CoopBetray;
                    }
                }
                else
                {
                    Betrays++;
                    if (MyGI.Rounds[i].Player1_Choice)
                    {
                        BetraysPoints += MyGI.inf.BetrayCoop;
                    }
                    else
                    {
                        BetraysPoints += MyGI.inf.BetrayBetray;
                    }
                }
            }
            if (CoopsPoints > 0 && BetraysPoints > 0)
            {
                CoopsImpact = CoopsPoints / Coops;
                BetraysImpact = BetraysPoints / Betrays;
            }

            return BetraysImpact > CoopsImpact;
        }
    }
}
