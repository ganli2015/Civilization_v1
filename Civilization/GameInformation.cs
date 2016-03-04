using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GameEntity;
using CivilizationEntity;

namespace Civilization
{
    public partial class GameInformation : Form
    {
        GameElements _gameElements;
        int _combox_SelectedIndex;

        public GameInformation()
        {
            InitializeComponent();
            _combox_SelectedIndex = 0;
        }

        public void UpdateUI()
        {
            textBox_Population.Text = Convert.ToString(ComputeTotalPopulation());
            textBox_CivNum.Text = Convert.ToString(GameParameter.CivNum);

            comboBox_Civ.Items.Clear();
            for (int i = 0; i < GameParameter.CivNum;++i )
            {
                string str = "Civ   " + Convert.ToString(i);
                comboBox_Civ.Items.Add(str);
            }
            
            textBox_Pop.Text = Convert.ToString(ComputeOneCivPopulation(_combox_SelectedIndex));

        }

        public void RefreshGameElements(ref GameElements gameElements)
        {
            _gameElements=gameElements;
        }

        int ComputeTotalPopulation()
        {
            List<Point> aliveIndex=_gameElements.GetAliveIndexes();
            int pop_sum = 0;
            foreach (Point index in aliveIndex)
            {
                Alive alive;
                _gameElements.GetAlive(index.X, index.Y, out alive);
                if (alive.GetElementType() != Element.Human) continue;

                Human human=alive as Human;
                pop_sum += human.Population;
            }

            return pop_sum;
        }

        int ComputeOneCivPopulation(int i)
        {
            List<Point> aliveIndex = _gameElements.GetAliveIndexes();
            int pop_sum = 0;
            foreach (Point index in aliveIndex)
            {
                Alive alive;
                _gameElements.GetAlive(index.X, index.Y, out alive);
                if (alive.GetElementType() != Element.Human) continue;
                if (alive.GetCivIndex() != i) continue;

                Human human = alive as Human;
                pop_sum += human.Population;
            }

            return pop_sum;
        }

        private void GameInformation_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }

        private void comboBox_Civ_SelectedIndexChanged(object sender, EventArgs e)
        {
            _combox_SelectedIndex=comboBox_Civ.SelectedIndex;

            textBox_Pop.Text = Convert.ToString(ComputeOneCivPopulation(_combox_SelectedIndex));
        }
    }
}
