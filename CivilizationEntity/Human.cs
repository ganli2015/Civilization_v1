using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using GameEntity;
using CivilizationEntity.CivilizationProperty;

namespace CivilizationEntity
{
    public class Human:Alive
    {
        int _x, _y;
        Color _myColor;
        GameDisplay _gameDisplay;

        int _population;
        int _civIndex;
        int _toVoyage;
        Element _environ;

        Agriculture _agriculture;
        Culture _culture;
        Industry _industry;
        Military _military;
        Technology _technology;

        public int Population
        {
            get { return _population; }
            set { _population = value; }
        }

        public int CivIndex
        {
            get { return _civIndex; }
            set { _civIndex = value; }
        }

        public int ToVoyage
        {
            get { return _toVoyage; }
            set { _toVoyage = value; }
        }

        public Element Environ
        {
            get { return _environ; }
            set { _environ = value; }
        }

        public double Agriculture
        {
            get { return _agriculture.Value; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                _agriculture.Value = value; 
            }
        }

        public double Industry
        {
            get { return _industry.Value; }
            set 
            {
                if (value < 0)
                {
                    value = 0;
                }
                _industry.Value = value; 
            }
        }

        public double Culture
        {
            get { return _culture.Value; }
            set 
            {
                if (value < 0)
                {
                    value = 0;
                }
                _culture.Value = value; 
            }
        }

        public double Military
        {
            get { return _military.Value; }
            set 
            {
                if (value < 0)
                {
                    value = 0;
                }
                _military.Value = value;
            }
        }

        public double Technology
        {
            get { return _technology.Value; }
            set 
            {
                if (value < 0)
                {
                    value = 0;
                }
                _technology.Value = value;
            }
        }

        public Human()
        {
            InitializeAttribute();
        }

        public Human(Point p)
        {
            _x = p.X;
            _y = p.Y;
            InitializeAttribute();
        }

        public Human(int i,int j)
        {
            _x = i;
            _y = j;
            InitializeAttribute();
        }

        void InitializeAttribute()
        {
            _myColor = GlobalParameter.HumanColor;

            _population = GameParameter.Human_Init_Population;
            _civIndex = -1;
            _toVoyage = -1;
            _environ = Element.None;

            Random ran = new Random();
            _agriculture = new Agriculture(ran.Next(0, 10));
            _culture = new Culture(ran.Next(0, 10));
            _industry = new Industry(ran.Next(0, 10));
            _military = new Military(ran.Next(0, 10));
            _technology = new Technology(ran.Next(0, 10));
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

        public Element GetEnviron()
        {
            return _environ;
        }

        public void SetEnviron(Element environ)
        {
            _environ = environ;
        }

        public void Draw()
        {
            Color myColor=GenerateColor(_population);
            

            if (_myColor != myColor)
            {
                _gameDisplay.DrawCircle(myColor, _x, _y, GlobalParameter.GridLength);
                _myColor = myColor;
            }
            
        }

        public void Paint()
        {
            _gameDisplay.DrawCircle(_myColor, _x, _y, GlobalParameter.GridLength);
        }

        public MessageSet Update()
        {
            MessageSet messageset = new MessageSet();

            if (_population <= 0)
            {
                messageset.Add(new Message_AllDead(_x, _y));
                return messageset;
            }

            if (_population >= GameParameter.Human_Immigrate_Population && _environ!=Element.Water)
            {
                messageset.Add(new Message_HighDensityPopulation(_x, _y));
            }

            if (_toVoyage >= 0)
            {
                _toVoyage++;
            }

            if (_toVoyage >= GameParameter.Human_GoVoyage && _population>=GameParameter.Human_VoyagePopulation)
            {
                messageset.Add(new Message_Govoyage(_x, _y));
            }

            if (_environ == Element.Water)
            {
                messageset.Add(new Message_OnVoyage(_x, _y));
            }

            return messageset;
        }

        public void SetPictureBox(ref GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
        }

        public Alive Clone()
        {
            Human clone = new Human();
            clone._x = _x;
            clone._y = _y;
            clone._population = _population;
            clone._gameDisplay = _gameDisplay;
            clone._myColor = _myColor;
            clone._civIndex = _civIndex;
            clone._toVoyage = _toVoyage;
            clone._environ = _environ;

            clone.Agriculture = Agriculture;
            clone.Culture = Culture;
            clone.Industry = Industry;
            clone.Military = Military;
            clone.Technology = Technology;

            return clone;
        }

        public void Save(string filename)
        {
            FileStream fs = new FileStream(filename,FileMode.Append);
            BinaryWriter bw = new BinaryWriter(fs);

            bw.Write(Convert.ToInt32(Element.Human));
            bw.Write(_x);
            bw.Write(_y);
            bw.Write(_population);
            bw.Write(_civIndex);
            bw.Write(_toVoyage);
            bw.Write(Convert.ToInt32(_environ));

            bw.Write(_agriculture.Value);
            bw.Write(_culture.Value);
            bw.Write(_industry.Value);
            bw.Write(_military.Value);
            bw.Write(_technology.Value);

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
            return Element.Human;
        }

        public void SetCivIndex(int i)
        {
            _civIndex = i;
        }

        public int GetCivIndex()
        {
            return _civIndex;
        }

        private Color GenerateColor(double population)
        {
//             Color myColor;
//             if (_population < 50)
//             {
//                 myColor = Color.Yellow;
//             }
//             else if (_population >= 50 && _population < 500)
//             {
//                 myColor = Color.Orange;
//             }
//             else if (_population >= 500 && _population < 1000)
//             {
//                 myColor = Color.Red;
//             }
//             else
//             {
//                 myColor = Color.Brown;
//             }
// 
//             return myColor;

            ColorManager cm = ColorManager.GetInstance();

            Color myColor=cm.GetHumanColor(_civIndex);
            

            return myColor;

        }



    }
}
