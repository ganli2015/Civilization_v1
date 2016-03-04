using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameEntity
{
    public enum ActionMessage
    {
        None,
        AllDead,
        HighDensityPopulation,
        ExtremeHighDensityPopulation,
        GoVoyage,
        OnVoyage
    }

    public abstract class GameMessage
    {
        protected Point _index;

        protected GameMessage(int x, int y)
        {
            _index = new Point(x, y);
        }


        public abstract ActionMessage GetMessageType();
        public Point GetLocation()
        {
            return _index;
        }
    }
}
