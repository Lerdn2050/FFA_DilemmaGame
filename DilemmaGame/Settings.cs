using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DilemmaGame
{
    public partial class Settings : Form
    {
        public static string EndGame = "До границы";
        public static int EndRound = 100;
        public static int MinRound = 100;
        public static int MaxRound = 200;
        public static int oddsDivisible = 1;
        public static int oddsDivisor = 100;
        public static int CoopCoop = 3;
        public static int BetrayCoop = 5;
        public static int CoopBetray = 0;
        public static int BetrayBetray = 1;
        public static bool SkipRound = true;
        object[] elements = new object[0];


        public Settings()
        {
            InitializeComponent();
            numericUpDown1.Value = BetrayBetray;
            numericUpDown2.Value = CoopBetray;
            numericUpDown3.Value = BetrayCoop;
            numericUpDown4.Value = CoopCoop;
            button5.Text = SkipRound ? "F" : "T"; //обратное значение
            button5_Click(button5, null); //смена значения искуственным кликом
            ShowSettingsMode();
        }

        private void ShowSettingsMode()
        {
            switch (EndGame)
            {
                case "До границы":
                    button2.BackColor = Color.FromArgb(10, 50, 40);
                    button3.BackColor = Color.FromArgb(80, 10, 20);
                    button4.BackColor = Color.FromArgb(80, 10, 20);
                    CreateLabel("Количество раундов", 300, 53);
                    CreateNumeric("EndRound", 305, 90, EndRound, 10, 100000);
                    break;
                case "С шансом":
                    button2.BackColor = Color.FromArgb(80, 10, 20);
                    button3.BackColor = Color.FromArgb(10, 50, 40);
                    button4.BackColor = Color.FromArgb(80, 10, 20);
                    CreateLabel("Вероятность завершения", 250, 53);
                    CreateNumeric("oddsDivisible", 255, 90, oddsDivisible, 1, oddsDivisor);
                    CreateLabel("на", 415, 90);
                    CreateNumeric("oddsDivisor", 462, 90, oddsDivisor, oddsDivisible, 100000);
                    break;
                case "В диапазоне":
                    button2.BackColor = Color.FromArgb(80, 10, 20);
                    button3.BackColor = Color.FromArgb(80, 10, 20);
                    button4.BackColor = Color.FromArgb(10, 50, 40);
                    CreateLabel("Количество раундов", 300, 53);
                    CreateLabel("От", 300, 96);
                    CreateNumeric("MinRound", 350, 90, MinRound, 1, MaxRound);
                    CreateLabel("До", 300, 142);
                    CreateNumeric("MaxRound", 350, 137, MaxRound, MinRound, 100000);
                    break;
            }
        }

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
        private void ClearElements()
        {
            foreach (object element in elements)
            {
                this.Controls.Remove((Control)element);
            }
            elements = new object[0];
        }
        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //До границы
        {
            EndGame = "До границы";
            ClearElements();
            ShowSettingsMode();
        }

        private void button3_Click(object sender, EventArgs e) //С шансом
        {
            EndGame = "С шансом";
            ClearElements();
            ShowSettingsMode();
        }

        private void button4_Click(object sender, EventArgs e) //В диапазоне
        {
            EndGame = "В диапазоне";
            ClearElements();
            ShowSettingsMode();
        }

        private void button1_Click(object sender, EventArgs e) //Задать
        {
            NumericUpDown numeric;
            switch (elements.Length)
            {
                case 2: //до границы
                    numeric = (NumericUpDown)elements[1]; //EndRound
                    EndRound = (int)numeric.Value;
                    break;
                case 4: //с шансом
                    numeric = (NumericUpDown)elements[1]; //oddsDivisible
                    oddsDivisible = (int)numeric.Value;
                    numeric = (NumericUpDown)elements[3]; //oddsDivisor
                    oddsDivisor = (int)numeric.Value;
                    break;
                case 5: //в диапазоне
                    numeric = (NumericUpDown)elements[2]; //MinRound
                    MinRound = (int)numeric.Value;
                    numeric = (NumericUpDown)elements[4]; //MaxRound
                    MaxRound = (int)numeric.Value;
                    break;
            }
            BetrayBetray = (int)numericUpDown1.Value;
            CoopBetray = (int)numericUpDown2.Value;
            BetrayCoop = (int)numericUpDown3.Value;
            CoopCoop = (int)numericUpDown4.Value;
            SkipRound = button5.Text == "T";
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "T")
            {
                button.Text = "F";
                button.BackColor = Color.FromArgb(140, 10, 40);
                button.ForeColor = Color.FromArgb(140, 10, 40);
            }
            else
            {
                button.Text = "T";
                button.BackColor = Color.FromArgb(10, 100, 40);
                button.ForeColor = Color.FromArgb(10, 100, 40);
            }
        }
    }
}
