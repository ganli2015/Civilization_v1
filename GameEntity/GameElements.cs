using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameEntity
{
    public interface GameElements
    {
        //not override
        void Add(Alive alive);
        void Add(Environ environment);

        //may override
        void Set(Alive alive);
        void Set(Environ environment);

        void RemoveAlive(Point p);
        void RemoveEnviron(Point p);

        bool GetAlive(int x, int y, out Alive alive);
        bool GetEnviron(int x, int y, out Environ environ);

        bool IsPossessed(Point p);
        bool IsAlivePossessed(Point p);
        bool IsEnvironPossessed(Point p);

        MessageSet Update();
        void Draw();
        void Paint();
        GameElements Clone();
        void Clear();
        void Save(string filename);

        List<Point> GetAliveIndexes();
    }
}
