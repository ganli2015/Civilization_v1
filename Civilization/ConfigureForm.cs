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
    public partial class ConfigureForm : Form
    {
        List<CheckBox> _checkboxes;

        public ConfigureForm()
        {
            InitializeComponent();

            InitializeCheckBox();

            InitializeLanguage();
        }

        private void InitializeCheckBox()
        {
            _checkboxes = new List<CheckBox>();
            _checkboxes.Add(checkBox_Human);
            _checkboxes.Add(checkBox_Grass);
            _checkboxes.Add(checkBox_Water);
        }

        public void InitializeLanguage()
        {
            LanguageManager lm = LanguageManager.GetInstance();

            this.Text = lm.Get("Title_Configure");
            checkBox_Human.Text = lm.Get("Human");
            checkBox_Grass.Text = lm.Get("Grass");
            checkBox_Water.Text = lm.Get("Water");
        }

        private void CheckFalseAllCheckBox()
        {
            foreach (CheckBox box in _checkboxes)
            {
                box.Checked = false;
            }
        }

        private void UncheckAllBoxBut(CheckBox checkbox)
        {
            foreach (CheckBox box in _checkboxes)
            {
                if (checkbox == box)
                {
                    box.Checked = true;
                }
                else
                    box.Checked = false;
            }
            return;
        }


        public bool GetElementSymbol(out Element element)
        {
            if (checkBox_Human.Checked)
            {
                 element=Element.Human;
                 return true;
            }
            else if (checkBox_Grass.Checked)
            {
                element = Element.Grass;
                return true;
            }
            else if (checkBox_Water.Checked)
            {
                element = Element.Water;
                return true;
            }

            element = Element.Human;
            return false;
        }

        private void ConfigureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;

            checkBox_Human.Checked = false;
            checkBox_Grass.Checked = false;
            checkBox_Water.Checked = false;
        }

        private void checkBox_Human_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Human.Checked)
            {
                UncheckAllBoxBut(checkBox_Human);
            }
        }

        private void checkBox_Grass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Grass.Checked)
            {
                UncheckAllBoxBut(checkBox_Grass);
            }
        }

        private void checkBox_Water_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Water.Checked)
            {
                UncheckAllBoxBut(checkBox_Water);
            }
        }

        
    }
}
