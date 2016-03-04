using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEntity;

namespace CivilizationEntity
{
    public class AliveFactory
    {
        public AliveFactory()
        {

        }

        public Alive CreateAlive(Element symbol)
        {
            switch (symbol)
            {
                case Element.Human:
                    return new Human();
                default:
                    throw new Exception("symbol is error!");
            }
        }
    
        
    }
}
