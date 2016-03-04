using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using GameEntity;

namespace CivilizationEntity
{
    public class Forest:Environ
    {
        int _x, _y;
        Color _myColor;
        GameDisplay _gameDisplay;

        public Forest()
        {
            InitializeAttribute();
        }

         public Forest(Point p)
        {
            _x = p.X;
            _y = p.Y;
            InitializeAttribute();
        }

         public Forest(int i, int j)
        {
            _x = i;
            _y = j;
            InitializeAttribute();
        }

        void InitializeAttribute()
        {
            _myColor = GlobalParameter.ForestColor;
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
            Color myColor =GlobalParameter.ForestColor;

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
            Forest environ = new Forest();
            environ._x = _x;
            environ._y = _y;
            environ._gameDisplay = _gameDisplay;

            return environ;
        }

        public void Save(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Convert.ToInt32(Element.Forest));
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
            return Element.Forest;
        }
    }
}
