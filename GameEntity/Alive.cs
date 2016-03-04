using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GameEntity
{
    public interface Alive
    {
        Point GetLocationIndex();
        void SetLocationIndex(int x, int y);

        void Draw();//conditional
        void Paint();//non-conditional
        MessageSet Update();
        Alive Clone();
        void SetPictureBox(ref GameDisplay gameDisplay);
        Color GetColor();
        Element GetElementType();

        void Save(string filename);

        void SetCivIndex(int i);
        int GetCivIndex();
        Element GetEnviron();
        void SetEnviron(Element environ);
    }
}
