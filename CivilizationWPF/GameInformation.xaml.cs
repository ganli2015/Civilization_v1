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
using System.ComponentModel;
using CivilizationEntity;
using GameEntity;

namespace CivilizationWPF
{
    /// <summary>
    /// GameInformation.xaml 的交互逻辑
    /// </summary>
    /// 

    

    public partial class GameInformation : Window
    {
        GameElements _gameElements;
        WholeInfo _gameInfo;

        int _selectedIndex;

        public GameInformation(ref GameElements gameElements)
        {
            InitializeComponent();

            _gameInfo = new WholeInfo(ref gameElements);
            _selectedIndex = -1;

            Binding binding_totalPop = new Binding();
            binding_totalPop.Source = _gameInfo;
            binding_totalPop.Path = new System.Windows.PropertyPath("TotalPop");
            BindingOperations.SetBinding(textBlock_Population, TextBlock.TextProperty, binding_totalPop);

            Binding binding_civNum = new Binding();
            binding_civNum.Source = _gameInfo;
            binding_civNum.Path = new System.Windows.PropertyPath("CivNum");
            BindingOperations.SetBinding(textBlock_CivNum, TextBlock.TextProperty, binding_civNum);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        public void RefreshGameElements(ref GameElements gameElements)
        {
            _gameInfo.Refresh(ref gameElements);

            List<string> itemColloction = new List<string>();
            for (int i = 0; i < GameParameter.CivNum; ++i)
            {
                string str = "Civ   " + Convert.ToString(i);
                itemColloction.Add(str);
            }
            comboBox_Civ.ItemsSource = itemColloction;

            if (_selectedIndex >= 0)
            {
                CivInfo civInfo = _gameInfo.GetSingleInfo(_selectedIndex);

                textBlock_Pop.Text = Convert.ToString(civInfo.Population);
            }
            
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
        }

        private void comboBox_Civ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedIndex = comboBox_Civ.SelectedIndex;

            
        }

    
    }

    class CivInfo
    {
        int _pop;
        int _index;

        public int Population
        {
            get { return _pop; }
            set { _pop = value; }
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public CivInfo(int i, ref GameElements gameElements)
        {
            _index = i;
            ComputePopulation(ref gameElements);
        }

        void ComputePopulation(ref GameElements gameElements)
        {
            List<System.Drawing.Point> aliveIndex = gameElements.GetAliveIndexes();
            int pop_sum = 0;
            foreach (System.Drawing.Point index in aliveIndex)
            {
                Alive alive;
                gameElements.GetAlive(index.X, index.Y, out alive);
                if (alive.GetElementType() != Element.Human) continue;
                if (alive.GetCivIndex() != _index) continue;

                Human human = alive as Human;
                pop_sum += human.Population;
            }

            _pop = pop_sum;
        }
    }

    class WholeInfo:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        int _totalPop;

        public int TotalPop
        {
            get { return _totalPop; }
            set
            {
                _totalPop = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("TotalPop"));
                }
            }
        }

        int _civNum;

        public int CivNum
        {
            get { return _civNum; }
            set
            {
                _civNum = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("CivNum"));
                }
            }
        }

        List<CivInfo> _civInfos;

        public WholeInfo(ref GameElements gameElements)
        {
            _civInfos = new List<CivInfo>();
            Refresh(ref gameElements);
        }

        public void Refresh(ref GameElements gameElements)
        {
            CivNum = GameParameter.CivNum;

            _civInfos.Clear();

            int popSum=0;
            for (int i = 0; i < GameParameter.CivNum; ++i)
            {
                CivInfo civInfo = new CivInfo(i, ref gameElements);
                popSum += civInfo.Population;

                _civInfos.Add(civInfo);
            }

            TotalPop = popSum;
        }

        public CivInfo GetSingleInfo(int i)
        {
            return _civInfos[i];
        }
    }
}
