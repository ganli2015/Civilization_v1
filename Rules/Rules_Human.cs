using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GameEntity;
using CivilizationEntity;

namespace Rules
{
    public class Rules_Human
    {
        double _immigrateRate=GameParameter.Human_ImmigrateRate;

        GameDisplay _gameDisplay;
        GameElements _gameEntity;

        public Rules_Human(ref GameElements gameEntity, ref GameDisplay gameDisplay)
        {
            _gameEntity = gameEntity;
            _gameDisplay = gameDisplay;
        }

        public  void ImmigrateToNeighbourhood(Point p)
        {
            Alive alive_center;
            if (!_gameEntity.GetAlive(p.X, p.Y, out alive_center))
            {
                throw new Exception("p is not the desired index!");
            }
            Human center = alive_center as Human;
            if (center == null)
            {
                throw new Exception("p must point to human!");
            }

            List<Point> neighbour = CommonFunctions.GetNeighbourIndex(p);
            foreach (Point neighbourIndex in neighbour)
            {
                if (!_gameEntity.IsEnvironPossessed(neighbourIndex)) continue;

                //Do not immigrate to Water or Desert or Mountain or Ice!
                Environ environ;
                _gameEntity.GetEnviron(neighbourIndex.X,neighbourIndex.Y,out environ);
                if (environ.GetElementType() == Element.Water 
                    || environ.GetElementType() == Element.Desert 
                    || environ.GetElementType() == Element.Mountain
                    || environ.GetElementType()==Element.Ice) continue;

                int immigratePop = (int)(center.Population * _immigrateRate);
                center.Population -= immigratePop;
                if (!_gameEntity.IsAlivePossessed(neighbourIndex))
                {
                    Human adjacent = new Human(neighbourIndex);
                    adjacent.Population = immigratePop;
                    adjacent.SetPictureBox(ref _gameDisplay);
                    adjacent.SetCivIndex(center.GetCivIndex());

                    Random ran = new Random();
                    adjacent.Agriculture = ran.Next(center.Agriculture - 1 < 0 ? 0 : (int)center.Agriculture - 1, (int)center.Agriculture + 2);
                    adjacent.Culture = ran.Next(center.Culture - 1 < 0 ? 0 : (int)center.Culture - 1, (int)center.Culture + 2);
                    adjacent.Industry = ran.Next(center.Industry - 1 < 0 ? 0 : (int)center.Industry - 1, (int)center.Industry + 2);
                    adjacent.Military = ran.Next(center.Military - 1 < 0 ? 0 : (int)center.Military - 1, (int)center.Military + 2);
                    adjacent.Technology = ran.Next(center.Technology - 1 < 0 ? 0 : (int)center.Technology - 1, (int)center.Technology + 2);


                    _gameEntity.Add(adjacent);
                }
                else
                {
                    Alive alive_adjacent;
                    _gameEntity.GetAlive(neighbourIndex.X, neighbourIndex.Y, out alive_adjacent);
                    Human adj = alive_adjacent as Human;
                    if (adj != null)
                    {
                        adj.Population += immigratePop;
                        //_gameEntity.Set(adj);
                    }
                }
            }
        }

        public void MoveFromLandToWater(Point p)
        {
            List<Point> validNeigbour = SelectNeighbourOfUnpossessedWater(p);
            if (validNeigbour.Count == 0)
            {
                return;
            }

            Alive alive;
            _gameEntity.GetAlive(p.X, p.Y, out alive);
            Human human = alive as Human;
            human.Population -= GameParameter.Human_VoyagePopulation;
            Random rand=new Random();
            human.ToVoyage = rand.Next(0, GameParameter.Human_GoVoyage);
            //_gameEntity.Set(human);

            Point waterIndex = CommonFunctions.RandonPickFromPnts(validNeigbour);
            Human voyageHuman = human.Clone() as Human;
            voyageHuman.SetLocationIndex(waterIndex.X, waterIndex.Y);
            voyageHuman.Population = GameParameter.Human_VoyagePopulation;
            voyageHuman.ToVoyage = -1;
            voyageHuman.SetPictureBox(ref _gameDisplay);
            //voyageHuman.Paint();
            _gameEntity.Set(voyageHuman);
        }

        public void MoveOnWater(Point p)
        {
            List<Point> validNeigbour = SelectNeighbourOfLandOrUnpossessedWater(p);
            if (validNeigbour.Count == 0)
            {
                return;
            }

            Alive alive;
            _gameEntity.GetAlive(p.X, p.Y, out alive);
            Human human = alive as Human;
            Human movedHuman = human.Clone() as Human;
            _gameEntity.RemoveAlive(p);

            Point newIndex = CommonFunctions.RandonPickFromPnts(validNeigbour);
            Environ environ;
            _gameEntity.GetEnviron(newIndex.X, newIndex.Y, out environ);
            if (environ.GetElementType() == Element.Water)
            {
                movedHuman.SetLocationIndex(newIndex.X, newIndex.Y);
                _gameEntity.Add(movedHuman);
                //movedHuman.Paint();
            }
            else//if on land
            {
                if (_gameEntity.IsAlivePossessed(newIndex))//if there exists human, then merge
                {
                    Alive possessed_alive;
                    _gameEntity.GetAlive(newIndex.X, newIndex.Y, out possessed_alive);
                    if (possessed_alive.GetElementType() == Element.Human)
                    {
                        Human possessed_human = possessed_alive as Human;
                        possessed_human.Population += movedHuman.Population;
                    }
                }
                else
                {
                    movedHuman.SetLocationIndex(newIndex.X, newIndex.Y);
                    _gameEntity.Add(movedHuman);
                    //movedHuman.Paint();
                }
            }
        }

        public void DecreaseGrowthRate(Point p)
        {
//             Alive alive_center;
//             if (!_gameEntity.GetAlive(p.X, p.Y, out alive_center))
//             {
//                 throw new Exception("p is not the desired index!");
//             }
//             Human center = alive_center as Human;
//             if (center == null)
//             {
//                 throw new Exception("p must point to human!");
//             }
// 
//             center.GrowthRate = -0.1;
//             _gameEntity.Set(center);
        }

        List<Point> SelectNeighbourOfUnpossessedWater(Point p)
        {
            List<Point> res = new List<Point>();
            List<Point> neighbours = CommonFunctions.GetNeighbourIndex(p);
            foreach (Point neighbour in neighbours)
            {
                if (!_gameEntity.IsEnvironPossessed(neighbour)) continue;

                Environ environ;
                _gameEntity.GetEnviron(neighbour.X, neighbour.Y, out environ);
                if (environ.GetElementType() != Element.Water) continue;

                if (_gameEntity.IsAlivePossessed(neighbour)) continue;

                res.Add(neighbour);
            }

            return res;
        }

        List<Point> SelectNeighbourOfLandOrUnpossessedWater(Point p)
        {
            List<Point> res = new List<Point>();
            List<Point> neighbours = CommonFunctions.GetNeighbourIndex(p);
            foreach (Point neighbour in neighbours)
            {
                if (!_gameEntity.IsEnvironPossessed(neighbour)) continue;

                Environ environ;
                _gameEntity.GetEnviron(neighbour.X, neighbour.Y, out environ);
                if (environ.GetElementType() == Element.Water)
                {
                    if (_gameEntity.IsAlivePossessed(neighbour)) continue;
                }

                res.Add(neighbour);
            }

            return res;
        }
    }
}
