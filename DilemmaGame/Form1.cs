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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "DilemmaGame.Strategies");
            //label1.Text = typelist[0].Name;
            //typelist[0].GetMethod("Strategy");
            for (int i = 0; i < typelist.Length; i++)
            {
                Button StrategyDescription = new Button();
                //определение координат новой кнопки
                StrategyDescription.Left = 10; //по горизонтали
                StrategyDescription.Top = 50 * (i + 1); //по вертикали
                StrategyDescription.Name = "btn" + i; //задание имени кнопки
                StrategyDescription.Text = typelist[i].Name;
                StrategyDescription.Click += ShowDescription; //событие которое происходит при клике на каждую кнопку
                StrategyDescription.Width = 280;
                StrategyDescription.Height = 35;
                StrategyDescription.ForeColor = Color.FromArgb(200, 200, 200);
                StrategyDescription.BackColor = Color.FromArgb(10, 10, 40);
                StrategyDescription.Font = new Font("Microsoft Sans Serif", 18.25f);
                this.Controls.Add(StrategyDescription); //добавляет кнопку на форму
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
            ConstructorInfo Constructor = typelist[n].GetConstructor(Type.EmptyTypes);
            object ClassObject = Constructor.Invoke(new object[] { });

            // Берётся метод
            MethodInfo DescriptionMethod = typelist[n].GetMethod("Description");
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
            Game game = new Game();
            game.Show();
        }
    }
}
