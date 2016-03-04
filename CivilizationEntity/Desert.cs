using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEntity;
using System.Drawing;
using System.IO;

namespace CivilizationEntity
{
    public class Desert:Environ
    {
        int _x, _y;
        Color _myColor;
        GameDisplay _gameDisplay;

        public Desert()
        {
            InitializeAttribute();
        }

         public Desert(Point p)
        {
            _x = p.X;
            _y = p.Y;
            InitializeAttribute();
        }

         public Desert(int i, int j)
        {
            _x = i;
            _y = j;
            InitializeAttribute();
        }

        void InitializeAttribute()
        {
            _myColor = GlobalParameter.DesertColor;
        }

        public Point GetLocationIndex()
        {
            return new Point(_x, _y);
        }

        public void SetLocationIndex(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void SetPictureBox(ref GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
        }


        public void Draw()
        {
            Color myColor =GlobalParameter.DesertColor;

            if (_myColor != myColor)
            {
                _gameDisplay.DrawSquare(myColor, _x, _y, GlobalParameter.GridLength);
                _myColor = myColor;
            }
            
            
        }

        public void Paint()
        {
            _gameDisplay.DrawSquare(_myColor, _x, _y, GlobalParameter.GridLength);

        }

        public MessageSet Update()
        {
            MessageSet messageSet = new MessageSet();

            return messageSet;
        }

        public Environ Clone()
        {
            Desert environ = new Desert();
            environ._x = _x;
            environ._y = _y;
            environ._gameDisplay = _gameDisplay;

            return environ;
        }

        public void Save(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Convert.ToInt32(Element.Desert));
            bw.Write(_x);
            bw.Write(_y);

            bw.Flush();
            bw.Close();
            fs.Close();
        }

        public Color GetColor()
        {
            return _myColor;
        }

        public Element GetElementType()
        {
            return Element.Desert;
        }
    }
}
