using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CivilizationEntity;
using GameEntity;

namespace CivilizationWPF
{
    /// <summary>
    /// ConfigureForm.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigureForm : Window
    {
        List<RadioButton> _checkboxes;

        public ConfigureForm()
        {
            InitializeComponent();
        }

        private void InitializeCheckBox()
        {
            _checkboxes = new List<RadioButton>();
            _checkboxes.Add(checkBox_Human);
            _checkboxes.Add(checkBox_Grass);
            _checkboxes.Add(checkBox_Water);
            _checkboxes.Add(checkBox_Desert);
            _checkboxes.Add(checkBox_Forest);
            _checkboxes.Add(checkBox_Mountain);
            _checkboxes.Add(checkBox_Ice);
        }

        public void InitializeLanguage()
        {
            LanguageManager lm = LanguageManager.GetInstance();

            this.Title = lm.Get("Title_Configure");
            checkBox_Human.Content = lm.Get("Human");
            checkBox_Grass.Content = lm.Get("Grass");
            checkBox_Water.Content = lm.Get("Water");
            checkBox_Desert.Content = lm.Get("Desert");
            checkBox_Forest.Content = lm.Get("Forest");
            checkBox_Mountain.Content = lm.Get("Mountain");
            checkBox_Ice.Content = lm.Get("Ice");
            button_exit.Content = lm.Get("Exit_Configure");
        }

        private void CheckFalseAllCheckBox()
        {
            foreach (RadioButton box in _checkboxes)
            {
                box.IsChecked = false;
            }
        }


        public bool GetElementSymbol(out Element element)
        {
            if ((bool)checkBox_Human.IsChecked)
            {
                element = Element.Human;
                return true;
            }
            else if ((bool)checkBox_Grass.IsChecked)
            {
                element = Element.Grass;
                return true;
            }
            else if ((bool)checkBox_Water.IsChecked)
            {
                element = Element.Water;
                return true;
            }
            else if ((bool)checkBox_Desert.IsChecked)
            {
                element = Element.Desert;
                return true;
            }
            else if ((bool)checkBox_Forest.IsChecked)
            {
                element = Element.Forest;
                return true;
            }
            else if ((bool)checkBox_Mountain.IsChecked)
            {
                element = Element.Mountain;
                return true;
            }
            else if ((bool)checkBox_Ice.IsChecked)
            {
                element = Element.Ice;
                return true;
            }


            element = Element.None;
            return false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Collapsed;

            AllCheckFalse();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

            AllCheckFalse();
        }

        private void AllCheckFalse()
        {
            checkBox_Human.IsChecked = false;
            checkBox_Grass.IsChecked = false;
            checkBox_Water.IsChecked = false;
            checkBox_Desert.IsChecked = false;
            checkBox_Forest.IsChecked = false;
            checkBox_Mountain.IsChecked = false;
            checkBox_Ice.IsChecked = false;
        }
    }
}
