using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace WindowsFormsApp1
{
    
    public partial class PlayGame : Form
    {
        public string[,] quizes = {
        {"Какое растение существует на самом деле?", "Лох чилийский", "Лох индийский", "Лох греческий","Лох русский","Лох индийский"  }, 
        {"Что за место, попав в которое, человек делает селфи на кухне, которую не может себе позволить?", "Рим", "Париж", "Лондон", "Икея", "Икея"},
        {"Какой город объявлен официальной родиной русского Деда Мороза?", "Малая Вишера", "Великий Устюг", "Вышний Волочек", "Нижний Новгород","Великий Устюг"},
        {"Что проводит боксер, наносящий удар противнику снизу?", "Свинг", "Хук", "Апперкот", "Джэб","Апперкот"},
        {"К кому первому обратились за помощью дед и бабка, не справившись с репкой?", "К Жучке", "К дочке", "К внучке", "К залу","К внучке"},
        {"Кого нет среди смешариков?", "Барана", "Свиньи", "Коня", "Лося","Коня"},
        {"Как называется ближайшая к Земле звезда?", "Проксиома Центавра", "Солнце", "Полярная", "Сириус", "Солнце"},
        {"Что помогает запомнить мнемоническое правило «Это я знаю и помню прекрасно»?", "Число Пи", "Ряд активности металлов", "Цвета радуги", "Порядок падежей", "Число Пи"},
        {"Какую площадь имеет клетка стандартной школьной тетради?", "0.25 кв.см", "1 кв.см", "0.5 кв.см", "1.25 кв. см","0.25 кв.см"},
        {"Что происходит на соревнованиях по стрельбе, если соперники набрали одинаковое количество очков?", "Перевербовка", "Перепалка", "Перебранка", "Перестрелка", "Перестрелка"},
        {"Что вращается вокруг Земли?", "Луна", "Солнце", "Марс", "Венера","Луна"},
        {"Как назывались старинные русские пушки-гаубицы?", "Кентавр", "Грифон","Василиск", "Единорог", "Единорог"},
        {"Сколько раз в сутки подзаводят куранты Спасской башни Кремля?", "Один", "Два", "Три", "Четыре", "Два"},
        {"Кто из перечисленных был пажом во времена Екатерины II?", "Д.И. Фонвизин", "Г.Р. Державин", "А.Н. Радищев", "Н.М. Карамзин", "Н.М. Карамзин"},
        {"В какой из этих столиц бывших союзных республик раньше появилось метро?", "Тбилиси", "Ереван", "Баку", "Минск", "Тбилиси"}
        };
        public Button[] controllers;
        public int catHealth = 100;
        public int scientistHealth = 100;
        public PlayGame()
        {
            InitializeComponent();

            timer1.Interval = 30;
            timer1.Tick += Timer1_Tick;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

        }

        private void playButton_MouseClick(object sender, MouseEventArgs e)
        {
            //написать генерацию мира с паролем и без
            if (passwordBox.Text == "Catgirl")
            {
                CreateCatgirlGame("Catgirl sprite.png");
                controllers = CreateGame();
                var catHealthBar = CreateHealth(new Point(430, 600));
                catHealthBar.Value = 100;
                Controls.Add(catHealthBar);
                var scientistHealthBar = CreateHealth(new Point(430, 200));
                scientistHealthBar.Value = 100;
                Controls.Add(scientistHealthBar);
                var question = CreateLabel(new Size(530, 90), new Point(258, 264));
                Controls.Add(question);
                Random rnd = new Random();
                int quiz = rnd.Next(0, 14);
                question.Text = quizes[quiz, 0];
                controllers[0].Text = quizes[quiz, 1];
                controllers[1].Text = quizes[quiz, 2];
                controllers[2].Text = quizes[quiz, 3];
                controllers[3].Text = quizes[quiz, 4];

                controllers[0].Click += (sender, args) => doQuiz(1, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
                controllers[1].Click += (sender, args) => doQuiz(2, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
                controllers[2].Click += (sender, args) => doQuiz(3, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
                controllers[3].Click += (sender, args) => doQuiz(4, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
            }
            //режим для инпектора
            else
            {
                CreateCatgirlGame("Cat sprite.png");
                controllers = CreateGame();
                var catHealthBar = CreateHealth(new Point(430, 600));
                catHealthBar.Value = 100;
                Controls.Add(catHealthBar);
                var scientistHealthBar = CreateHealth(new Point(430, 200));
                scientistHealthBar.Value = 100;
                Controls.Add(scientistHealthBar);
                var question = CreateLabel(new Size(530, 90), new Point(258, 264));
                Controls.Add(question);
                Random rnd = new Random();
                int quiz = rnd.Next(0, 14);
                question.Text = quizes[quiz, 0];
                controllers[0].Text = quizes[quiz, 1];
                controllers[1].Text = quizes[quiz, 2];
                controllers[2].Text = quizes[quiz, 3];
                controllers[3].Text = quizes[quiz, 4];

                controllers[0].Click += (sender, args) => doQuiz(1, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
                controllers[1].Click += (sender, args) => doQuiz(2, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
                controllers[2].Click += (sender, args) => doQuiz(3, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
                controllers[3].Click += (sender, args) => doQuiz(4, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
            }
            //режим для буржуя
        }


        public void doQuiz(int i, Button[] controllers, string[,] quizes, int quiz,
            ProgressBar catHealthBar, ProgressBar scientistHealthBar, Label question)
        {
            if(catHealthBar.Value == 0 || scientistHealthBar.Value == 0)
            {
                Controls.Clear();
                var gameOver = new PictureBox()
                {
                    Size = new Size(512, 128),
                    Location = new Point(275, 340)
                };
                System.IO.FileStream fs = new System.IO.FileStream(@"..\..\..\Sprites\GameOver.png", System.IO.FileMode.Open);
                System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
                fs.Close();
                gameOver.Image = img;
                gameOver.BackColor = Color.Transparent;
                Controls.Add(gameOver);
            }
            if (controllers[i - 1].Text == quizes[quiz, 5])
            {
                scientistHealth = scientistHealth - 10;
                scientistHealthBar.Value = scientistHealth;
            }
            
            Random rnd = new Random();
            int a = rnd.Next(0, 1);
            if (10 * a > 0)
            {
                catHealth = catHealth - 10;
                catHealthBar.Value = catHealth;
            }
            
            quiz = rnd.Next(0, 14);
            question.Text = quizes[quiz, 0];
            controllers[0].Text = quizes[quiz, 1];
            controllers[1].Text = quizes[quiz, 2];
            controllers[2].Text = quizes[quiz, 3];
            controllers[3].Text = quizes[quiz, 4];


            controllers[0].Click += (sender, args) => doQuiz(1, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
            controllers[1].Click += (sender, args) => doQuiz(2, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
            controllers[2].Click += (sender, args) => doQuiz(3, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
            controllers[3].Click += (sender, args) => doQuiz(4, controllers, quizes, quiz, catHealthBar, scientistHealthBar, question);
        }

        public void CreateCatgirlGame(String catSprite)
        {
            Controls.Clear();
            Controls.Add(createHero(new Point(415, 635), catSprite, "Catgirl"));
            Controls.Add(createHero(new Point(415, 0), "Angry scientist1.png", "Angry Scientist"));
        }

        public PictureBox createHero(Point location, String sprite, string name)
        {
            PictureBox hero = new PictureBox()
            {
                Location = location,
                Size = new Size(200, 200)
            };
            System.IO.FileStream fs = new System.IO.FileStream(@"..\..\..\Sprites\" + sprite, System.IO.FileMode.Open);
            System.Drawing.Image img = System.Drawing.Image.FromStream(fs);
            fs.Close();
            hero.Image = img;
            hero.BackColor = Color.Transparent;
            return hero;
        }

        public Button[] CreateGame()
        {
            
            var answer1 = CreateButton(new Size(260, 65), new Point(258, 376));
            Controls.Add(answer1);
            var answer2 = CreateButton(new Size(260, 65), new Point(528, 376));
            Controls.Add(answer2);
            var answer3 = CreateButton(new Size(260, 65), new Point(528, 460));
            Controls.Add(answer3);
            var answer4 = CreateButton(new Size(260, 65), new Point(258, 460));
            Controls.Add(answer4);
            var allControls = new[] {answer1, answer2, answer3, answer4};
            return allControls;
        }


        public Button CreateButton(Size size, Point point)
        {
            Button question = new Button()
            {
                Size = size,
                Location = point
            };
            return question;
        }
        public Label CreateLabel(Size size, Point point)
        {
            Label question = new Label()
            {
                Size = size,
                Location = point
            };
            return question;
        }
        public  ProgressBar CreateHealth(Point point)
        {
            ProgressBar health = new ProgressBar()
            {
                Location = point,
                Size = new Size(190, 30),
                Value = 100,
                Text = "Health"
            };
            health.ForeColor = Color.Red;
            return health;
        }
    }

}
