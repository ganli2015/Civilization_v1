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
//     class CIVAliveEntity: AliveEntity
//     {
//         Dictionary<Point,Alive> _alives;
//         PictureBox _picturebox;
// 
//         public CIVAliveEntity()
//         {
//             _alives = new Dictionary<Point, Alive>();
//         }
// 
//         public void Add(Alive alive)
//         {
//             if (!_alives.ContainsKey(alive.GetLocationIndex()))
//             {
//                 alive.SetPictureBox(ref _picturebox);
//                 _alives.Add(alive.GetLocationIndex(), alive);
//             }
//         }
// 
//         public void Set(Alive alive)
//         {
//             _alives[alive.GetLocationIndex()] = alive;
//         }
// 
//         public bool GetAlive(int x, int y, out Alive alive)
//         {
//             Point p=new Point(x,y);
//             if (_alives.ContainsKey(p))
//             {
//                 alive = _alives[p];
//                 return true;
//             }
//             else
//             {
//                 alive = null;
//                 return false;
//             }
//         }
// 
//         public bool IsPossessed(Point p)
//         {
//             if (_alives.ContainsKey(p))
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
//             foreach (KeyValuePair<Point,Alive> pair in _alives)
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
//                 _alives.Remove(p);
// 
//                 Graphics g = _picturebox.CreateGraphics();
//                 RectangleF recf=new RectangleF(CommonFunctions.ComputeRectangleLocation(p.X,p.Y),new SizeF(GlobalParameter.GridLength,GlobalParameter.GridLength));
//                 g.FillRectangle(new SolidBrush(SystemColors.Control), recf);
//             }
// 
//             return messageset;
//           
//         }
// 
//         public AliveEntity Clone()
//         {
//             CIVAliveEntity entity = new CIVAliveEntity();
//             entity._alives = _alives;
//             entity._picturebox = _picturebox;
//             return entity;
//         }
// 
//         public void Draw()
//         {
//             foreach (KeyValuePair<Point, Alive> pair in _alives)
//             {
//                 pair.Value.Draw();
//             }
//         }
// 
//         public void Paint()
//         {
//             foreach (KeyValuePair<Point, Alive> pair in _alives)
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
//             _alives.Clear();
//         }
//         
//     }
// }
