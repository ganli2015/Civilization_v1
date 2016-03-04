using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace GameEntity
{
    public class GameDisplayImp1:GameDisplay
    {
        PictureBox _picturebox;

        public GameDisplayImp1(ref PictureBox picturebox)
        {
            _picturebox = picturebox;
        }

        public void DrawCircle(Color color, int x, int y, float diameter)
        {
            Graphics g = _picturebox.CreateGraphics();
            PointF circleLocation = CommonFunctions.ComputeCircleLocation(x, y);
            g.FillEllipse(new SolidBrush(color), circleLocation.X, circleLocation.Y, diameter, diameter);
        }

        public void DrawSquare(Color color, int x, int y, float sideLength)
        {
            Graphics g = _picturebox.CreateGraphics();
            PointF circleLocation = CommonFunctions.ComputeRectangleLocation(x, y);
            g.FillRectangle(new SolidBrush(color), circleLocation.X, circleLocation.Y, sideLength, sideLength);
        }

        public void ResetGrid(int x, int y)
        {
            Graphics g = _picturebox.CreateGraphics();
            RectangleF recf = new RectangleF(CommonFunctions.ComputeRectangleLocation(x, y), new SizeF(GlobalParameter.GridLength, GlobalParameter.GridLength));
            g.FillRectangle(new SolidBrush(SystemColors.Control), recf);
        }
    }
}
