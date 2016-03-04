using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using GameEntity;

namespace CivilizationEntity
{
    public class Water:Environ
    {
        int _x, _y;
        Color _myColor;
        GameDisplay _gameDisplay;

        int _seafood;
        double _growthRate;

        public int Seafood
        {
            get { return _seafood; }
            set{_seafood=value;}
        }

        public double GrowthRate
        {
            get { return _growthRate; }
            set { _growthRate = value; }
        }

        public Water()
        {
            InitializeAttribute();
        }

         public Water(Point p)
        {
            _x = p.X;
            _y = p.Y;
            InitializeAttribute();
        }

         public Water(int i, int j)
        {
            _x = i;
            _y = j;
            InitializeAttribute();
        }

        void InitializeAttribute()
        {
            _myColor = GlobalParameter.WaterColor;

            _seafood = GameParameter.Water_Init_Seafood;
            _growthRate = GameParameter.Water_Init_GrowthRate;
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
            Color myColor =GlobalParameter.WaterColor;

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

            if (_seafood <= 100)
            {
                _seafood += 10;
            }
            else
            {
                _seafood += (int)(_seafood * _growthRate);
            }

            if (_seafood >= GameParameter.Water_Upperlimit_Food)
            {
                _seafood = GameParameter.Water_Upperlimit_Food;
            }

            return messageSet;
        }

        public Environ Clone()
        {
            Water environ = new Water();
            environ._x = _x;
            environ._y = _y;
            environ._gameDisplay = _gameDisplay;
            environ._seafood = _seafood;
            environ._growthRate = _growthRate;

            return environ;
        }

        public void Save(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Convert.ToInt32(Element.Water));
            bw.Write(_x);
            bw.Write(_y);
            bw.Write(_seafood);
            bw.Write(_growthRate);

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
            return Element.Water;
        }
    }
}
