using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEntity;
using System.Drawing;
using System.Windows.Forms;

namespace CivilizationEntity
{
    public class CommonFunctions
    {
        static public PointF ComputeCircleLocation(int x,int y)
        {
            float px = (float)(x * GlobalParameter.GridLength);
            float py = (float)(y * GlobalParameter.GridLength);
            return new PointF(px, py);
        }

        static public PointF ComputeRectangleLocation(int x, int y)
        {
            float px = (float)(x * GlobalParameter.GridLength);
            float py = (float)(y * GlobalParameter.GridLength);
            return new PointF(px, py);
        }

        static public List<Point> GetNeighbourIndex(int x, int y)
        {
            List<Point> neighbour = new List<Point>();
            if (x != 0)
            {
                neighbour.Add(new Point(x - 1, y));
            }
            if (x != (GlobalParameter.grid_num_x - 1))
            {
                neighbour.Add(new Point(x + 1, y));
            }
            if (y != 0)
            {
                neighbour.Add(new Point(x, y - 1));
            }
            if (y != (GlobalParameter.grid_num_y - 1))
            {
                neighbour.Add(new Point(x, y + 1));
            }
            if (x != 0 && y != 0)
            {
                neighbour.Add(new Point(x - 1, y - 1));
            }
            if (x != 0 && y != (GlobalParameter.grid_num_y - 1))
            {
                neighbour.Add(new Point(x - 1, y + 1));
            }
            if (x != (GlobalParameter.grid_num_x - 1) && y != 0)
            {
                neighbour.Add(new Point(x + 1, y - 1));
            }
            if (x != (GlobalParameter.grid_num_x - 1) && y != (GlobalParameter.grid_num_y - 1))
            {
                neighbour.Add(new Point(x + 1, y + 1));
            }

            return neighbour;
        }

        static public List<Point> GetNeighbourIndex(Point p)
        {
            return GetNeighbourIndex(p.X, p.Y);
        }

        static public void ResetGrid(Point index,ref PictureBox picuturebox)
        {
            Graphics g = picuturebox.CreateGraphics();
            RectangleF recf = new RectangleF(CommonFunctions.ComputeRectangleLocation(index.X, index.Y), new SizeF(GlobalParameter.GridLength, GlobalParameter.GridLength));
            g.FillRectangle(new SolidBrush(SystemColors.Control), recf);
        }

        static public void ExtractSeparatedSubSet(ref List<Point> sample, Point refPnt, ref List<Point> sub)
        {
            if (sample.Count == 0) return;

            List<Point> neighbour = CommonFunctions.GetNeighbourIndex(refPnt.X, refPnt.Y);
            sub.Add(refPnt);
            sample.Remove(refPnt);
            foreach (Point p in neighbour)
            {
                if (sample.Contains(p))
                    ExtractSeparatedSubSet(ref sample, p, ref sub);
            }
        }

        static public Point RandonPickFromPnts(List<Point> pnts)
        {
            Point p = new Point(-1,-1);
            if(pnts.Count==0) return p;

            int count = pnts.Count;
            Random rand = new Random();
            int myRand = rand.Next(0, count - 1);

            return pnts[myRand];
        }

        static public List<Point> GetCircleArea(Point center, int radius)
        {
            List<Point> res = new List<Point>();
            if (radius == 0) return res;

            Point foot_lefttop = new Point(center.X - radius+1, center.Y - radius+1);
            List<Point> squarePnts = GetSquareArea(foot_lefttop, radius*2-1);
            foreach (Point p in squarePnts)
            {
                int squareDis = 0;
                if((p.X - center.X)==0)
                {
                    squareDis+=0;
                }
                else
                {
                    squareDis+=(Math.Abs(p.X - center.X)+1) * (Math.Abs(p.X - center.X)+1);
                }
                if ((p.Y - center.Y) == 0)
                {
                    squareDis += 0;
                }
                else
                {
                    squareDis += (Math.Abs(p.Y - center.Y) + 1) * (Math.Abs(p.Y - center.Y) + 1);
                }

                if (squareDis <= radius * radius)
                {
                    res.Add(p);
                }
            }

            return res;
        }

        static public List<Point> GetSquareArea(Point leftTopPnt, int sideLength)
        {
            List<Point> res = new List<Point>();

            for (int i = leftTopPnt.X; i < leftTopPnt.X + sideLength;++i )
            {
                for (int j = leftTopPnt.Y; j < leftTopPnt.Y + sideLength; ++j)
                {
                    if(i>=0 && i<GlobalParameter.grid_num_x
                        && j >= 0 && j < GlobalParameter.grid_num_y)
                    {
                        res.Add(new Point(i, j));
                    }

                }
            }

            return res;
        }
    }
}
