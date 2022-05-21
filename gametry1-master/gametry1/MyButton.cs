using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gametry1
{
    class MyButton:Button
    {
        public static Color cuurColors = Color.White;


        public static int Btn_size = 50;
            
        public int row { get; set; }
        public int col { get; set; }

        public MyButton()
        {
            Width = Height = Btn_size;
            
            this.BackColor = cuurColors;
           
        }

        public static implicit operator MyButton(int v) => throw new NotImplementedException();
    }
}
