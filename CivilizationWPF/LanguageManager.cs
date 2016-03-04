using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CivilizationWPF
{
    public enum Language
    {
            Chinese,
            English
    }

    public class LanguageManager
    {
        Dictionary<string, string> _ID_name;

        static LanguageManager _instance;

        static public LanguageManager GetInstance()
        {
            if (_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = new LanguageManager();
                return _instance;
            }
        }

        public LanguageManager()
        {
            _ID_name = new Dictionary<string, string>();
            RefreshData(Language.Chinese);
        }

        public string Get(string id)
        {
            return _ID_name[id];
        }

        public void SetLanguage(Language language)
        {
            RefreshData(language);
        }

        private void RefreshData(Language language)
        {
            switch(language)
            {
                case Language.English:
                    {
                        _ID_name["Title_MainForm"] = "Civilization";
                        _ID_name["Button_Start"] = "Start";
                        _ID_name["Button_Configure"] = "Create";
                        _ID_name["Button_Pause"] = "Pause";
                        _ID_name["Button_Resume"] = "Resume";
                        _ID_name["Button_Reset"] = "Reset";
                        _ID_name["Button_Save"] = "Save";
                        _ID_name["Button_Load"] = "Load";
                        _ID_name["Label_SpeedContral"] = "Speed Control";
                        _ID_name["button_GameInfo1"] = "Game";
                        _ID_name["button_GameInfo2"] = "Info";
                        _ID_name["button_Exit"] = "Exit";

                        _ID_name["Title_Configure"] = "Elements";
                        _ID_name["Human"] = "Human";
                        _ID_name["Grass"] = "Grass";
                        _ID_name["Water"] = "Water";
                        _ID_name["Desert"] = "Desert";
                        _ID_name["Forest"] = "Forest";
                        _ID_name["Mountain"] = "Mountain";
                        _ID_name["Ice"] = "Ice";
                        _ID_name["Exit_Configure"] = "Back";

                        _ID_name["Human_Population"] = "Population";
                        _ID_name["Human_Civilization"] = "Civilization Index";
                        _ID_name["Agriculture"] = "Agriculture";
                        _ID_name["Culture"] = "Culture";
                        _ID_name["Industry"] = "Industry";
                        _ID_name["Military"] = "Military";
                        _ID_name["Technology"] = "Technology";

                        _ID_name["Grass_Food"] = "Food";
                        _ID_name["Water_Seafood"] = "Seafood";
                        

                        break;
                    }
                case Language.Chinese:
                    {
                        _ID_name["Title_MainForm"] = "文明";
                        _ID_name["Button_Start"] = "开始";
                        _ID_name["Button_Configure"] = "创建单位";
                        _ID_name["Button_Pause"] = "暂停";
                        _ID_name["Button_Resume"] = "继续";
                        _ID_name["Button_Reset"] = "游戏重置";
                        _ID_name["Button_Save"] = "保存";
                        _ID_name["Button_Load"] = "读取";
                        _ID_name["Label_SpeedContral"] = "速度控制";
                        _ID_name["button_GameInfo1"] = "游戏";
                        _ID_name["button_GameInfo2"] = "信息";
                        _ID_name["button_Exit"] = "退出";

                        _ID_name["Title_Configure"] = "游戏单位";
                        _ID_name["Human"] = "人类";
                        _ID_name["Grass"] = "草原";
                        _ID_name["Water"] = "水";
                        _ID_name["Desert"] = "沙漠";
                        _ID_name["Forest"] = "森林";
                        _ID_name["Mountain"] = "山脉";
                        _ID_name["Ice"] = "冰原";
                        _ID_name["Exit_Configure"] = "返回";

                        _ID_name["Human_Population"] = "人口";
                        _ID_name["Human_Civilization"] = "文明编号";
                        _ID_name["Agriculture"] = "农业";
                        _ID_name["Culture"] = "文化";
                        _ID_name["Industry"] = "工业";
                        _ID_name["Military"] = "军事";
                        _ID_name["Technology"] = "科技";

                        _ID_name["Grass_Food"] = "食物";
                        _ID_name["Water_Seafood"] = "水产";

                        break;
                    }
            }
        }
    }
}
