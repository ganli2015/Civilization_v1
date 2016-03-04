using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GameEntity;

namespace CivilizationEntity
{
    public class Message_HighDensityPopulation:GameMessage
    {
        public Message_HighDensityPopulation(int x,int y):base(x,y)
        {
            
        }

        override public ActionMessage GetMessageType()
        {
            return ActionMessage.HighDensityPopulation;
        }
    }

    public class Message_ExtremeHighDensityPopulation : GameMessage
    {

        public Message_ExtremeHighDensityPopulation(int x, int y)
            : base(x, y)
        {
            
        }

        override public ActionMessage GetMessageType()
        {
            return ActionMessage.ExtremeHighDensityPopulation; ;
        }
    }

    public class Message_AllDead : GameMessage
    {

        public Message_AllDead(int x, int y)
            : base(x, y)
        {
            
        }

        override public ActionMessage GetMessageType()
        {
            return ActionMessage.AllDead;
        }

    }

    public class Message_Govoyage : GameMessage
    {

        public Message_Govoyage(int x, int y)
            : base(x, y)
        {
            
        }

        override public ActionMessage GetMessageType()
        {
            return ActionMessage.GoVoyage;
        }

    }

    public class Message_OnVoyage : GameMessage
    {

        public Message_OnVoyage(int x, int y)
            : base(x, y)
        {
            
        }

        override public ActionMessage GetMessageType()
        {
            return ActionMessage.OnVoyage;
        }
    }

}
