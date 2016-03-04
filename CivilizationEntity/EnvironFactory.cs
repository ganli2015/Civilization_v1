using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEntity;

namespace CivilizationEntity
{
    public class EnvironFactory
    {
        public EnvironFactory()
        {

        }

        public Environ CreateEnviron(Element symbol)
        {
            switch (symbol)
            {
                case Element.Grass:
                    return new Grass();
                case Element.Water:
                    return new Water();
                case Element.Desert:
                    return new Desert();
                case Element.Forest:
                    return new Forest();
                case Element.Mountain:
                    return new Mountain();
                case Element.Ice:
                    return new Ice();
                default:
                    throw new Exception("symbol is error!");
            }
        }
    }
}
