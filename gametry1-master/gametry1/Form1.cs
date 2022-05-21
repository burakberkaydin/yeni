using System;
using System.Collections.Generic;
using System.Drawing;

using System.Windows.Forms;

namespace gametry1
{
    public partial class Form1 : Form
    {
        private MyButton[,] btnGrid;
        private Color currColor;
        private int rows, cols;
        private int currRow, currCol;
        static Random rdn = new Random();
        private List<MovableObject> Objects { get; set; }
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
            RemoveMatches();
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
                btnGrid[r, c].Location = new Point(r * MyButton.Btn_size, c * MyButton.Btn_size);

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
        public bool RemoveMatches()
        {
            List<Line> lines = new List<Line>();
            //поиск горизонтальных линий
            MyButton[,]tmpMatrix =(MyButton[,]) btnGrid.Clone();

            for (int y = 0; y < 9; ++y)
                for (int x = 0; x < 9; ++x)
                {
                    
                    int count = 1;
                    for (int i = x + 1; i < 9; ++i)
                        if (tmpMatrix[y, i].BackColor == tmpMatrix[y, x].BackColor)
                            count++;
                        else
                            break;
                    if (count >= 3)
                    {
                        for (int i = x; i < x + count; ++i)
                            tmpMatrix[y, i].BackColor= Color.White;
                        lines.Add(new Line(new Index(x, y), new Index(x + count - 1, y)));
                    }
                }
            MyButton[,] tmpMatrix2 = (MyButton[,])btnGrid.Clone();
            for (int y = 0; y < 9; ++y)
                for (int x = 0; x < 9; ++x)
                {
                   
                    int count = 1;
                    for (int i = y + 1; i < 9; ++i)
                        if (tmpMatrix2[i, x].BackColor == tmpMatrix2[y, x].BackColor)
                            count++;
                        else
                            break;
                    if (count >= 3)
                    {
                        for (int i = y; i < y + count; ++i)
                            tmpMatrix2[i, x].BackColor = Color.White;
                        lines.Add(new Line(new Index(x, y), new Index(x, y + count - 1)));
                    }
                }
            if (lines.Count == 0)
                return false;

            int baseValue = 10;
            foreach (Line line in lines)
            {
                int count = 0;
                //горизонтальная линия
                if (line.Start.Y == line.Finish.Y)
                    for (int i = line.Start.X; i <= line.Finish.X; ++i)
                    {
                        btnGrid[line.Start.Y, i].BackColor = Color.White;
                        if (ElementRemoved != null)
                            ElementRemoved(i, line.Start.Y);
                        count++;
                    }
                //вертикальная линия
                else
                    for (int i = line.Start.Y; i <= line.Finish.Y; ++i)
                    {
                        btnGrid[i, line.Start.X].BackColor = Color.White;
                        if (ElementRemoved != null)
                            ElementRemoved(line.Start.X, i);
                        count++;
                    }
                int value = (count - 2) * baseValue;
              
            }
            if (MatchesRemoved != null)
                MatchesRemoved();
            return true;
        }
       
        public delegate void ElementRemoveHandler(int x, int y);
        public event ElementRemoveHandler ElementRemoved;
        public delegate void MatchesRemoveHandler();
        public event MatchesRemoveHandler MatchesRemoved;
        public delegate void ElementsFallHandler(List<Index> elements);
        public event ElementsFallHandler ElementsFalled;
    }
   
}

