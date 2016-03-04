using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameEntity
{
    public interface GameDisplay
    {
        void DrawCircle(Color color, int x, int y, float diameter);
        void DrawSquare(Color color, int x, int y, float sideLength);
        void ResetGrid(int x, int y);
    }
}
