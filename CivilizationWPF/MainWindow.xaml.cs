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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using System.Text;
using GameEntity;
using CivilizationEntity;
using Evolution;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.IO;
using System.Windows.Media.Effects;
using Microsoft.Expression.Media.Effects;



namespace CivilizationWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        GameElements _gameElements;
        GameMain _gameMain;
        GameDisplay _gameDisplay;
        Thread _mainthread;

        ConfigureForm _config;
        GameInformation _gameInfo;

        bool _pictureboxStateChanged;


        public MainWindow()
        {
            InitializeComponent();

            Initialize();

            
        }

        private void Initialize()
        {
            _config = new ConfigureForm();
            _gameInfo = new GameInformation(ref _gameElements);
            _gameDisplay = new GameDisplayImp2(ref PictureGrid);
            _gameElements = new CIVElements(ref _gameDisplay);
            _gameMain = new GameMain(ref _gameDisplay);

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            _pictureboxStateChanged = false;

            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.Topmost = false;
            this.Left = 0.0;
            this.Top = 0.0;
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;

            comboBox_Language.SelectedIndex = 0;

            InitializePictureBox();
            InitializeLanguage();

            {
//                 MapGenerator mapGenerator = new MapGenerator(ref _gameDisplay);
//                 mapGenerator.AllGrass();
//                 mapGenerator.IslandInLake();
//                 mapGenerator.RandomIsland();
            }
        }

        private void InitializePictureBox()
        {
            PictureGrid.Children.Clear();

            PictureGrid.Width = border1.Width - 2 * border1.BorderThickness.Top;
            PictureGrid.Height = border1.Height - 2 * border1.BorderThickness.Top;
            GlobalParameter.grid_num_x = (int)(PictureGrid.Width / GameEntity.GlobalParameter.GridLength);
            GlobalParameter.grid_num_y = (int)(PictureGrid.Height / GameEntity.GlobalParameter.GridLength);
            PictureGrid.Width = GlobalParameter.grid_num_x * GlobalParameter.GridLength;
            PictureGrid.Height = GlobalParameter.grid_num_y * GlobalParameter.GridLength;
            GlobalParameter.valid_width = (int)PictureGrid.Width;
            GlobalParameter.valid_height = (int)PictureGrid.Height;
            border1.Width = GlobalParameter.valid_width+2*border1.BorderThickness.Top;
            border1.Height = GlobalParameter.valid_height + 2 * border1.BorderThickness.Top;

        }

        private void InitializeLanguage()
        {
            LanguageManager lm = LanguageManager.GetInstance();

            this.Title = lm.Get("Title_MainForm");
            button_Start.Content = lm.Get("Button_Start");
            button_Configure.Content = lm.Get("Button_Configure");
            button_Pause.Content = lm.Get("Button_Pause");
            button_Reset.Content = lm.Get("Button_Reset");
            button_Save.Content = lm.Get("Button_Save");
            button_Load.Content = lm.Get("Button_Load");
            TextBlock_SpeedControl.Text = lm.Get("Label_SpeedContral");
            textblock_GameInfo1.Text = lm.Get("button_GameInfo1");
            textblock_GameInfo2.Text = lm.Get("button_GameInfo2");
            button_Exit.Content = lm.Get("button_Exit");
        }

        private void button_Exit_Click(object sender, RoutedEventArgs e)
        {
            if(_mainthread!=null)
                _mainthread.Abort();
            this.Close();
            _config.Close();
            _gameInfo.Close();
            Environment.Exit(0);
        }

        private void button_Start_Click(object sender, RoutedEventArgs e)
        {
            if(!_pictureboxStateChanged)
                ChangePictureboxState();

            button_Start.IsEnabled = false;
            button_Start.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            button_Configure.IsEnabled = false;
            button_Configure.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));

            _mainthread = new Thread(new ThreadStart(Start));
            _mainthread.SetApartmentState(ApartmentState.STA);
            _mainthread.IsBackground = true;

            _mainthread.Start();

        }

        private void Start()
        {
            _gameMain.InitializeHumanCiviDistribution(ref _gameElements);

            while (true)
            {
                GlobalParameter.main_running = true;
                DateTime start = DateTime.Now;
                _gameMain.Run(ref _gameElements);
                if (GlobalParameter.resizing)
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        _gameElements.Paint();
                    });
                    GlobalParameter.resizing = false;
                }
                else
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        _gameElements.Draw();
                    });
                }
                GlobalParameter.main_running = false;
                while ((DateTime.Now - start).Milliseconds < GlobalParameter.GameRound_Interval) ;

                if (_gameInfo.Visibility == Visibility.Visible)
                {
                    this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        _gameInfo.RefreshGameElements(ref _gameElements);
                    });
                }
            }
        }

        private void button_Configure_Click(object sender, RoutedEventArgs e)
        {
            if (!_pictureboxStateChanged)
                ChangePictureboxState();

            _config.WindowStartupLocation = this.WindowStartupLocation;
            _config.Visibility = Visibility.Visible;
            _config.Topmost = true;
            _config.InitializeLanguage();
            _config.Show();
        }

        private void button_Pause_Click(object sender, RoutedEventArgs e)
        {
            if (_mainthread == null) return;

            LanguageManager lm = LanguageManager.GetInstance();

            if (button_Pause.Content == lm.Get("Button_Pause"))
            {
                while (GlobalParameter.main_running) ;
                if (_mainthread != null)
                    _mainthread.Suspend();
                button_Pause.Content = lm.Get("Button_Resume");

                if (_mainthread != null)
                {
                    button_Configure.IsEnabled = true;
                    button_Configure.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                }
            }
            else
            {
                if (_mainthread != null)
                    _mainthread.Resume();
                button_Pause.Content = lm.Get("Button_Pause");

                if (_mainthread != null)
                {
                    button_Configure.IsEnabled = false;
                    button_Configure.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
                }
            }
        }

        private void button_Reset_Click(object sender, RoutedEventArgs e)
        {
            if (_mainthread != null)
            {
                ThreadState state = _mainthread.ThreadState;
                if (state == (ThreadState.Background | ThreadState.Suspended))
                {
                    _mainthread.Resume();
                    _mainthread.Abort();
                }
                else
                    _mainthread.Abort();
            }

            _gameElements.Clear();
            button_Start.IsEnabled = true;
            button_Start.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            button_Configure.IsEnabled = true;
            button_Configure.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
//             Graphics g = pictureBox.CreateGraphics();
//             g.Clear(SystemColors.Control);
            InitializePictureBox();
            BitmapImage bi = new BitmapImage(new Uri("gameBackground.bmp", UriKind.RelativeOrAbsolute));
            PictureGrid.Background = new ImageBrush(bi);

            GameParameter.CivNum = 0;
        }

        private void button_Save_Click(object sender, RoutedEventArgs e)
        {
            string initPath = System.AppDomain.CurrentDomain.BaseDirectory;
            initPath += "Save";
            if (!Directory.Exists(initPath))
            {
                Directory.CreateDirectory(initPath);
            }

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.InitialDirectory = initPath;
            sfd.Filter = "civ文件(*.civ)|*.civ";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                IOManager.SaveAsFile(sfd.FileName, ref _gameElements);
            }
        }

        private void button_Load_Click(object sender, RoutedEventArgs e)
        {
            string initPath = System.AppDomain.CurrentDomain.BaseDirectory;
            initPath += "Save";
            if (!Directory.Exists(initPath))
            {
                Directory.CreateDirectory(initPath);
            }

            System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.InitialDirectory = initPath;
            openFileDialog.Filter = "civ文件(*.civ)|*.civ";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_mainthread != null)
                {
                    _mainthread.Abort();
                }
                _gameElements.Clear();
                IOManager.LoadFile(openFileDialog.FileName, ref _gameElements);
                if (!_pictureboxStateChanged)
                {
                    ChangePictureboxState();
                }
                _gameElements.Paint();

            }

        }

        private void slider_SpeedControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GlobalParameter.GameRound_Interval = (int)(999 - e.NewValue * 10);
        }

        private void comboBox_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox_Language.SelectedIndex == 0)
            {
                LanguageManager lm = LanguageManager.GetInstance();
                lm.SetLanguage(CivilizationWPF.Language.Chinese);


                InitializeLanguage();
                _config.InitializeLanguage();
            }
            else if (comboBox_Language.SelectedIndex == 1)
            {
                LanguageManager lm = LanguageManager.GetInstance();
                lm.SetLanguage(CivilizationWPF.Language.English);


                InitializeLanguage();
                _config.InitializeLanguage();
            }
        }

        private void StartGameInfo()
        {
            _gameInfo.WindowStartupLocation = this.WindowStartupLocation;
            _gameInfo. Visibility=Visibility.Visible;
            _gameInfo.Topmost = true;
            _gameInfo.Show();

        }

        private void button_GameInfo_Click(object sender, RoutedEventArgs e)
        {
            StartGameInfo();
        }

        private GridIndex GetGridIndex(Point p)
        {
            float correctedGridLength = (GlobalParameter.valid_width - GlobalParameter.GridLength) / (GlobalParameter.valid_width / GlobalParameter.GridLength-2);

            GridIndex index;
            double px = (double)p.X;
            double py = (double)p.Y;
            double tmpx = 0, tmpy = 0;
            int index_x = -1, index_y = -1;
            while (tmpx <= px)
            {
                tmpx += correctedGridLength;
                ++index_x;
            }
            while (tmpy <= py)
            {
                tmpy += correctedGridLength;
                ++index_y;
            }

            index.x = index_x;
            index.y = index_y;

            return index;
        }

        private void AddAliveToEntity(GridIndex index, ref GameElements gameElements, Element element)
        {
            AliveFactory factory = new AliveFactory();
            Alive alive = factory.CreateAlive(element);
            alive.SetLocationIndex(index.x, index.y);
            gameElements.Set(alive);
            alive.Paint();
        }

        private void AddEnvironToEntity(GridIndex index, ref GameElements gameElements, Element element)
        {
            EnvironFactory factory = new EnvironFactory();
            Environ environ = factory.CreateEnviron(element);
            environ.SetLocationIndex(index.x, index.y);
            gameElements.Set(environ);
            System.Drawing.Point p = new System.Drawing.Point();
            p.X = index.x;
            p.Y = index.y;

            if (_gameElements.IsAlivePossessed(p))
            {
                Alive alive;
                _gameElements.GetAlive(p.X, p.Y, out alive);
                environ.Paint();
                alive.Paint();
            }
            else
                environ.Paint();
        }

        private System.Drawing.PointF ComputeCircleCenter(GridIndex index)
        {
            float px = (float)(index.x * GlobalParameter.GridLength);
            float py = (float)(index.y * GlobalParameter.GridLength);
            return new System.Drawing.PointF(px, py);
        }

        private System.Drawing.PointF ComputeRectangleLocation(GridIndex index)
        {
            float px = (float)(index.x * GlobalParameter.GridLength);
            float py = (float)(index.y * GlobalParameter.GridLength);
            return new System.Drawing.PointF(px, py);
        }

        private bool IsAlive(Element elem)
        {
            if (elem == Element.Human)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsEnviron(Element elem)
        {
            if (elem == Element.Grass)
            {
                return true;
            }
            else if (elem == Element.Water)
            {
                return true;
            }
            else if (elem == Element.Desert)
            {
                return true;
            }
            else if (elem == Element.Forest)
            {
                return true;
            }
            else if (elem == Element.Mountain)
            {
                return true;
            }
            else if (elem == Element.Ice)
            {
                return true;
            }    
            else
            {
                return false;
            }
        }

        private void PictureGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.PictureGrid.Focus();

            if (_config.IsVisible)
            {
                Element element;
                if (!_config.GetElementSymbol(out element)) return;
                if (IsAlive(element))
                {
                    MouseEventArgs mouse_e = (MouseEventArgs)e;
                    double xx=mouse_e.GetPosition(PictureGrid).X;
                    double yy=mouse_e.GetPosition(PictureGrid).Y;
                    if (mouse_e.GetPosition(PictureGrid).X < GlobalParameter.valid_width && mouse_e.GetPosition(PictureGrid).Y < GlobalParameter.valid_height)
                    {
                        GridIndex index = GetGridIndex(mouse_e.GetPosition(PictureGrid));
                        AddAliveToEntity(index, ref _gameElements, element);
                    }
                }
                else if (IsEnviron(element))
                {
                    MouseEventArgs mouse_e = (MouseEventArgs)e;
                    if (mouse_e.GetPosition(PictureGrid).X < GlobalParameter.valid_width && mouse_e.GetPosition(PictureGrid).Y < GlobalParameter.valid_height)
                    {
                        GridIndex index = GetGridIndex(mouse_e.GetPosition(PictureGrid));
                        AddEnvironToEntity(index, ref _gameElements, element);
                    }
                }
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (_config.IsVisible == false)
            {
                return;
            }

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Element element;
                if (!_config.GetElementSymbol(out element)) return;

                if (e.GetPosition(PictureGrid).X >= GlobalParameter.valid_width && e.GetPosition(PictureGrid).Y >= GlobalParameter.valid_height) return;

                if (IsAlive(element))
                {
                    GridIndex index = GetGridIndex(e.GetPosition(PictureGrid));
                    AddAliveToEntity(index, ref _gameElements, element);
                }
                else if (IsEnviron(element))
                {
                    GridIndex index = GetGridIndex(e.GetPosition(PictureGrid));
                    AddEnvironToEntity(index, ref _gameElements, element);

                }
            }
        }

        private void PictureGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            LanguageManager lm = LanguageManager.GetInstance();

            if (e.RightButton == MouseButtonState.Pressed)
            {
                ListBox list_tooltip = new ListBox();
                list_tooltip.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("BurlyWood"));
                list_tooltip.BorderThickness = new Thickness(0);

                LinearGradientBrush lgb_text = new LinearGradientBrush();
                lgb_text.EndPoint = new Point(1, 0.5);
                lgb_text.StartPoint = new Point(0, 0.5);
                lgb_text.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("Black"), 0));
                lgb_text.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("SaddleBrown"), 1));
                list_tooltip.Foreground = lgb_text;
                list_tooltip.FontWeight = FontWeights.Bold;

                list_tooltip.Items.Clear();
                GridIndex index = GetGridIndex(e.GetPosition(PictureGrid));
                string messege = "";
                Alive alive;
                if (_gameElements.GetAlive(index.x, index.y, out alive))
                {
                    Human hu = (Human)alive;
                    list_tooltip.Items.Add(lm.Get("Human_Population") + "   " + Convert.ToString(hu.Population) + "\r\n");
                    list_tooltip.Items.Add(lm.Get("Human_Civilization") + "   " + Convert.ToString(hu.GetCivIndex()));
                    list_tooltip.Items.Add(lm.Get("Agriculture") + "   " + Convert.ToString((int)hu.Agriculture));
                    list_tooltip.Items.Add(lm.Get("Culture") + "   " + Convert.ToString((int)hu.Culture));
                    list_tooltip.Items.Add(lm.Get("Industry") + "   " + Convert.ToString((int)hu.Industry));
                    list_tooltip.Items.Add(lm.Get("Military") + "   " + Convert.ToString((int)hu.Military));
                    list_tooltip.Items.Add(lm.Get("Technology") + "   " + Convert.ToString((int)hu.Technology));

                }

                Environ environ;
                if (_gameElements.GetEnviron(index.x, index.y, out environ))
                {
                    messege += "\r\n";
                    Element element = environ.GetElementType();
                    switch (element)
                    {
                        case Element.Grass:
                            {
                                Grass grass = environ as Grass;
                                list_tooltip.Items.Add(lm.Get("Grass_Food") + "   " + Convert.ToString(grass.Food));
                                break;
                            }
                        case Element.Water:
                            {
                                Water water = environ as Water;
                                list_tooltip.Items.Add(lm.Get("Water_Seafood") + "   " + Convert.ToString(water.Seafood));
                                break;
                            }
                    }

                }

                ToolTip tooltip = new ToolTip();
                tooltip.HasDropShadow = true;
                tooltip.Content = list_tooltip;
                tooltip.IsOpen = true;
                tooltip.BorderThickness = new Thickness(2);

                LinearGradientBrush lgb = new LinearGradientBrush();
                lgb.EndPoint = new Point(1, 0.5);
                lgb.StartPoint = new Point(0, 0.5);
                lgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("LightBlue"), 0));
                lgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#E00064FF"), 1));
                tooltip.BorderBrush = lgb;

                tooltip.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("BurlyWood"));
                PictureGrid.ToolTip = tooltip;

            }
            
        }

        private void PictureGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            ToolTip tooltip = (ToolTip)PictureGrid.ToolTip;
            if (tooltip.IsOpen==true)
            {
                tooltip.IsOpen = false;
                PictureGrid.ToolTip = null;
            }

        }

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            BloomEffect newEffect = new BloomEffect();
            newEffect.BloomIntensity = 1;
            newEffect.BaseSaturation = 1;
            newEffect.BloomSaturation = 0.5;
            newEffect.Threshold = 0.5;
            button.Effect = newEffect;
        }

        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Effect = null;
        }

        private void ChangePictureboxState()
        {
            _pictureboxStateChanged = true;

            for (int i = 1; i < GlobalParameter.grid_num_x; ++i)
            {
                PictureGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 1; i < GlobalParameter.grid_num_y; ++i)
            {
                PictureGrid.RowDefinitions.Add(new RowDefinition());
            }

            TextBlock textBlock_chTitle = PictureGrid.FindName("textBlock_chTitle") as TextBlock;
            TextBlock textBlock_enTitle = PictureGrid.FindName("textBlock_enTitle") as TextBlock;
            if(textBlock_chTitle!=null)
                PictureGrid.Children.Remove(textBlock_chTitle);
            if(textBlock_enTitle!=null)
                PictureGrid.Children.Remove(textBlock_enTitle);

            System.Drawing.Color pictureboxColor = GlobalParameter.PictureboxColor;
            //PictureGrid.Background = new SolidColorBrush((Color.FromRgb(pictureboxColor.R,pictureboxColor.G,pictureboxColor.B)));

//             RadialGradientBrush rgb = new RadialGradientBrush();
//             System.Drawing.Color gColor = System.Drawing.Color.BurlyWood;
//             rgb.Center = new Point(0.35, 0.15);
//             rgb.RadiusX = 0.5;
//             rgb.RadiusY = 0.5;
//             rgb.GradientStops.Add(new GradientStop((Color.FromArgb(pictureboxColor.A, pictureboxColor.R, pictureboxColor.G, pictureboxColor.B)), 0.7));
//             rgb.GradientStops.Add(new GradientStop((Color.FromArgb(gColor.A, gColor.R, gColor.G, gColor.B)), 1));
//             for (int i=0;i<GlobalParameter.grid_num_x;++i)
//             {
//                 for (int j = 0; j < GlobalParameter.grid_num_y; ++j)
//                 {
//                     Rectangle rec = new Rectangle();
//                     rec.Fill = rgb;
//                     rec.SetValue(Grid.ColumnProperty, i);
//                     rec.SetValue(Grid.RowProperty, j);
//                     PictureGrid.Children.Add(rec);
//                 }
//             }

            BitmapImage bi = new BitmapImage(new Uri("gameBackground.bmp",UriKind.RelativeOrAbsolute));
            PictureGrid.Background = new ImageBrush(bi);
        }
        
    }
}
