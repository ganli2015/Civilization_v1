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
using Evolution;
using System.Windows.Shapes;
using System.Threading;
using System.IO;

namespace Civilization
{
    public partial class MainForm : Form
    {

        GameElements _gameElements;
        GameMain _gameMain;
        GameDisplay _gameDisplay;
        Thread _mainthread;

        ConfigureForm _config;
        GameInformation _gameInfo;

        public MainForm()
        {
            InitializeComponent();

            Initialize();

            _config = new ConfigureForm();
            _gameInfo = new GameInformation();

            _gameDisplay = new GameDisplayImp1(ref pictureBox);

            _gameElements = new CIVElements(ref _gameDisplay);

            _gameMain = new GameMain(ref _gameDisplay);

            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Initialize()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
                pictureBox.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - pictureBox.Left-8, Screen.PrimaryScreen.WorkingArea.Height - pictureBox.Top-6);
            }

            comboBox_Language.SelectedIndex = 0;

            InitializePictureBox();


            {
                MapGenerator mapGenerator=new MapGenerator(ref _gameDisplay);
                //mapGenerator.AllGrass();
                //mapGenerator.IslandInLake();
                //mapGenerator.RandomIsland();
            }
        }

        private void InitializePictureBox()
        {
            GlobalParameter.valid_width = pictureBox.Width - 10;
            GlobalParameter.valid_height = pictureBox.Height - 40;

            Bitmap bm = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bm);
            g.DrawRectangle(new Pen(Color.Black), 0, 0, GlobalParameter.valid_width, GlobalParameter.valid_height);
            pictureBox.BackgroundImage = bm;
            pictureBox.BackgroundImageLayout = ImageLayout.Center;

            GlobalParameter.grid_num_x = (int)((double)(GlobalParameter.valid_width) / GameEntity.GlobalParameter.GridLength);
            GlobalParameter.grid_num_y = (int)((double)(GlobalParameter.valid_height) / GameEntity.GlobalParameter.GridLength);
            GlobalParameter.valid_top = pictureBox.Top;
            GlobalParameter.valid_left = pictureBox.Left;
        }

        private void InitializeLanguage()
        {
            LanguageManager lm = LanguageManager.GetInstance();

            this.Text = lm.Get("Title_MainForm");
            StartButton.Text = lm.Get("Button_Start");
            button_Configure.Text = lm.Get("Button_Configure");
            button_Pause.Text = lm.Get("Button_Pause");
            button_Reset.Text = lm.Get("Button_Reset");
            button_Save.Text = lm.Get("Button_Save");
            button_Load.Text = lm.Get("Button_Load");
            label_SpeedControl.Text = lm.Get("Label_SpeedContral");
        }

        private void button_Pause_Click(object sender, EventArgs e)
        {
            if (_mainthread == null) return;

            LanguageManager lm = LanguageManager.GetInstance();

            if (button_Pause.Text == lm.Get("Button_Pause"))
            {
                while (GlobalParameter.main_running) ;
                if(_mainthread!=null)
                    _mainthread.Suspend();
                button_Pause.Text = lm.Get("Button_Resume");

                if (_mainthread != null)
                {
                    button_Configure.Enabled = true;
                    button_Configure.ForeColor = Color.Black;
                }
            }
            else
            {
                if(_mainthread!=null)
                    _mainthread.Resume();
                button_Pause.Text = lm.Get("Button_Pause");

                if (_mainthread != null)
                {
                    button_Configure.Enabled = false;
                    button_Configure.ForeColor = Color.Silver;
                }
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            StartButton.ForeColor = Color.Silver;
            button_Configure.Enabled = false;
            button_Configure.ForeColor = Color.Silver;

            _mainthread = new Thread(Start);
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
                _gameMain.Start(ref _gameElements);
                GlobalParameter.main_running = false;
                while ((DateTime.Now - start).Milliseconds < GlobalParameter.GameRound_Interval) ;

                if (_gameInfo.Visible == true)
                {
                    _gameInfo.RefreshGameElements(ref _gameElements);
                    _gameInfo.UpdateUI();
                }
            }
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            string initPath = System.AppDomain.CurrentDomain.BaseDirectory;
            initPath += "Save";
            if (!Directory.Exists(initPath))
            {
                Directory.CreateDirectory(initPath);
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = initPath;
            sfd.Filter = "civ文件(*.civ)|*.civ";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                IOManager.SaveAsFile(sfd.FileName, ref _gameElements);
            }

        }

        private void button_Load_Click(object sender, EventArgs e)
        {
            string initPath = System.AppDomain.CurrentDomain.BaseDirectory;
            initPath += "Save";
            if (!Directory.Exists(initPath))
            {
                Directory.CreateDirectory(initPath);
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = initPath;
            openFileDialog.Filter = "civ文件(*.civ)|*.civ";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IOManager.LoadFile(openFileDialog.FileName, ref _gameElements);
            }

            _gameElements.Paint();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

            if (_mainthread == null)
            {
                _gameElements.Paint();
            }


        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            LanguageManager lm = LanguageManager.GetInstance();

            if (e.Button == MouseButtons.Right)
            {
                GridIndex index = GetGridIndex(e.Location);
                string messege = "";
                Alive alive;
                if (_gameElements.GetAlive(index.x, index.y, out alive))
                {
                    Human hu = (Human)alive;
                    messege += lm.Get("Human_Population") + "   " + Convert.ToString(hu.Population) + "\r\n";
                    messege += lm.Get("Human_Civilization") + "   " + Convert.ToString(hu.GetCivIndex());
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
                                messege += lm.Get("Grass_Food") + "   " + Convert.ToString(grass.Food);
                                break;
                            }
                        case Element.Water:
                            {
                                Water water = environ as Water;
                                messege += lm.Get("Water_Seafood") + "   " + Convert.ToString(water.Seafood);
                                break;
                            }
                    }
                    
                }

                this.toolTip1.Show(messege, this.pictureBox, e.Location.X, e.Location.Y, 2000);

            }
            else
            {
                this.toolTip1.Hide(pictureBox);
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            this.pictureBox.Focus();

            if (_config.Visible)
            {
                Element element ;
                if(!_config.GetElementSymbol(out element)) return;
                if (IsAlive(element))
                {
                    MouseEventArgs mouse_e = (MouseEventArgs)e;
                    if (mouse_e.X < GlobalParameter.valid_width && mouse_e.Y < GlobalParameter.valid_height)
                    {
                        GridIndex index = GetGridIndex(mouse_e.Location);
                        AddAliveToEntity(index, ref _gameElements, element);
                    }
                }
                else if (IsEnviron(element))
                {
                    MouseEventArgs mouse_e = (MouseEventArgs)e;
                    if (mouse_e.X < GlobalParameter.valid_width && mouse_e.Y < GlobalParameter.valid_height)
                    {
                        GridIndex index = GetGridIndex(mouse_e.Location);
                        AddEnvironToEntity(index, ref _gameElements, element);
                    }
                }
            }
        }

        private void button_Configure_Click(object sender, EventArgs e)
        {
            _config.Location = this.Location;
            _config.Visible = true;
            _config.InitializeLanguage();
            _config.Show();
        }

        private void button_Reset_Click(object sender, EventArgs e)
        {
            if(_mainthread!=null)
            {
                ThreadState state=_mainthread.ThreadState;
                if(state==(ThreadState.Background | ThreadState.Suspended))
                {
                    _mainthread.Resume();
                    _mainthread.Abort();
                }
                else
                    _mainthread.Abort();
            }

            _gameElements.Clear();
            StartButton.Enabled = true;
            StartButton.ForeColor = SystemColors.ControlText;
            button_Configure.Enabled = true;
            button_Configure.ForeColor = SystemColors.ControlText;
            Graphics g = pictureBox.CreateGraphics();
            g.Clear(SystemColors.Control);
            InitializePictureBox();

            GameParameter.CivNum = 0;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (_config.Visible == false)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                Element element;
                if (!_config.GetElementSymbol(out element)) return;

                if (e.Location.X >= GlobalParameter.valid_width && e.Location.Y >= GlobalParameter.valid_height) return;

                if(IsAlive(element))
                {
                    GridIndex index = GetGridIndex(e.Location);
                    AddAliveToEntity(index, ref _gameElements, element);
                }
                else if (IsEnviron(element))
                {
                    GridIndex index = GetGridIndex(e.Location);
                    AddEnvironToEntity(index, ref _gameElements, element);
                    
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                GlobalParameter.resizing = true;
            }

        }

        private void DrawCircle(Point center, float radius)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.FillEllipse(new SolidBrush(Color.Red), (float)center.X, (float)center.Y, 2*radius, 2*radius);
        }
        private void DrawCircle(PointF center, float radius,Color color)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.FillEllipse(new SolidBrush(color), center.X, center.Y, 2*radius, 2*radius);
        }

        private void DrawRectangle(PointF location, float size,Color color)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.FillRectangle(new SolidBrush(color), location.X,location.Y,size,size);
        }

        private GridIndex GetGridIndex(Point p)
        {
            GridIndex index;
            double px = (double)p.X;
            double py = (double)p.Y;
            double tmpx=0,tmpy=0;
            int index_x=-1, index_y=-1;
            while (tmpx <px)
            {
                tmpx += GlobalParameter.GridLength;
                ++index_x;
            }
            while (tmpy < py)
            {
                tmpy += GlobalParameter.GridLength;
                ++index_y;
            }

            index.x = index_x;
            index.y = index_y;

            return index;
        }

        private void AddAliveToEntity(GridIndex index, ref GameElements gameElements, Element element)
        {
            AliveFactory factory = new AliveFactory();
            Alive alive=factory.CreateAlive(element);
            alive.SetLocationIndex(index.x, index.y);
            gameElements.Add(alive);
            DrawCircle(ComputeCircleCenter(index), GlobalParameter.GridLength / 2,alive.GetColor());
        }

        private void AddEnvironToEntity(GridIndex index, ref GameElements gameElements, Element element)
        {
            EnvironFactory factory = new EnvironFactory();
            Environ environ = factory.CreateEnviron(element);
            environ.SetLocationIndex(index.x, index.y);
            gameElements.Set(environ);
            Point p=new Point();
            p.X=index.x;
            p.Y=index.y;

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

        private PointF ComputeCircleCenter(GridIndex index)
        {
            float px=(float)(index.x  * GlobalParameter.GridLength);
            float py=(float)(index.y  * GlobalParameter.GridLength);
            return new PointF(px,py);
        }

        private PointF ComputeRectangleLocation(GridIndex index)
        {
            float px = (float)(index.x * GlobalParameter.GridLength);
            float py = (float)(index.y * GlobalParameter.GridLength);
            return new PointF(px, py);
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
            else
            {
                return false;
            }
        }

        private void InitializeColorManager()
        {
            ColorManager colorManager = ColorManager.GetInstance();
            
        }

        private void comboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_Language.SelectedIndex == 0)
            {
                LanguageManager lm = LanguageManager.GetInstance();
                lm.SetLanguage(Language.Chinese);


                InitializeLanguage();
            }
            else if (comboBox_Language.SelectedIndex == 1)
            {
                LanguageManager lm = LanguageManager.GetInstance();
                lm.SetLanguage(Language.English);


                InitializeLanguage();
            }
        }

        private void hScrollBar_SpeedControl_Scroll(object sender, ScrollEventArgs e)
        {
            GlobalParameter.GameRound_Interval = 999 - e.NewValue*10;
        }

        private void button_GameInfo_Click(object sender, EventArgs e)
        {
            _gameInfo.Location = this.Location;
            _gameInfo.Visible = true;
            _gameInfo.Show();
        }

        


    }
}
