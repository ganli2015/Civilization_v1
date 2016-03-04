using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEntity;
using System.Windows.Forms;
using System.Drawing;

namespace CivilizationEntity
{
    public class CIVElements : GameElements
    {
//         EnvironmentEntity _environmentEntity;
//         AliveEntity _aliveEntity;

        TileEntity _tileEntity;
        GameDisplay _gameDisplay;

        public CIVElements(ref GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
            _tileEntity = new CIVTileEntity();
            _tileEntity.SetPictureBox(ref gameDisplay);
        }

        public void Add(Alive alive)
        {
            _tileEntity.Add(alive);
        }

        public void Add(GameEntity.Environ environment)
        {
            _tileEntity.Add(environment);
        }

        public void Set(Alive alive)
        {
            _tileEntity.Set(alive);
        }
        public void Set(Environ environment)
        {
            _tileEntity.Set(environment);
        }

        public void RemoveAlive(Point p)
        {
            if (!IsAlivePossessed(p)) return;

            _tileEntity.RemoveAlive(p);

        }

        public void RemoveEnviron(Point p)
        {
            if (!IsEnvironPossessed(p)) return;

            _tileEntity.RemoveEnviron(p);
        }

        public bool GetAlive(int x, int y, out Alive alive)
        {
            return _tileEntity.GetAlive(x, y, out alive);
        }

        public bool GetEnviron(int x, int y, out Environ environ)
        {
            return _tileEntity.GetEnviron(x, y, out environ);
        }

        public bool IsPossessed(Point p)
        {
            if (IsAlivePossessed(p) || IsEnvironPossessed(p))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsAlivePossessed(Point p)
        {
            return _tileEntity.IsAlivePossessed(p);
        }
        public bool IsEnvironPossessed(Point p)
        {
            return _tileEntity.IsEnvironPossessed(p);
        }

        public MessageSet Update()
        {
            MessageSet messageset = new MessageSet();

            MessageSet tile_messageset = _tileEntity.Update();

            messageset.Add(tile_messageset.GetMessages());

            return messageset;
        }

        public GameElements Clone()
        {
            CIVElements elements = new CIVElements(ref _gameDisplay);
            elements._tileEntity = _tileEntity.Clone();

            return elements;
        }

        public void Draw()
        {
            _tileEntity.Draw();
        }

        public void Paint()
        {
            _tileEntity.Paint();
        }

        public void Clear()
        {
            _tileEntity.Clear();
        }

        public void Save(string filename)
        {
            _tileEntity.Save(filename);
        }


        public List<Point> GetAliveIndexes()
        {
            return _tileEntity.GetAliveIndexes();
        }
    }
}
