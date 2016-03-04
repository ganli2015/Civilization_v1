using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GameEntity
{
    public interface TileEntity
    {
        void Add(Alive alive);
        void Set(Alive alive);
        void Add(Environ environment);
        void Set(Environ environment);

        void RemoveAlive(Point p);
        void RemoveEnviron(Point p);

        bool GetEnviron(int x, int y, out Environ environ);
        bool GetAlive(int x, int y, out Alive alive);
        bool IsPossessed(Point p);
        bool IsAlivePossessed(Point p);
        bool IsEnvironPossessed(Point p);
        MessageSet Update();
        TileEntity Clone();
        void Draw();
        void Paint();
        void Clear();
        void SetPictureBox(ref GameDisplay gameDisplay);
        void Save(string filename);

        List<Point> GetAliveIndexes();
    }
}
