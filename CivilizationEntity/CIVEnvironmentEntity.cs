// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using GameEntity;
// using System.Drawing;
// using System.Windows.Forms;
// 
// namespace CivilizationEntity
// {
//     public class CIVEnvironmentEntity:EnvironmentEntity
//     {
//         Dictionary<Point, Environ> _environ;
//         PictureBox _picturebox;
// 
//         public CIVEnvironmentEntity()
//         {
//             _environ = new Dictionary<Point, Environ>();
//         }
// 
//         public void Add(Environ environ)
//         {
//             if (!_environ.ContainsKey(environ.GetLocationIndex()))
//             {
//                 _environ.Add(environ.GetLocationIndex(), environ);
//             }
//         }
// 
//         public void Set(Environ environment)
//         {
//             _environ[environment.GetLocationIndex()] = environment;
//         }
// 
//         public bool GetEnviron(int x, int y, out Environ environ)
//         {
//             Point p = new Point(x, y);
//             if (_environ.ContainsKey(p))
//             {
//                 environ = _environ[p];
//                 return true;
//             }
//             else
//             {
//                 environ = null; 
//                 return false;
//             }
//         }
// 
//         public bool IsPossessed(Point p)
//         {
//             if (_environ.ContainsKey(p))
//             {
//                 return true;
//             }
//             else
//             {
//                 return false;
//             }
//         }
// 
//         public MessageSet Update()
//         {
//             MessageSet messageset = new MessageSet();
// 
//             List<Point> toRemove = new List<Point>();
//             foreach (KeyValuePair<Point, Environ> pair in _environ)
//             {
//                 MessageSet tmp_messageset = pair.Value.Update();
//                 List<GameMessage> message_list = tmp_messageset.GetMessages();
//                 if (message_list.Count > 0)
//                 {
//                     ActionMessage firstMessage = message_list[0].GetMessageType();
//                     if (firstMessage == ActionMessage.AllDead)
//                     {
//                         toRemove.Add(pair.Key);
//                     }
//                 }
//                 messageset.Add(tmp_messageset.GetMessages());
//             }
// 
//             foreach (Point p in toRemove)
//             {
//                 _environ.Remove(p);
// 
//                 Graphics g = _picturebox.CreateGraphics();
//                 RectangleF recf = new RectangleF(CommonFunctions.ComputeRectangleLocation(p.X, p.Y), new SizeF(GlobalParameter.GridLength, GlobalParameter.GridLength));
//                 g.FillRectangle(new SolidBrush(SystemColors.Control), recf);
//             }
// 
//             return messageset;
//         }
// 
//         public EnvironmentEntity Clone()
//         {
//             CIVEnvironmentEntity entity = new CIVEnvironmentEntity();
//             entity._environ = _environ;
//             entity._picturebox = _picturebox;
// 
//             return entity;
//         }
// 
//         public void Draw()
//         {
//             foreach (KeyValuePair<Point, Environ> pair in _environ)
//             {
//                 pair.Value.Draw();
//             }
//         }
// 
//         public void Paint()
//         {
//             foreach (KeyValuePair<Point, Environ> pair in _environ)
//             {
//                 pair.Value.Paint();
//             }
//         }
// 
//         public void SetPictureBox(ref PictureBox picturebox)
//         {
//             _picturebox = picturebox;
//         }
// 
//         public void Clear()
//         {
//             _environ.Clear();
//         }
//     }
// }
