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
    public partial class Form1 : Form
    {
        public static Type[] typelist;
        Type[] entireTypeList;
        bool[] onStrategies;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            entireTypeList = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "DilemmaGame.Strategies");
            onStrategies = new bool[entireTypeList.Length];
            for (int i = 0; i < onStrategies.Length; i++)
            {
                onStrategies[i] = true;
            }
            //label1.Text = typelist[0].Name;
            //typelist[0].GetMethod("Strategy");
            for (int i = 0; i < entireTypeList.Length; i++)
            {
                Button StrategyDescription = new Button();
                //определение координат новой кнопки
                StrategyDescription.Left = 10; //по горизонтали
                StrategyDescription.Top = 50 * (i + 1); //по вертикали
                StrategyDescription.Name = "btn" + i; //задание имени кнопки
                StrategyDescription.Text = entireTypeList[i].Name;
                StrategyDescription.Click += ShowDescription; //событие которое происходит при клике на каждую кнопку
                StrategyDescription.Width = 280;
                StrategyDescription.Height = 35;
                StrategyDescription.ForeColor = Color.FromArgb(200, 200, 200);
                StrategyDescription.BackColor = Color.FromArgb(10, 10, 40);
                StrategyDescription.Font = new Font("Microsoft Sans Serif", 18.25f);
                this.Controls.Add(StrategyDescription); //добавляет кнопку на форму

                Button chooseCheck = new Button();
                //определение координат новой кнопки
                chooseCheck.Left = 20 + StrategyDescription.Width; //по горизонтали
                chooseCheck.Top = 50 * (i + 1); //по вертикали
                chooseCheck.Name =  Convert.ToString(i); //задание имени кнопки
                chooseCheck.Text = "T";
                chooseCheck.Click += ChooseStrategy; //событие которое происходит при клике на каждую кнопку
                chooseCheck.Width = 36;
                chooseCheck.Height = 38;
                chooseCheck.ForeColor = Color.FromArgb(10, 100, 40);
                chooseCheck.BackColor = Color.FromArgb(10, 100, 40);
                chooseCheck.Font = new Font("Microsoft Sans Serif", 18.25f);
                this.Controls.Add(chooseCheck); //добавляет кнопку на форму
            }
        }
        private void ChooseStrategy(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "T")
            {
                button.Text = "F";
                button.BackColor = Color.FromArgb(140, 10, 40);
                button.ForeColor = Color.FromArgb(140, 10, 40);
                onStrategies[Convert.ToInt32(button.Name)] = false;
            }
            else
            {
                button.Text = "T";
                button.BackColor = Color.FromArgb(10, 100, 40);
                button.ForeColor = Color.FromArgb(10, 100, 40);
                onStrategies[Convert.ToInt32(button.Name)] = true;
            }
        }
        private void ShowDescription(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string cache = "";
            //берётся номер кнопки
            for (int i = 3; i < btn.Name.Length; i++)
            {
                cache += btn.Name[i];
            }
            int n = Convert.ToInt32(cache);
            cache = "";
            ConstructorInfo Constructor = entireTypeList[n].GetConstructor(Type.EmptyTypes);
            object ClassObject = Constructor.Invoke(new object[] { });

            // Берётся метод
            MethodInfo DescriptionMethod = entireTypeList[n].GetMethod("Description");
            object textDescription = DescriptionMethod.Invoke(ClassObject, new object[] { /*входящие аргументы*/});
            Description description = new Description();
            description.textBox1.Text = Convert.ToString(textDescription);
            description.Show();
            //label1.Text = Convert.ToString(textDescription);
            //label1.Text = typelist[n].GetMember("Description");
            //description.Description = typelist[n].GetMethod("Description");
        }

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings form = new Settings();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            typelist = new Type[0];

            for (int i = 0; i < entireTypeList.Length; i++)
            {
                if (onStrategies[i])
                {
                    Array.Resize(ref typelist, typelist.Length + 1);
                    typelist[typelist.Length - 1] = entireTypeList[i];
                }
            }
            Game game = new Game();
            game.Show();
        }
    }
}
