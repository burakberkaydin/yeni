using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _5
{
    public partial class Form1 : Form
    {
        private MyButton[,] btnGrid;
        private Color currColor;
        private int rows, cols;
        private int currRow, currCol;
        static Random rdn = new Random();
        
        public Form1()
        {
            InitializeComponent();
            gamepanel.Height = MyButton.Btn_size * 9;
            gamepanel.Width = MyButton.Btn_size * 9;
            populateGrid();
            assignColor();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        public void populateGrid()
        {


            cols = gamepanel.Height / MyButton.Btn_size;
            rows = gamepanel.Width / MyButton.Btn_size;



            btnGrid = new MyButton[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    btnGrid[r, c] = new MyButton();
                    btnGrid[r, c].row = r;
                    btnGrid[r, c].col = c;

                    btnGrid[r, c].Click += btnWhite_Click;
                    gamepanel.Controls.Add(btnGrid[r, c]);
                    btnGrid[r, c].Location = new Point(r * MyButton.Btn_size, c * MyButton.Btn_size);
                }
            }

        }


        public void assignColor()
        {


            Color[] colors = { Color.Blue, Color.Yellow, Color.Purple };


            cols = gamepanel.Height / MyButton.Btn_size;
            rows = gamepanel.Width / MyButton.Btn_size;




            int n = 3;

            while (n > 0)
            {
                int r = rdn.Next() % rows;
                int c = rdn.Next() % cols;
                if (btnGrid[r, c].BackColor == Color.White)
                {
                    btnGrid[r, c].row = r;
                    btnGrid[r, c].col = c;
                    btnGrid[r, c].BackColor = colors[rdn.Next() % colors.Length];

                    gamepanel.Controls.Add(btnGrid[r, c]);
                    btnGrid[r, c].Click -= btnWhite_Click;
                    btnGrid[r, c].Click += takeLocation;
                    btnGrid[r, c].Location = new Point(r * MyButton.Btn_size, c * MyButton.Btn_size);
                    n--;
                }
                else
                {
                    continue;
                }

            }






        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            try
            {
                Button cb = (sender as Button);

                int c = currCol;
                int r = currRow;


                btnGrid[currRow, currCol].BackColor = Color.White;
                btnGrid[r, c] = new MyButton();
                btnGrid[r, c].row = r;
                btnGrid[r, c].col = c;

                btnGrid[r, c].Click += btnWhite_Click;
                gamepanel.Controls.Add(btnGrid[r, c]);
                btnGrid[r, c].Location = new Point(r * MyButton.Btn_size, c * MyButton.Btn_size);

                ClickToMove(cb.Location.X, cb.Location.Y, cb);

                c = cb.Location.Y / MyButton.Btn_size;
                r = cb.Location.X / MyButton.Btn_size;

                btnGrid[r, c].row = r;
                btnGrid[r, c].col = c;
                btnGrid[r, c].BackColor = currColor;

                gamepanel.Controls.Add(btnGrid[r, c]);
                btnGrid[r, c].Click -= btnWhite_Click;
                btnGrid[r, c].Click += takeLocation;

                for (int i = 0; i < r; i++)
                {
                    for (int j = 0; j < c; j++)
                    {
                        btnGrid[r, c].Location = new Point(r * MyButton.Btn_size, c * MyButton.Btn_size);
                    }
                }
               

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                assignColor();
            }


        }

        private void gamepanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void gamepanel_Paint_1(object sender, PaintEventArgs e)
        {

        }

        public void takeLocation(object sender, EventArgs e)
        {
            Button cb = (sender as Button);
            currCol = cb.Location.Y / MyButton.Btn_size;
            currRow = cb.Location.X / MyButton.Btn_size;
            currColor = cb.BackColor;



        }

        public void ClickToMove(int x, int y, Button cb)
        {



        }

    }
}
