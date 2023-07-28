using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_game
{
    public partial class Form1 : Form
    {
        private Label _snakehead;
        private int _snakepiecenumber;
        private int _snakesize = 20;
        private int _fruitsize = 20;
        private Random _random;
        private Label _fruit;
        private Movement _direction;
        private int _snakeheaddistance = 1;

        public Form1()
        {
            InitializeComponent();
            _random = new Random();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _snakepiecenumber = 0;
            snakeplace();
            fruitcreate();
            fruitreplace();
            _direction = Movement.up;
            timersnake.Enabled = true;



        }
        private void Sifirla()
        {
            fruitcreate();
            fruitreplace();
        }
        private Label snakepiececreate(int locationX, int locationY)
        {


            _snakepiecenumber++;
            Label lbl = new Label();
            {
                Name = "snakepiece" + _snakepiecenumber;
                BackColor = Color.Blue;
                Width = _snakesize;
                Height = _snakesize;
                Location = new Point(locationX, locationY);
            }
            this.pnl.Controls.Add(lbl);
            return lbl;

        }

        private void snakeplace()
        {
            _snakehead = snakepiececreate(0, 0);
            var LocationX = (pnl.Width / 2) - (_snakehead.Width / 2);
            var LocationY = (pnl.Height / 2) - (_snakehead.Height / 2);
            _snakehead.Location = new Point(LocationX, LocationY);
        }
        private void fruitcreate()
        {
            Label lbl = new Label();
            {
                Name = "fruit";
                BackColor = Color.White;
                Width = _snakesize;
                Height = _snakesize;

            }
            _fruit = lbl;
            this.pnl.Controls.Add(lbl);

        }
        private void fruitreplace()
        {
            var locationX = 0;
            var locationY = 0;

            bool durum;
            do
            {
                durum = false;
                var locationx = _random.Next(0, pnl.Width - _fruitsize);
                var locationy = _random.Next(0, pnl.Height - _fruitsize);
                var rect1 = new Rectangle(new Point(locationX, locationY), _fruit.Size);

                foreach (Control control in pnl.Controls)
                {
                    if (control is Label && control.Name.Contains("snakepiece"))
                    {
                        var rect2 = new Rectangle(control.Location, control.Size);
                        if (rect1.IntersectsWith(rect2))
                        {
                            durum = true;
                            break;
                        }
                    }
                }

                _fruit.Location = new Point(locationX, locationY);

            } while (durum);





        }
        private enum Movement
        {
            up,
            down,
            rigth,
            left,
        }



        private void Form1_KeyDown(Object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            switch (keyCode)
            {
                case Keys.W:
                    _direction = Movement.up;
                    break;
                case Keys.S:
                    _direction = Movement.down;
                    break;
                case Keys.A:
                    _direction = Movement.left;
                    break;
                case Keys.D:
                    _direction = Movement.rigth;
                    break;
                default:
                    break;
            }
        }

        private void timersnake_Tick(object sender, EventArgs e)
        {
            snakeheadfollow();
            var locationX = _snakehead.Location.X;
            var locationY = _snakehead.Location.Y;

            switch (_direction)
            {
                case Movement.up:
                    _snakehead.Location = new Point(locationX, locationY - (_snakehead.Width + _snakeheaddistance));
                    break;
                case Movement.down:
                    _snakehead.Location = new Point(locationX, locationY + (_snakehead.Width + _snakeheaddistance));
                    break;
                case Movement.rigth:
                    _snakehead.Location = new Point(locationX + (_snakehead.Width + _snakeheaddistance), locationY);
                    break;
                case Movement.left:
                    _snakehead.Location = new Point(locationX - (_snakehead.Width + _snakeheaddistance), locationY);
                    break;
                default:
                    break;
            }
            snakefruiteat();
           
        }
        private void snakefruiteat()
        {
            var rect1 = new Rectangle(_snakehead.Location, _snakehead.Size);
            var rect2 = new Rectangle(_fruit.Location, _fruit.Size);

            if (rect1.IntersectsWith(rect2))
            {
                lblPUAN.Text = (Convert.ToInt32(lblPUAN.Text) + 5).ToString();
                fruitreplace();
                snakepiececreate(-_snakesize, -_snakesize);
            }
        }
        private void snakeheadfollow()
        {
            if (_snakepiecenumber <= 1) return;
            for (int i = _snakepiecenumber; i>1; i--)
            {
                var Nextpiece = (Label)pnl.Controls[i];
                var Beforepiece = (Label)pnl.Controls[i - 1];
                Nextpiece.Location = Beforepiece.Location;
            }
        }

    }

}
