using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameEntity
{
    public class ColorManager
    {
        List<Color> _colors;
        List<Color> _forbiddenColors;
        Color _defaultHumanColor;
        static ColorManager _instance;

        static public ColorManager GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = new ColorManager();
                return _instance;
            }
        }

        private ColorManager()
        {
            _colors = new List<Color>();
            _forbiddenColors = new List<Color>();

            Initialize();
        }

        private void Initialize()
        {
            _forbiddenColors.Add(GlobalParameter.PictureboxColor);
            _forbiddenColors.Add(GlobalParameter.HumanColor);
            _forbiddenColors.Add(GlobalParameter.GrassColor);
            _forbiddenColors.Add(GlobalParameter.WaterColor);
            _forbiddenColors.Add(GlobalParameter.DesertColor);
            _forbiddenColors.Add(GlobalParameter.ForestColor);
            _forbiddenColors.Add(GlobalParameter.MountainColor);

            List<Color> candidateColors = new List<Color>();

            candidateColors.Add(Color.Orange);
            candidateColors.Add(Color.Yellow);
            candidateColors.Add(Color.Brown);
            candidateColors.Add(Color.Bisque);
            candidateColors.Add(Color.DarkBlue);
            candidateColors.Add(Color.Coral);
            candidateColors.Add(Color.Crimson);
            candidateColors.Add(Color.Chocolate);
            candidateColors.Add(Color.Chartreuse);
            candidateColors.Add(Color.CadetBlue);
            candidateColors.Add(Color.BurlyWood);
            candidateColors.Add(Color.BlanchedAlmond);
            candidateColors.Add(Color.DarkKhaki);
            candidateColors.Add(Color.DarkGray);

            foreach (Color color in candidateColors)
            {
                if (!_forbiddenColors.Contains(color))
                {
                    _colors.Add(color);
                }
            }

            _defaultHumanColor = GlobalParameter.HumanColor;

        }

        public Color GetHumanColor(int civIndex)
        {
            if (civIndex >= _colors.Count || civIndex<0) return _defaultHumanColor;

            return _colors[civIndex];
        }

        Color CloneColor(Color color)
        {
            Color res = Color.FromArgb(color.ToArgb());
            return res;
        }
       
    }

    
}
