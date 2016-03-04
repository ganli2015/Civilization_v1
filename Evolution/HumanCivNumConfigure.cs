using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GameEntity;
using CivilizationEntity;

namespace Evolution
{
    public class HumanCivNumConfigure
    {
        struct potentialCiv_attribute
        {
            public Point markPnt;
            public int potentialToCiv;
        }

        List<civ_attribute> _civs;
        List<potentialCiv_attribute> _potentialCivs;

        public HumanCivNumConfigure()
        {
            _civs = new List<civ_attribute>();
            _potentialCivs = new List<potentialCiv_attribute>();
        }

        public void UpdateCivs(ref List<civ_attribute> civs)
        {
            _civs = civs;
        }

        public void ComputeHumanCiviDistribution(ref GameElements gameElements)
        {
            List<Point> aliveIndexes = gameElements.GetAliveIndexes();
            List<List<Point>> potentialCivRegions = new List<List<Point>>();
            ComputePotentialNewCivs(aliveIndexes, ref potentialCivRegions);

            _potentialCivs = AdjustPotentialCivAttribute(ref potentialCivRegions, ref gameElements);

            CheckNewCivCreation(ref aliveIndexes, ref gameElements);
        }

        void CheckNewCivCreation(ref List<Point> aliveIndexes,ref GameElements gameElements)
        {
            List<potentialCiv_attribute> ToRemove = new List<potentialCiv_attribute>();
            foreach (potentialCiv_attribute p_a in _potentialCivs)
            {
                if (p_a.potentialToCiv >= GameParameter.Value_ConvertToNewCiv)
                {
                    List<Point> region=new List<Point>();
                    CommonFunctions.ExtractSeparatedSubSet(ref aliveIndexes, p_a.markPnt, ref region);
                    SetCivIndex(region, GameParameter.CivNum, ref gameElements);

                    civ_attribute c_a;
                    c_a.markPoint = p_a.markPnt;
                    c_a.index = GameParameter.CivNum;
                    ++GameParameter.CivNum;
                    _civs.Add(c_a);
                    ToRemove.Add(p_a);
                }
            }

            foreach (potentialCiv_attribute remove in ToRemove)
            {
                _potentialCivs.Remove(remove);
            }
        }

        List<potentialCiv_attribute> AdjustPotentialCivAttribute(ref List<List<Point>> potentialCivRegions, ref GameElements gameElements)
        {
            List<potentialCiv_attribute> res=new List<potentialCiv_attribute>();

//             for (int i = 0; i < _potentialCivs.Count; ++i)
//             {
//                 foreach (List<Point> region in potentialCivRegions)
//                 {
//                     if (region.Contains(_potentialCivs[i].markPnt))
//                     {
//                         potentialCiv_attribute p_a;
//                         p_a.markPnt = ComputePointOfHighestPopulation(region, ref gameElements);
//                         p_a.index = _potentialCivs[i].index;
//                         p_a.potentialToCiv = _potentialCivs[i].potentialToCiv + 1;
//                         res.Add(p_a);
//                         break;
//                     }
//                 }
//             }


            foreach (List<Point> region in potentialCivRegions)
            {
                //two new potential civs may merge to one,than they both point to the same region,
                //then select the one who has higher <potentialToCiv>
                List<potentialCiv_attribute> p_a_inRegion = new List<potentialCiv_attribute>();
                bool has_p_a = false;
                foreach (potentialCiv_attribute p_a in _potentialCivs)
                {
                    if (region.Contains(p_a.markPnt))
                    {
                        potentialCiv_attribute p_a_new;
                        p_a_new.markPnt = ComputePointOfHighestPopulation(region, ref gameElements);
                        p_a_new.potentialToCiv = p_a.potentialToCiv + 1;
                        p_a_inRegion.Add(p_a_new);
                        has_p_a = true;
                    }
                }

                if (has_p_a)
                {
                    potentialCiv_attribute desired_p_a=new potentialCiv_attribute();
                    int max_potentialToCiv = -1;
                    foreach (potentialCiv_attribute p_a in p_a_inRegion)
                    {
                        if (p_a.potentialToCiv > max_potentialToCiv)
                        {
                            max_potentialToCiv = p_a.potentialToCiv;
                            desired_p_a = p_a;
                        }
                    }
                    res.Add(desired_p_a);
                }
                else
                {
                    potentialCiv_attribute p_a_new;
                    p_a_new.markPnt = ComputePointOfHighestPopulation(region, ref gameElements);
                    p_a_new.potentialToCiv = 0;
                    res.Add(p_a_new);
                }
                
                
            }

            return res;
        }

        Point ComputePointOfHighestPopulation(List<Point> region, ref GameElements gameElements)
        {
            Point res=new Point();
            double highestPop=-1.0;
            foreach (Point p in region)
            {
                Alive alive;
                if (gameElements.GetAlive(p.X, p.Y, out alive))
                {
                    Human human = alive as Human;
                    if (human!=null)
                    {
                        if(human.Population>highestPop)
                        {
                            highestPop=human.Population;
                            res=p;
                        }
                    }
                }
            }

            return res;
        }

        void ComputePotentialNewCivs(List<Point> inputRegion, ref List<List<Point>> potentialNewRegions)
        {
            List<Point> region = new List<Point>(inputRegion.ToArray());
            while (region.Count != 0)
            {
                List<Point> sub = new List<Point>();
                CommonFunctions.ExtractSeparatedSubSet(ref region, region[0], ref sub);

                bool isCiv = false;
                foreach (civ_attribute c_a in _civs)
                {
                    if (sub.Contains(c_a.markPoint))
                    {
                        isCiv = true;
                        break;
                    }
                }
                if (!isCiv)
                {
                    potentialNewRegions.Add(sub);
                }

            }
        }

        void SetCivIndex(List<Point> region, int index, ref GameElements gameElements)
        {
            foreach (Point p in region)
            {
                Alive alive;
                if (gameElements.GetAlive(p.X, p.Y, out alive))
                {
                    Human human = alive as Human;
                    if (human != null)
                    {
                        human.SetCivIndex(index);
                        gameElements.Set(human);
                    }
                }
            }
        }
    }
}
