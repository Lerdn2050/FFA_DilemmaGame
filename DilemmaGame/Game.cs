using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace DilemmaGame
{
    public partial class Game : Form
    {
        public class GameInfo
        {
            Round[] rounds = new Round[0]; //История раундов
            private Statistic statistic; //Статистика игры
            private Info info;
            private int quantityPlayers; //Количество игроков

            public GameInfo(int QuantityPlayers, Info info)
            {
                this.quantityPlayers = QuantityPlayers;
                this.info = info;
                statistic = new Statistic(QuantityPlayers);
            }

            public Round[] Rounds { get => rounds; }
            public int QuantityPlayers { get => quantityPlayers; }
            public Info inf { get => info; set => info = value; }
            public Statistic Stat { get => statistic; }

            public static GameInfo operator +(GameInfo gameInfo, Round round)
            {
                Array.Resize(ref gameInfo.rounds, gameInfo.Rounds.Length + 1);
                gameInfo.Rounds[gameInfo.Rounds.Length - 1] = new Round(round);
                if (round.Player1_Choice)
                {
                    if (round.Player2_Choice)
                    {
                        gameInfo.statistic.AmountOfPoints += gameInfo.info.CoopCoop * 2;
                        gameInfo.statistic.QuantityCoop += 2;
                        gameInfo.statistic.players[round.Player1].QuantityCoop++;
                        gameInfo.statistic.players[round.Player1].QuantityReceivedCoop++;
                        gameInfo.statistic.players[round.Player1].AmountOfPoints += gameInfo.info.CoopCoop;
                        gameInfo.statistic.players[round.Player2].QuantityCoop++;
                        gameInfo.statistic.players[round.Player2].QuantityReceivedCoop++;
                        gameInfo.statistic.players[round.Player2].AmountOfPoints += gameInfo.info.CoopCoop;
                    }
                    else
                    {
                        gameInfo.statistic.AmountOfPoints += gameInfo.info.CoopBetray + gameInfo.info.BetrayCoop;
                        gameInfo.statistic.QuantityCoop++;
                        gameInfo.statistic.QuantityBetray++;
                        gameInfo.statistic.players[round.Player1].QuantityCoop++;
                        gameInfo.statistic.players[round.Player1].QuantityReceivedBetray++;
                        gameInfo.statistic.players[round.Player1].AmountOfPoints += gameInfo.info.CoopBetray;
                        gameInfo.statistic.players[round.Player2].QuantityBetray++;
                        gameInfo.statistic.players[round.Player2].QuantityReceivedCoop++;
                        gameInfo.statistic.players[round.Player2].AmountOfPoints += gameInfo.info.BetrayCoop;
                    }
                }
                else
                {
                    if (round.Player2_Choice)
                    {
                        gameInfo.statistic.AmountOfPoints += gameInfo.info.CoopBetray + gameInfo.info.BetrayCoop;
                        gameInfo.statistic.QuantityCoop++;
                        gameInfo.statistic.QuantityBetray++;
                        gameInfo.statistic.players[round.Player1].QuantityBetray++;
                        gameInfo.statistic.players[round.Player1].QuantityReceivedCoop++;
                        gameInfo.statistic.players[round.Player1].AmountOfPoints += gameInfo.info.BetrayCoop;
                        gameInfo.statistic.players[round.Player2].QuantityCoop++;
                        gameInfo.statistic.players[round.Player2].QuantityReceivedBetray++;
                        gameInfo.statistic.players[round.Player2].AmountOfPoints += gameInfo.info.CoopBetray;
                    }
                    else
                    {
                        gameInfo.statistic.AmountOfPoints += gameInfo.info.BetrayBetray * 2;
                        gameInfo.statistic.QuantityBetray += 2;
                        gameInfo.statistic.players[round.Player1].QuantityBetray++;
                        gameInfo.statistic.players[round.Player1].QuantityReceivedBetray++;
                        gameInfo.statistic.players[round.Player1].AmountOfPoints += gameInfo.info.BetrayBetray;
                        gameInfo.statistic.players[round.Player2].QuantityBetray++;
                        gameInfo.statistic.players[round.Player2].QuantityReceivedBetray++;
                        gameInfo.statistic.players[round.Player2].AmountOfPoints += gameInfo.info.BetrayBetray;
                    }
                }
                //gameInfo.statistic
                return gameInfo;
            }
            public class Info //Информация о последствии выборов
            {
                private int coopCoop; //за общее сотрудничество
                private int betrayCoop; //за предательство при сотрудничестве
                private int coopBetray; //за сотрудничество при предательстве
                private int betrayBetray; //за общее предательство
                private bool skipRound; //пропуск хода если игрок предан
                public Info(int coopCoop, int betrayCoop, int coopBetray, int betrayBetray, bool skipRound)
                {
                    this.coopCoop = coopCoop;
                    this.betrayCoop = betrayCoop;
                    this.coopBetray = coopBetray;
                    this.betrayBetray = betrayBetray;
                    this.skipRound = skipRound;
                }

                public int CoopCoop { get => coopCoop; }
                public int BetrayCoop { get => betrayCoop; }
                public int CoopBetray { get => coopBetray; }
                public int BetrayBetray { get => betrayBetray; }
                public bool SkipRound { get => skipRound; }
            }

            public class Round //Информация о раунде
            {
                private int player1;
                private int player2;
                private bool player1_Choice;
                private bool player2_Choice;
                public Round(int Player1, int Player2, bool Player1_Choice, bool Player2_Choice)
                {
                    this.player1 = Player1;
                    this.player2 = Player2;
                    this.player1_Choice = Player1_Choice;
                    this.player2_Choice = Player2_Choice;
                }
                public Round(Round round)
                {
                    this.player1 = round.Player1;
                    this.player2 = round.Player2;
                    this.player1_Choice = round.Player1_Choice;
                    this.player2_Choice = round.Player2_Choice;
                }
                public int Player1 { get => player1; }
                public int Player2 { get => player2; }
                public bool Player1_Choice { get => player1_Choice; }
                public bool Player2_Choice { get => player2_Choice; }
            }
            public class Statistic //Статистика игры
            {
                private Player[] Players;
                private int amountOfPoints = 0; //Общее количество очков
                private int quantityBetray = 0; //Общее Количество предательств
                private int quantityCoop = 0; //Общее Количество сотрудничеств
                public Statistic(int QuantityPlayers)
                {
                    players = new Player[QuantityPlayers];
                    for (int i = 0; i < QuantityPlayers; i++)
                    {
                        players[i] = new Player();
                    }
                }

                public int AmountOfPoints { get => amountOfPoints; set => amountOfPoints = value; }
                public int QuantityBetray { get => quantityBetray; set => quantityBetray = value; }
                public int QuantityCoop { get => quantityCoop; set => quantityCoop = value; }
                public Player[] players { get => Players; set => Players = value; }

                public class Player
                {
                    Round[] StepHistory = new Round[0];
                    private int amountOfPoints = 0; //Количество очков
                    private int quantityBetray = 0; //Количество предательств
                    private int quantityCoop = 0; //Количество сотрудничеств
                    private int quantityReceivedBetray = 0; //Количество предательств от оппонентов
                    private int quantityReceivedCoop = 0; //Количество сотрудничеств от оппонентов

                    public int AmountOfPoints { get => amountOfPoints; set => amountOfPoints = value; }
                    public int QuantityBetray { get => quantityBetray; set => quantityBetray = value; }
                    public int QuantityCoop { get => quantityCoop; set => quantityCoop = value; }
                    public int QuantityReceivedBetray { get => quantityReceivedBetray; set => quantityReceivedBetray = value; }
                    public int QuantityReceivedCoop { get => quantityReceivedCoop; set => quantityReceivedCoop = value; }
                }
            }
        }
        public class Table
        {
            private Column[] columns;
            public Table(Column[] columns)
            {
                this.columns = columns;
            }
            public Table(params string[][] columns)
            {
                this.columns = new Column[columns.Length];
                for (int i = 0; i < columns.Length; i++)
                {
                    this.columns[i] = new Column("", columns[i]);
                }
            }
            public Table(string[] firstColumn, params int[][] columns)
            {
                this.columns = new Column[columns.Length + (firstColumn == null ? 0 : 1)];
                string[][] textColumns = new string[columns.Length][];

                for (int i = 0; i < columns.Length; i++)
                {
                    textColumns[i] = new string[columns[i].Length];
                    for (int k = 0; k < columns[i].Length; k++)
                    {
                        textColumns[i][k] = Convert.ToString(columns[i][k]);
                    }
                }
                if(firstColumn != null)
                    this.columns[0] = new Column("", firstColumn);
                for (int i = 0; i < textColumns.Length; i++)
                {
                    this.columns[i + (firstColumn == null ? 0 : 1)] = new Column("", textColumns[i]);
                }
            }

            public Column[] Columns { get => columns; set => columns = value; }

            public void addNames(ref Table table, params string[] names)
            {
                for (int i = 0; i < names.Length && i < table.columns.Length; i++)
                {
                    table.columns[i].Name = names[i];
                }
            }
            public class Column
            {
                private string name;
                private string[] cells;
                public Column(string name, params string[] cells)
                {
                    this.name = name;
                    this.cells = cells;
                }

                public string Name { get => name; set => name = value; }
                public string[] Cells { get => cells; set => cells = value; }
            }
        }

        private void showTable(Table table) //отображение таблицы
        {
            ClearElements();
            for (int i = 0; i < table.Columns.Length; i++)
            {
                CreateCell(("Cell#" + i + ",0"), i * 202 + 250, 20, table.Columns[i].Name, true);
                for (int k = 1; k < table.Columns[i].Cells.Length + 1; k++)
                {
                    CreateCell(("Cell#" + i + "," + k), i * 202 + 250, 20 + k * 40, table.Columns[i].Cells[k - 1], true);
                }
            }

        }


        public Game()
        {
            InitializeComponent();

        }
        Type[] typelist;
        GameInfo.Info info;
        GameInfo GI;
        private void Game_Load(object sender, EventArgs e)
        {
            //расчёт результатов

            //сбор данных
            typelist = Form1.typelist;
            info = new GameInfo.Info(Settings.CoopCoop, Settings.BetrayCoop, Settings.CoopBetray, Settings.BetrayBetray, Settings.SkipRound);
            GI = new GameInfo(typelist.Length, info);

            int[] skipes = new int[typelist.Length]; //количество пропускаемых ходов для каждого

            //расчёт
            int player = 0;
            int opponent = 0;
            while (!EndCheck(GI.Rounds.Length))
            {
                
                if (player == opponent)
                {
                    if (opponent + 1 != typelist.Length)
                    {
                        opponent++;
                    }
                    else
                    {
                        goto skip;
                    }
                }
                bool pChoice = GetChoice(player, opponent);//выбор текущего игрока
                bool oChoice = GetChoice(opponent, player); //выбор оппонента
                skipes[player] += oChoice ? 0 : 1;
                skipes[opponent] += pChoice ? 0 : 1;
                GI += new GameInfo.Round(player, opponent, pChoice, oChoice);
            skip:;
                opponent++;
                if (opponent == typelist.Length)
                {
                    opponent = 0;
                    player = (player + 1) % typelist.Length;
                    if (GI.inf.SkipRound)
                    {
                        while (skipes[player] > 0)
                        {
                            skipes[player]--;
                            player = (player + 1) % typelist.Length;
                        }
                    }
                }
            }

        }
        private bool GetChoice(int player, int opponent)
        {
            ConstructorInfo Constructor = typelist[player].GetConstructor(Type.EmptyTypes);
            object ClassObject = Constructor.Invoke(new object[] { });

            // Берётся метод
            MethodInfo DescriptionMethod = typelist[player].GetMethod("Strategy");
            object textDescription = DescriptionMethod.Invoke(ClassObject, new object[] { GI, player, opponent });
            return Convert.ToBoolean(textDescription);
        }
        public static Random j = new Random();
        private bool EndCheck(int round) //true - конец игры
        {
            

            switch (Settings.EndGame)
            {
                case "До границы":
                    if (round < Settings.EndRound)
                    {
                        return false;
                    }
                    break;
                case "С шансом":
                    if (j.Next(Settings.oddsDivisor) > Settings.oddsDivisible)
                    {
                        return false;
                    }
                    break;
                case "В диапазоне":
                    if (round < Settings.MinRound)
                    {
                        return false;
                    }
                    else if (j.Next(Settings.MaxRound - round) > 0)
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }

        object[] elements = new object[0];

        private void CreateLabel(string text, int x, int y)
        {
            Array.Resize(ref elements, elements.Length + 1);
            Label label = new Label();
            label.Text = text;
            label.Left = x;
            label.Top = y;
            label.Width = label.Text.Length * 25;
            label.Height = 38;
            label.Name = "lbl" + (elements.Length - 1);
            label.BackColor = Color.Transparent;
            label.ForeColor = Color.FromArgb(200, 200, 200);
            label.Font = new Font("Microsoft Sans Serif", 18.25f);
            this.Controls.Add(label);
            elements[elements.Length - 1] = label;
        }
        private void CreateNumeric(string name, int x, int y, int Value, int minValue, int maxValue)
        {
            Array.Resize(ref elements, elements.Length + 1);
            NumericUpDown numeric = new NumericUpDown();
            numeric.Minimum = minValue;
            numeric.Maximum = maxValue;
            numeric.Value = Value;
            numeric.Left = x;
            numeric.Top = y;
            numeric.Width = 150;
            numeric.Height = 38;
            numeric.Name = name;
            numeric.BackColor = Color.FromArgb(10, 10, 40);
            numeric.ForeColor = Color.FromArgb(200, 200, 200);
            numeric.Font = new Font("Microsoft Sans Serif", 18.25f);

            this.Controls.Add(numeric);
            elements[elements.Length - 1] = numeric;
        }
        private void CreateCell(string name, int x, int y, string text, bool readOnly)
        {
            Array.Resize(ref elements, elements.Length + 1);
            TextBox cell = new TextBox();
            cell.Left = x;
            cell.Top = y;
            cell.Width = 200;
            cell.Height = 38;
            cell.Name = name;
            cell.Text = text;
            cell.BackColor = Color.FromArgb(10, 10, 40);
            cell.ForeColor = Color.FromArgb(200, 200, 200);
            cell.Font = new Font("Microsoft Sans Serif", 18.25f);
            //возможность менять значения
            cell.ReadOnly = readOnly;

            this.Controls.Add(cell);
            elements[elements.Length - 1] = cell;
        }
        private void ClearElements()
        {
            foreach (object element in elements)
            {
                this.Controls.Remove((Control)element);
            }
            elements = new object[0];
        }

        private void button1_Click(object sender, EventArgs e) //Топ
        {
            int[] playersNumbers = new int[typelist.Length];
            int[] playersScores = new int[typelist.Length];
            for (int i = 0; i < typelist.Length; i++)
            {
                playersNumbers[i] = i;
                playersScores[i] = GI.Stat.players[i].AmountOfPoints;
            }
            QuickSort(ref playersScores, ref playersNumbers, 0, typelist.Length - 1);
            Array.Reverse(playersScores);
            Array.Reverse(playersNumbers);
            //отображение данных из массивов
            string[] playersNames = new string[Form1.typelist.Length];
            for (int i = 0; i < playersNames.Length; i++)
            {
                playersNames[i] = Form1.typelist[playersNumbers[i]].Name;
            }
            Table table = new Table(playersNames, playersScores);
            table.addNames(ref table, "Игрок", "Счёт");
            
            showTable(table);
        }
        private void button2_Click(object sender, EventArgs e) //Общие
        {

            /*Вывести:
            GI.Stat.AmountOfPoints;
            GI.Stat.QuantityBetray;
            GI.Stat.QuantityCoop;
            */

            Table.Column[] columns = new Table.Column[3];
            columns[0] = new Table.Column("Общий счёт", Convert.ToString(GI.Stat.AmountOfPoints));
            columns[1] = new Table.Column("Предательств", Convert.ToString(GI.Stat.QuantityBetray));
            columns[2] = new Table.Column("Сотрудничеств", Convert.ToString(GI.Stat.QuantityCoop));
            showTable(new Table(columns));
        }

        private void button3_Click(object sender, EventArgs e) //Взаимодействия
        {
            /*
            вывести о игроках по очереди:
            GI.Stat.players[i].QuantityBetray;
            GI.Stat.players[i].QuantityCoop;
            GI.Stat.players[i].QuantityReceivedBetray;
            GI.Stat.players[i].QuantityReceivedCoop;
            */
            int[] playersQuantityBetray = new int[typelist.Length];
            int[] playersQuantityCoop = new int[typelist.Length];
            int[] playersQuantityReceivedBetray = new int[typelist.Length];
            int[] playersQuantityReceivedCoop = new int[typelist.Length];
            for (int i = 0; i < typelist.Length; i++)
            {
                playersQuantityBetray[i] = GI.Stat.players[i].QuantityBetray;
                playersQuantityCoop[i] = GI.Stat.players[i].QuantityCoop;
                playersQuantityReceivedBetray[i] = GI.Stat.players[i].QuantityReceivedBetray;
                playersQuantityReceivedCoop[i] = GI.Stat.players[i].QuantityReceivedCoop;
            }
            //считывание имён соответствующих номерам
            string[] playersNames = new string[Form1.typelist.Length];
            for (int i = 0; i < playersNames.Length; i++)
            {
                playersNames[i] = Form1.typelist[i].Name;
            }
            //перевод данных в таблицу
            Table table = new Table(playersNames, playersQuantityBetray, playersQuantityCoop, playersQuantityReceivedBetray, playersQuantityReceivedCoop);
            table.addNames(ref table, "Игрок", "Предательства", "Сотрудничества", "Предан", "Не предан");
            //отображение таблицы
            showTable(table);
        }

        private void button4_Click(object sender, EventArgs e) //Все данные
        {
            int[] playersNumbers = new int[typelist.Length];
            int[] playersScores = new int[typelist.Length];
            int[] playersQuantityBetray = new int[typelist.Length];
            int[] playersQuantityCoop = new int[typelist.Length];
            int[] playersQuantityReceivedBetray = new int[typelist.Length];
            int[] playersQuantityReceivedCoop = new int[typelist.Length];
            for (int i = 0; i < typelist.Length; i++)
            {
                playersNumbers[i] = i;
                playersScores[i] = GI.Stat.players[i].AmountOfPoints;
                playersQuantityBetray[i] = GI.Stat.players[i].QuantityBetray;
                playersQuantityCoop[i] = GI.Stat.players[i].QuantityCoop;
                playersQuantityReceivedBetray[i] = GI.Stat.players[i].QuantityReceivedBetray;
                playersQuantityReceivedCoop[i] = GI.Stat.players[i].QuantityReceivedCoop;
            }
            QuickSort(ref playersScores, ref playersNumbers, 0, typelist.Length - 1);
            Array.Reverse(playersScores);
            Array.Reverse(playersNumbers);
            playersQuantityBetray = ArraySynchronization(playersNumbers, playersQuantityBetray);
            playersQuantityCoop = ArraySynchronization(playersNumbers, playersQuantityCoop);
            playersQuantityReceivedBetray = ArraySynchronization(playersNumbers, playersQuantityReceivedBetray);
            playersQuantityReceivedCoop = ArraySynchronization(playersNumbers, playersQuantityReceivedCoop);
            //отображение данных из массивов
            //считывание имён соответствующих номерам
            string[] playersNames = new string[Form1.typelist.Length];
            for (int i = 0; i < playersNames.Length; i++)
            {
                playersNames[i] = Form1.typelist[playersNumbers[i]].Name;
            }
            //перевод данных в таблицу
            Table table = new Table(playersNames, playersScores, playersQuantityBetray, playersQuantityCoop, playersQuantityReceivedBetray, playersQuantityReceivedCoop);
            table.addNames(ref table, "Игрок", "Счёт", "Предательства", "Сотрудничества", "Предан", "Не предан");
            //отображение таблицы
            showTable(table);
        }
        private int[] ArraySynchronization(int [] Numbers, int [] Array)
        {
            int[] NewArray = new int[Numbers.Length];
            for (int i = 0; i < Numbers.Length; i++)
            {
                NewArray[i] = Array[Numbers[i]];
            }
            return NewArray;
        }
        private int Partition(ref int[] Array, ref int[] Array2, int start, int end) //для метода QuickSort
        {
            int temp;//для смены ячеек местами
            int temp2;
            int marker = start;//Граница подмассивов
            for (int i = start; i <= end; i++)
            {
                if (Array[i] < Array[end]) //array[end] это сводная ячейка
                {
                    temp = Array[marker]; // смена местами маркера с текущей расматриваемой ячейкой
                    temp2 = Array2[marker];
                    Array[marker] = Array[i];
                    Array2[marker] = Array2[i];
                    Array[i] = temp;
                    Array2[i] = temp2;
                    marker += 1;
                }
            }
            //сводная ячейка помещается между левым и правым подмассивами
            temp = Array[marker];
            temp2 = Array2[marker];
            Array[marker] = Array[end];
            Array2[marker] = Array2[end];
            Array[end] = temp;
            Array2[end] = temp2;
            return marker;
        }

        private void QuickSort(ref int[] Array, ref int[] Array2, int start, int end) //Сортирует от меньшего к большему, второй массив синхранизируется с первым
        {
            if (start >= end)
            {
                return;
            }
            int pivot = Partition(ref Array, ref Array2, start, end);
            QuickSort(ref Array, ref Array2, start, pivot - 1);
            QuickSort(ref Array, ref Array2, pivot + 1, end);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] roundsNumbers = new string[GI.Rounds.Length];
            int [] playersNumbers = new int[GI.Rounds.Length];
            int[] opponentsNumbers = new int[GI.Rounds.Length];
            string[] playersChoses = new string[GI.Rounds.Length];
            string[] opponentsChoses = new string[GI.Rounds.Length];
            for (int i = 0; i < GI.Rounds.Length; i++)
            {
                roundsNumbers[i] = Convert.ToString(i);
                playersNumbers[i] = GI.Rounds[i].Player1;
                opponentsNumbers[i] = GI.Rounds[i].Player2;
                playersChoses[i] = Convert.ToString(GI.Rounds[i].Player1_Choice);
                opponentsChoses[i] = Convert.ToString(GI.Rounds[i].Player2_Choice);
            }
            string[] playersNames = new string[GI.Rounds.Length];
            string[] opponentsNames = new string[GI.Rounds.Length];
            for (int i = 0; i < playersNames.Length; i++)
            {
                playersNames[i] = Form1.typelist[playersNumbers[i]].Name;
                opponentsNames[i] = Form1.typelist[opponentsNumbers[i]].Name;
            }
            Table table = new Table(roundsNumbers, playersNames, opponentsNames, playersChoses, opponentsChoses);
            table.addNames(ref table, "Номер раунда", "Игрок", "Оппонент", "Выбор игрока", "Выбор оппонента");
            showTable(table);
        }
    }
}
