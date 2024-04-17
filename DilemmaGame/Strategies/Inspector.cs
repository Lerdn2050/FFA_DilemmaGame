using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Inspector
    {
        public static string Description()
        {
            return "выполняет 4 стратегии по 5 * количество противников ходов, всегда сотрудничество, предательство, затем для каждого то что давало больше и наоборот, далее придерживается для каждого самого выгодного выбора  ";
        }
        public static bool record(ref int quantity, ref int points, ref decimal impact, int round, bool first, bool second, Game.GameInfo GameInfo)
        {

            quantity++;
            if (first)
            {
                if (second)
                {
                    points += GameInfo.inf.CoopCoop * 6;
                }
                else
                {
                    points += (GameInfo.inf.CoopBetray + 3) * 6;
                }
            }
            else
            {
                if (second)
                {
                    points += GameInfo.inf.BetrayCoop * 4 + 1;
                }
                else
                {
                    points += GameInfo.inf.BetrayBetray * 3;
                }
            }
            impact = points / quantity;
            return round > 0 && round % 5 == 0;
        }
        public static void nextStady(ref bool[] choices, decimal[] impacts1, decimal[] impacts2)
        {
            if (impacts1 == null)
            {
                for (int i = 0; i < choices.Length; i++)
                {
                    choices[i] = !choices[i];
                }
            }
            else
            {
                for (int i = 0; i < choices.Length; i++)
                {
                    choices[i] = impacts2[i] > impacts1[i] ? choices[i] : !choices[i];
                }
            }
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {            
            int myRound = 0;
            //1 стадия
            int [] Coops = new int[GameInfo.QuantityPlayers];
            int[] CoopsPoints = new int[GameInfo.QuantityPlayers];
            decimal[] CoopsImpact = new decimal[GameInfo.QuantityPlayers];

            //2 стадия
            int[] Betrays = new int[GameInfo.QuantityPlayers];
            int[] BetraysPoints = new int[GameInfo.QuantityPlayers];
            decimal[] BetraysImpact = new decimal[GameInfo.QuantityPlayers];

            //3 стадия
            int[] Stady3 = new int[GameInfo.QuantityPlayers];
            int[] Stady3points = new int[GameInfo.QuantityPlayers];
            decimal[] Stady3impact = new decimal[GameInfo.QuantityPlayers];
            //4 стадия
            int[] Stady4 = new int[GameInfo.QuantityPlayers];
            int[] Stady4points = new int[GameInfo.QuantityPlayers];
            decimal[] Stady4impact = new decimal[GameInfo.QuantityPlayers];
            bool[] Choices = new bool[GameInfo.QuantityPlayers];
            
            
            for (int i = 0; i < GameInfo.Rounds.Length && myRound <= GameInfo.QuantityPlayers * 20; i++)
            {
                if (GameInfo.Rounds[i].Player1 == MyNumber)
                {
                    myRound++;
                    if (myRound >= GameInfo.QuantityPlayers * 15)
                    {
                        if (record(ref Coops[GameInfo.Rounds[i].Player2], ref CoopsPoints[GameInfo.Rounds[i].Player2], ref CoopsImpact[GameInfo.Rounds[i].Player2], myRound, GameInfo.Rounds[i].Player1_Choice, GameInfo.Rounds[i].Player2_Choice, GameInfo))
                        {
                            nextStady(ref Choices, null, null);
                        }
                    }
                    else if (myRound >= GameInfo.QuantityPlayers * 10)
                    {
                        if (record(ref Betrays[GameInfo.Rounds[i].Player2], ref BetraysPoints[GameInfo.Rounds[i].Player2], ref BetraysImpact[GameInfo.Rounds[i].Player2], myRound, GameInfo.Rounds[i].Player1_Choice, GameInfo.Rounds[i].Player2_Choice, GameInfo))
                        {
                            nextStady(ref Choices, CoopsImpact, BetraysImpact);
                        }
                    }
                    else if (myRound >= GameInfo.QuantityPlayers * 5)
                    {
                        if (record(ref Stady3[GameInfo.Rounds[i].Player2], ref Stady3points[GameInfo.Rounds[i].Player2], ref Stady3impact[GameInfo.Rounds[i].Player2], myRound, GameInfo.Rounds[i].Player1_Choice, GameInfo.Rounds[i].Player2_Choice, GameInfo))
                        {
                            nextStady(ref Choices, null, null);
                        }
                    }
                    else
                    {
                        if (record(ref Stady4[GameInfo.Rounds[i].Player2], ref Stady4points[GameInfo.Rounds[i].Player2], ref Stady4impact[GameInfo.Rounds[i].Player2], myRound, GameInfo.Rounds[i].Player1_Choice, GameInfo.Rounds[i].Player2_Choice, GameInfo))
                        {
                            nextStady(ref Choices, Stady3impact, Stady4impact);
                        }
                    }

                }
                else if (GameInfo.Rounds[i].Player2 == MyNumber)
                {
                    myRound++;
                    if (myRound >= GameInfo.QuantityPlayers * 15)
                    {
                        if (record(ref Coops[GameInfo.Rounds[i].Player1], ref CoopsPoints[GameInfo.Rounds[i].Player1], ref CoopsImpact[GameInfo.Rounds[i].Player1], myRound, GameInfo.Rounds[i].Player2_Choice, GameInfo.Rounds[i].Player1_Choice, GameInfo))
                        {
                            nextStady(ref Choices, null, null);
                        }
                    }
                    else if (myRound >= GameInfo.QuantityPlayers * 10)
                    {
                        if (record(ref Betrays[GameInfo.Rounds[i].Player1], ref BetraysPoints[GameInfo.Rounds[i].Player1], ref BetraysImpact[GameInfo.Rounds[i].Player1], myRound, GameInfo.Rounds[i].Player2_Choice, GameInfo.Rounds[i].Player1_Choice, GameInfo))
                        {
                            nextStady(ref Choices, CoopsImpact, BetraysImpact);
                        }
                    }
                    else if (myRound >= GameInfo.QuantityPlayers * 5)
                    {
                        if (record(ref Stady3[GameInfo.Rounds[i].Player1], ref Stady3points[GameInfo.Rounds[i].Player1], ref Stady3impact[GameInfo.Rounds[i].Player1], myRound, GameInfo.Rounds[i].Player2_Choice, GameInfo.Rounds[i].Player1_Choice, GameInfo))
                        {
                            nextStady(ref Choices, null, null);
                        }
                    }
                    else
                    {
                        if (record(ref Stady4[GameInfo.Rounds[i].Player1], ref Stady4points[GameInfo.Rounds[i].Player1], ref Stady4impact[GameInfo.Rounds[i].Player1], myRound, GameInfo.Rounds[i].Player2_Choice, GameInfo.Rounds[i].Player1_Choice, GameInfo))
                        {
                            nextStady(ref Choices, Stady3impact, Stady4impact);
                        }
                    }
                }
            }
            return Choices[OpponentNumber];
        }
    }
}
