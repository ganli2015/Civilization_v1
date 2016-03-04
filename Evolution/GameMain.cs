using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GameEntity;
using CivilizationEntity;
using Rules;
using System.Windows.Forms;

namespace Evolution
{
    public struct civ_attribute
    {
        public Point markPoint;
        public int index;
    }

    public class GameMain
    {
        GameDisplay _gameDisplay;
        List<civ_attribute> _civs;
        HumanCivNumConfigure _humanCivNumConfigure;

        public GameMain(ref GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
            _civs = new List<civ_attribute>();
            _humanCivNumConfigure = new HumanCivNumConfigure();
        }

        public void Run(ref GameElements gameEntity)
        {
            _humanCivNumConfigure.UpdateCivs(ref _civs);
            _humanCivNumConfigure.ComputeHumanCiviDistribution(ref gameEntity);

            MessageSet messageSet = gameEntity.Update();
            DealWithMessages(messageSet, ref gameEntity);
            
        }

        void DealWithMessages(MessageSet messageSet,ref GameElements gameEntity)
        {
            List<GameMessage> messages = messageSet.GetMessages();
            Rules_Human rules_human=new Rules_Human(ref gameEntity,ref _gameDisplay);
            foreach (GameMessage message in messages)
            {
                switch (message.GetMessageType())
                {
                    case ActionMessage.HighDensityPopulation:
                        {
                            rules_human.ImmigrateToNeighbourhood(message.GetLocation());
                            break;
                        }
                    case ActionMessage.ExtremeHighDensityPopulation:
                        {
                            rules_human.DecreaseGrowthRate(message.GetLocation());
                            break;
                        }
                    case ActionMessage.GoVoyage:
                        {
                            rules_human.MoveFromLandToWater(message.GetLocation());
                            break;
                        }
                    case ActionMessage.OnVoyage:
                        {
                            rules_human.MoveOnWater(message.GetLocation());
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        bool CheckReDraw(MessageSet messageSet)
        {
            List<GameMessage> messages = messageSet.GetMessages();
            foreach (GameMessage message in messages)
            {
//                 if (message.GetMessageType() == ActionMessage.AllDead)
//                 {
//                     return true;
//                 }
                if (message.GetMessageType() == ActionMessage.HighDensityPopulation)
                {
                    return true;
                }
            }

            return false;
        }

        public void InitializeHumanCiviDistribution(ref GameElements gameElements)
        {
            List<Point> aliveIndexes = gameElements.GetAliveIndexes();

            List<List<Point>> subRegions = new List<List<Point>>();
            while (aliveIndexes.Count != 0)
            {
                List<Point> sub = new List<Point>();
                CommonFunctions.ExtractSeparatedSubSet(ref aliveIndexes, aliveIndexes[0], ref sub);
                subRegions.Add(sub);
            }

            for (int i = 0; i < subRegions.Count;++i )
            {
                civ_attribute c_a;
                c_a.markPoint = subRegions[i][0];
                c_a.index = GameParameter.CivNum++;
                _civs.Add(c_a);

                foreach (Point p in subRegions[i])
                {
                    Alive human;
                    gameElements.GetAlive(p.X, p.Y, out human);
                    human.SetCivIndex(c_a.index);
                }
            }

//             civ_attribute c_a;
//             c_a.markPoint = subRegions[0][0];
//             c_a.index = GameParameter.CivNum++;
//             _civs.Add(c_a);
// 
//             foreach (Point p in subRegions[0])
//             {
//                 Alive human;
//                 gameElements.GetAlive(p.X, p.Y, out human);
//                 human.SetCivIndex(c_a.index);
//             }
        }


        

        
    }

    
}
