using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DilemmaGame.Strategies
{
    class Adaptive
    {
        public static string Description()
        {
            return "имеет на каждого оппонента заготовленные реакции, каждые 2 * количество игроков ходов немного меняет стратегию, учитывает свой импакт на общее количество ходов  ";
        }
        public static void addPoints(ref int points, bool first, bool second, Game.GameInfo GameInfo)
        {
            if (first)
            {
                if (second)
                {
                    points += GameInfo.inf.CoopCoop * 10;
                }
                else
                {
                    points += (GameInfo.inf.CoopBetray + 1) * 10;
                }
            }
            else
            {
                if (second)
                {
                    points += GameInfo.inf.BetrayCoop * 10000;
                }
                else
                {
                    points += GameInfo.inf.BetrayBetray * 10;
                }
            }
        }
        public static bool Strategy(Game.GameInfo GameInfo, int MyNumber, int OpponentNumber)
        {
            int myRound = 0;
            int stady = 0;
            int start = 0;
            int points = 0;
            double bestImpact = 0; //количество очков на общее количество раундов
            double impact = 0;
            bool[] bestChoices = new bool[GameInfo.QuantityPlayers];
            bool[] Choices = new bool[GameInfo.QuantityPlayers];
            
            
            for (int i = 0; i < GameInfo.Rounds.Length; i++)
            {
                goto skip;
            nextStady:;
                impact = points / ((i - 1) - start);

                if (bestImpact < impact)
                {
                    bestImpact = impact;
                    for (int k = 0; k < Choices.Length; k++)
                    {
                        bestChoices[k] = Choices[k];
                    }
                }
                else
                {
                    for (int k = 0; k < Choices.Length; k++)
                    {
                        Choices[k] = bestChoices[k];
                    }
                }
                if (stady % Choices.Length == MyNumber)
                {
                    stady++;
                }
                Choices[stady % Choices.Length] = !Choices[stady % Choices.Length];
                start = i;
                points = 0;
                stady++;
                goto end;
            skip:;
                if (GameInfo.Rounds[i].Player1 == MyNumber)
                {
                    addPoints(ref points, GameInfo.Rounds[i].Player1_Choice, GameInfo.Rounds[i].Player2_Choice, GameInfo);
                    myRound++;
                    if (myRound > 0 && myRound % (GameInfo.QuantityPlayers * 2) == 0)
                    {
                        goto nextStady;
                    }
                }
                else if (GameInfo.Rounds[i].Player2 == MyNumber)
                {
                    addPoints(ref points, GameInfo.Rounds[i].Player2_Choice, GameInfo.Rounds[i].Player1_Choice, GameInfo);
                    myRound++;
                    if (myRound > 0 && myRound % (GameInfo.QuantityPlayers * 2) == 0)
                    {
                        goto nextStady;
                    }
                }
            end:;
            }
            return !Choices[OpponentNumber];
        }
    }
}
