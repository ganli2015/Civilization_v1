using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using GameEntity;
using CivilizationEntity;

namespace Civilization
{
    public class MapGenerator
    {
        GameDisplay _gameDisplay;

        public MapGenerator(ref GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
        }

        public void AllGrass()
        {
            GameElements gameEntity = new CIVElements(ref _gameDisplay);

            for (int i = 0; i < GlobalParameter.grid_num_x;++i )
            {
                for (int j = 0; j < GlobalParameter.grid_num_y;++j )
                {
                    Grass grass = new Grass(i, j);
                    gameEntity.Add(grass);
                }
            }

            IOManager.SaveAsFile("D:\\Grass.civ", ref gameEntity);

        }

        public void IslandInLake()
        {
            GameElements gameEntity = new CIVElements(ref _gameDisplay);

            for (int i = 0; i < GlobalParameter.grid_num_x; ++i)
            {
                for (int j = 0; j < GlobalParameter.grid_num_y; ++j)
                {
                    Grass grass = new Grass(i, j);
                    gameEntity.Add(grass);
                }
            }

            Point circleCenter = new Point((GlobalParameter.grid_num_x) / 2, (GlobalParameter.grid_num_y) / 2);
            int islandRadius = 5;
            int lakeRadius = 10;
            for (int i = 0; i < GlobalParameter.grid_num_x; ++i)
            {
                for (int j = 0; j < GlobalParameter.grid_num_y; ++j)
                {
                    int square = (i - circleCenter.X) * (i - circleCenter.X) + (j - circleCenter.Y) * (j - circleCenter.Y);
                    if (square >= islandRadius * islandRadius && square <= lakeRadius * lakeRadius)
                    {
                        Water water = new Water(i, j);
                        gameEntity.Set(water);
                    }
                }
            }

            IOManager.SaveAsFile("D:\\IslandInLake.civ", ref gameEntity);
        }

        public void RandomIsland()
        {
            GameElements gameEntity = new CIVElements(ref _gameDisplay);

            for (int i = 0; i < GlobalParameter.grid_num_x; ++i)
            {
                for (int j = 0; j < GlobalParameter.grid_num_y; ++j)
                {
                    Water water = new Water(i, j);
                    gameEntity.Add(water);
                }
            }

            Random rand = new Random();
            int islandNum=30;
            for (int i = 0; i < islandNum;++i )
            {
                int islandSize=rand.Next(1,10);
                int x = rand.Next(0, GlobalParameter.grid_num_x);
                int y = rand.Next(0, GlobalParameter.grid_num_y);
                Point circleCenter = new Point(x, y);
                List<Point> islandPnts = CommonFunctions.GetCircleArea(circleCenter, islandSize);

                foreach (Point p in islandPnts)
                {
                    Grass grass = new Grass(p.X, p.Y);
                    gameEntity.Set(grass);
                }
            }

            IOManager.SaveAsFile("D:\\RandomIslands.civ", ref gameEntity);
        }
    }
}
