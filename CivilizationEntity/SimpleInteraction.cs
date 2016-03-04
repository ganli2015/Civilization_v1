using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEntity;

namespace CivilizationEntity
{
    public class SimpleInteraction
    {
        Alive _alive;
        Environ _environ;

        public SimpleInteraction(ref Alive alive, ref Environ environ)
        {
            _alive = alive;
            _environ = environ;
        }

        public void Perform()
        {
            if (_alive == null) return;

            Element alive_elem = _alive.GetElementType();

            switch (alive_elem)
            {
                case Element.Human:
                    {
                        Human human=_alive as Human;
                        Interaction_HumanAndEnviron(ref human, ref _environ);
                        break;
                    }
            }
        }

        void Interaction_HumanAndEnviron(ref Human human, ref Environ environ)
        {
            if (environ == null)
            {
                Interaction_HumanAndNone(ref human);
                return;
            }

            Element environ_elem = environ.GetElementType();

            switch (environ_elem)
            {
                case Element.Grass:
                    {
                        Grass grass = environ as Grass;
                        Interaction_HumanAndGrass(ref human, ref grass);
                        break;
                    }
                case Element.Water:
                    {
                        Water water = environ as Water;
                        Interaction_HumanAndWater(ref human, ref water);
                        break;
                    }
                case Element.Desert:
                    {
                        Desert desert = environ as Desert;
                        Interaction_HumanAndDesert(ref human, ref desert);
                        break;
                    }
                case Element.Mountain:
                    {
                        Mountain mountain = environ as Mountain;
                        Interaction_HumanAndMountain(ref human, ref mountain);
                        break;
                    }
                case Element.Ice:
                    {
                        Ice ice = environ as Ice;
                        Interaction_HumanAndIce(ref human, ref ice);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        void Interaction_HumanAndNone(ref Human human)
        {
            if (human.Environ == Element.None)
            {
                HumanSlowDie(human);
            }
        }

        void Interaction_HumanAndGrass(ref Human human, ref Grass grass)
        {
            if (human.GetLocationIndex() == grass.GetLocationIndex())//if human and grass are on the same tile
            {
                if (grass.Food < 100)//if food is not enough
                {
                    human.Population -= (int)(human.Population * GameParameter.Human_LackFoodDeathRate);
                }
                else
                {
                    if (grass.Food - human.Population < GameParameter.Grass_Lowerlimit_Food)//if food is not enough
                    {
                        int delta_food = grass.Food - GameParameter.Grass_Lowerlimit_Food;
                        grass.Food = GameParameter.Grass_Lowerlimit_Food;
                        human.Population += (int)(grass.Food * GameParameter.Human_GrowthRate_Grass);
                        int deathPop = (int)((human.Population - delta_food) * GameParameter.Human_LackFoodDeathRate);
                        if (deathPop < 50)
                            deathPop = 50;
                        human.Population -= deathPop;
                    }
                    else
                    {
                        int delta_food = human.Population;
                        grass.Food -= delta_food;
                        human.Population += (int)(grass.Food * GameParameter.Human_GrowthRate_Grass);
                    }
                }
            }
        }

        void Interaction_HumanAndWater(ref Human human, ref Water water)
        {
            if (water.Seafood <= GameParameter.Water_LowerLimit_Food) return;

            if (human.Environ == Element.Grass)
            {
                int delta_food = water.Seafood / 10;
                water.Seafood -= delta_food;
                human.Population += delta_food;
            }

            if (human.GetLocationIndex() == water.GetLocationIndex())
            {
                human.Population -= 10;
            }

            if (human.ToVoyage < 0 && human.Environ!=Element.Water)
            {
                Random ran=new Random();
                human.ToVoyage = ran.Next(0, GameParameter.Human_GoVoyage);
            }
        }

        void Interaction_HumanAndDesert(ref Human human, ref Desert desert)
        {
            if (human.GetLocationIndex()==desert.GetLocationIndex())
            {
                HumanSlowDie(human);
            }
        }

        void Interaction_HumanAndMountain(ref Human human, ref Mountain mountain)
        {
            if (human.GetLocationIndex() == mountain.GetLocationIndex())
            {
                HumanSlowDie(human);
            }
        }

        void Interaction_HumanAndIce(ref Human human, ref Ice ice)
        {
            if (human.GetLocationIndex() == ice.GetLocationIndex())
            {
                HumanSlowDie(human);
            }
        }

        void HumanSlowDie(Human human)
        {
            if (human.Population == 1)
            {
                human.Population = 0;
            }
            else
                human.Population -= human.Population / 2;
        }
    }
}
