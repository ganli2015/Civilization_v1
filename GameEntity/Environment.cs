using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GameEntity
{
    public interface Environ
    {
        Point GetLocationIndex();
        void SetLocationIndex(int x, int y);
        void SetPictureBox(ref GameDisplay gameDisplay);
        void Draw();
        void Paint();
        MessageSet Update();
        Environ Clone();

        void Save(string filename);

        Color GetColor();
        Element GetElementType();
    }
}
