using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 排块游戏
{
    public partial class 排块游戏 : Form
    {
        public 排块游戏()
        {
            InitializeComponent();
        }
        const int n = 4;
        Button[,] buttons = new Button[n, n];
        private void Form1_Load(object sender, EventArgs e)
        {
            generateAllButtons();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            shuffle();
        }
        void generateAllButtons()
        {
            int x0 = 100, y0 = 10, w = 45, d = 50;
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                {
                    int num = r * n + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Width = w;
                    btn.Height = w;
                    btn.Visible = true;
                    btn.Tag = r * n + c;

                    //btn.Click +=   new EventHandler(Btn_Click);
                    btn.Click += Btn_Click;

                    buttons[r, c] = btn;
                    this.Controls.Add(btn);
                }
            buttons[n - 1, n - 1].Visible = false;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Button blank = Findhiddenbutton();//查找隐藏按钮

            //判断是否和空白块相邻，是的话就交换
            if(IsNeighbor(btn,blank ))
            {
                swap(btn, blank);
                blank.Focus();
            }
            //判断是否完成了
            if(ResultIsOk())
            {
                MessageBox.Show("ok");
            }
        }

        //查找隐藏按钮
        Button Findhiddenbutton()
        {
            for(int r=0;r<n;r++)
                for(int c=0;c<n;c++)
                {
                    if(!buttons[r,c ].Visible)
                    {
                        return buttons[r, c];
                    }
                }
            return null;
        }
        //判断是否相邻
        bool IsNeighbor(Button btna,Button btnb)
        {
            int a = (int)btna.Tag;// btn.Tag = r * n + c;
            int b = (int)btnb.Tag;
            int r1 = a / n, c1 = a % n;
            int r2 = b / n, c2 = b % n;

            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1) || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))
                return true;
            return false;

        }
        //判断是否完成
        bool ResultIsOk()
        {
            for (int r = 0; r < n; r++)
                for (int c = 0; c < n; c++)
                {
                    if (buttons[r,c].Text !=(r*n+c+1).ToString())
                    {
                        return false;
                    }
                }
            return true;
        }
        //多次随机打乱按钮次序
        void shuffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = rnd.Next(n);
                int b = rnd.Next(n);
                int c = rnd.Next(n);
                int d = rnd.Next(n);
                swap(buttons[a, b], buttons[c, d]);
            }


        }

        void swap(Button btna ,Button btnb)
        {
            string t = btna.Text;
            btna.Text = btnb.Text;
            btnb.Text = t;

            bool v = btna.Visible;
            btna.Visible = btnb.Visible;
            btnb.Visible = v;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
