using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GameEntity;
using CivilizationEntity;

namespace CivilizationWPF
{
    public class IOManager
    {
        public static void SaveAsFile(string filename,ref GameElements gameEntity)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            //bw.Write(GlobalParameter.GridLength);
            bw.Write(GlobalParameter.grid_num_x);
            bw.Write(GlobalParameter.grid_num_y);
            bw.Write(GlobalParameter.valid_height);
            bw.Write(GlobalParameter.valid_width);
            bw.Write(GlobalParameter.valid_left);
            bw.Write(GlobalParameter.valid_top);

            bw.Write(GameParameter.CivNum);
//             bw.Write(GameParameter.Value_ConvertToNewCiv);
//             bw.Write(GameParameter.Human_Init_Population);
//             bw.Write(GameParameter.Human_Immigrate_Population);
//             bw.Write(GameParameter.Grass_Init_Food);
//             bw.Write(GameParameter.Grass_Init_GrowthRate);

            bw.Flush();
            bw.Close();
            fs.Close();
            gameEntity.Save(filename);
        }

        public static void LoadFile(string filename, ref GameElements gameEntity)
        {
            gameEntity.Clear();

            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            GlobalParameter.grid_num_x=br.ReadInt32();
            GlobalParameter.grid_num_y=br.ReadInt32();
            GlobalParameter.valid_height=br.ReadInt32();
            GlobalParameter.valid_width=br.ReadInt32();
            GlobalParameter.valid_left=br.ReadInt32();
            GlobalParameter.valid_top = br.ReadInt32();

            GameParameter.CivNum = br.ReadInt32();

            try
            {
                while (true)
                {
                    int index = br.ReadInt32();
                    Element element = (Element)(index);

                    switch (element)
                    {
                        case Element.Human:
                            {
                                int x = br.ReadInt32();
                                int y = br.ReadInt32();
                                int population = br.ReadInt32();
                                int civIndex = br.ReadInt32();
                                int toVoyage = br.ReadInt32();
                                Element environ = (Element)(br.ReadInt32());

                                double agriculture = br.ReadDouble();
                                double culture = br.ReadDouble();
                                double industry = br.ReadDouble();
                                double military = br.ReadDouble();
                                double technology = br.ReadDouble();

                                Human human = new Human(x,y);
                                human.Population = population;
                                human.SetCivIndex(civIndex);
                                human.ToVoyage = toVoyage;
                                human.Environ = environ;

                                human.Agriculture = agriculture;
                                human.Culture = culture;
                                human.Industry = industry;
                                human.Military = military;
                                human.Technology = technology;

                                gameEntity.Add(human);

                                break;
                            }
                        case Element.Grass:
                            {
                                int x = br.ReadInt32();
                                int y = br.ReadInt32();
                                int food = br.ReadInt32();
                                double growthRate = br.ReadDouble();

                                Grass grass = new Grass(x, y);
                                grass.Food = food;
                                grass.GrowthRate = growthRate;
                                gameEntity.Add(grass);

                                break;
                            }
                        case Element.Water:
                            {
                                int x = br.ReadInt32();
                                int y = br.ReadInt32();
                                int food = br.ReadInt32();
                                double growthRate = br.ReadDouble();

                                Water water = new Water(x, y);
                                water.Seafood = food;
                                water.GrowthRate = growthRate;
                                gameEntity.Add(water);

                                break;
                            }
                    }
                }
            }
            catch (EndOfStreamException ee)
            {
                br.Close();
            }
        }
    }
}
