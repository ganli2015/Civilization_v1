using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameEntity
{
    public struct GlobalParameter
    {
        public const float GridLength=10;

        public static bool resizing;
        public static bool main_running;

        public static int grid_num_x;
        public static int grid_num_y;
        public static int valid_width;
        public static int valid_height;
        public static int valid_top;
        public static int valid_left;

        public static int GameRound_Interval = 910;

        public static Color PictureboxColor = Color.Cornsilk;

        public static Color HumanColor = Color.Red;

        public static Color GrassColor = Color.FromArgb(153, 204, 51);
        public static Color WaterColor = Color.LightBlue;
        public static Color DesertColor = Color.SandyBrown;
        public static Color ForestColor = Color.ForestGreen;
        public static Color MountainColor = Color.Sienna;
        public static Color IceColor = Color.Snow;
    }

    public struct GameParameter
    {
        public static int CivNum = 0;
        public const int Value_ConvertToNewCiv = 15;
        public const double CIVPropertyTranslationCoefficient = 0.1;

        public const int Human_Init_Population = 100;
        public const double Human_GrowthRate_Grass = 0.01;
        public const int Human_Immigrate_Population = 300;
        public const double Human_LackFoodDeathRate = 0.5;
        public const double Human_ImmigrateRate = 0.1;
        public const int Human_GoVoyage = 50;
        public const int Human_VoyagePopulation = 100;

        public const int Grass_Init_Food = 10000;
        public const int Grass_Upperlimit_Food = 100000;
        public const int Grass_Lowerlimit_Food = 100;
        public const double Grass_Init_GrowthRate = 1;
        public const int Water_Init_Seafood = 1000;
        public const int Water_Upperlimit_Food = 1000;
        public const int Water_LowerLimit_Food = 100;
        public const double Water_Init_GrowthRate = 0.2;
    }
}


