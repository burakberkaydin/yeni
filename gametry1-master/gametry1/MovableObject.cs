using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gametry1
{
    class MovableObject
    {
        private double _x;
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;

            }
        }

        private double _y;
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;

            }
        }

        private System.Threading.Timer MoveTimer;

        private double DestinationX;
        private double DestinationY;

        public void MoveToPosition(double x, double y)
        {
            DestinationX = x;
            DestinationY = y;

            if (MoveTimer != null)
                MoveTimer.Dispose();

            MoveTimer = new System.Threading.Timer(o => MoveStep(), null, 0, 10);
        }

        private void MoveStep()
        {
            if (Math.Abs(X - DestinationX) > 5)
            {
                if (X < DestinationX)
                    X = X + 5;
                else if (X > DestinationX)
                    X = X - 5;
            }

            if (Math.Abs(Y - DestinationY) > 5)
            {
                if (Y < DestinationY)
                    Y = Y + 5;
                else if (Y > DestinationY)
                    Y = Y - 5;
            }
        }
    }  
}
