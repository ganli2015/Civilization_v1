using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using GameEntity;

namespace CivilizationEntity
{
    public class CIVTileEntity:TileEntity
    {
        Dictionary<Point,Tile> _tiles;
        GameDisplay _gameDisplay;

        List<Point> _toRemove;
        List<Point> _newAdded;

        public CIVTileEntity()
        {
            _tiles = new Dictionary<Point, Tile>();
            _toRemove = new List<Point>();
            _newAdded = new List<Point>();
        }

        public void Add(Alive alive)
        {
            if (!_tiles.ContainsKey(alive.GetLocationIndex()))
            {
                alive.SetPictureBox(ref _gameDisplay);
                Tile tile = new Tile();
                tile.SetAlive(alive);
                _tiles.Add(alive.GetLocationIndex(), tile);
            }
            else
            {
                Tile tile = _tiles[alive.GetLocationIndex()];
                if (!tile.IsAlivePossessed())
                {
                    alive.SetPictureBox(ref _gameDisplay);
                    tile.SetAlive(alive);
                    _tiles[alive.GetLocationIndex()] = tile;
                }
                
            }

            _newAdded.Add(alive.GetLocationIndex());
        }

        public void Set(Alive alive)
        {
            if (!_tiles.ContainsKey(alive.GetLocationIndex()))
            {
                alive.SetPictureBox(ref _gameDisplay);
                Tile tile = new Tile();
                tile.SetAlive(alive);
                _tiles.Add(alive.GetLocationIndex(), tile);
            }
            else
            {
                Tile tile = _tiles[alive.GetLocationIndex()];
                alive.SetPictureBox(ref _gameDisplay);
                tile.SetAlive(alive);
                _tiles[alive.GetLocationIndex()] = tile;
            }

            _newAdded.Add(alive.GetLocationIndex());

        }

        public void Add(Environ environ)
        {
            if (!_tiles.ContainsKey(environ.GetLocationIndex()))
            {
                environ.SetPictureBox(ref _gameDisplay);
                Tile tile = new Tile();
                tile.SetEnviron(environ);
                _tiles.Add(environ.GetLocationIndex(), tile);
            }
            else
            {
                Tile tile = _tiles[environ.GetLocationIndex()];
                if (!tile.IsEnvironPossessed())
                {
                    environ.SetPictureBox(ref _gameDisplay);
                    tile.SetEnviron(environ);
                    _tiles[environ.GetLocationIndex()] = tile;
                }
            }

            _newAdded.Add(environ.GetLocationIndex());

        }

        public void Set(Environ environ)
        {
            if (!_tiles.ContainsKey(environ.GetLocationIndex()))
            {
                environ.SetPictureBox(ref _gameDisplay);
                Tile tile = new Tile();
                tile.SetEnviron(environ);
                _tiles.Add(environ.GetLocationIndex(), tile);
            }
            else
            {
                Tile tile = _tiles[environ.GetLocationIndex()];
                environ.SetPictureBox(ref _gameDisplay);
                tile.SetEnviron(environ);
                _tiles[environ.GetLocationIndex()] = tile;
            }

            _newAdded.Add(environ.GetLocationIndex());

        }

        public void RemoveAlive(Point p)
        {
            Tile tile = _tiles[p];
            tile.RemoveAlive();
            _toRemove.Add(p);
        }

        public void RemoveEnviron(Point p)
        {
            Tile tile = _tiles[p];
            tile.RemoveEnviron();
            _toRemove.Add(p);
        }

        public bool GetAlive(int x, int y, out Alive alive)
        {
            Point p=new Point(x,y);
            if (_tiles.ContainsKey(p))
            {
                alive = _tiles[p].GetAlive();
                if (alive == null)
                    return false;
                else
                    return true;
            }
            else
            {
                alive = null;
                return false;
            }
        }

        public bool GetEnviron(int x, int y, out Environ environ)
        {
            Point p = new Point(x, y);
            if (_tiles.ContainsKey(p))
            {
                environ = _tiles[p].GetEnviron();
                if (environ == null)
                    return false;
                else
                    return true;
            }
            else
            {
                environ = null;
                return false;
            }
        }

        public bool IsPossessed(Point p)
        {
            if (_tiles.ContainsKey(p))
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
            if (!_tiles.ContainsKey(p)) 
                return false;
            else
            {
                Tile tile = _tiles[p];
                if (tile.IsAlivePossessed())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
        public bool IsEnvironPossessed(Point p)
        {
            if (!_tiles.ContainsKey(p))
                return false;
            else
            {
                Tile tile = _tiles[p];
                if (tile.IsEnvironPossessed())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public MessageSet Update()
        {
            SetEachTileNeighbour();

            MessageSet messageset = new MessageSet();

            _toRemove = new List<Point>();
            foreach (KeyValuePair<Point, Tile> pair in _tiles)
            {
                MessageSet tmp_messageset = pair.Value.Update();
                if (HasAllDeadMessage(tmp_messageset))
                {
                    _toRemove.Add(pair.Key);
                }
                messageset.Add(tmp_messageset.GetMessages());
            }

            return messageset;
          
        }

        public TileEntity Clone()
        {
            CIVTileEntity entity = new CIVTileEntity();
            entity._tiles = _tiles;
            return entity;
        }

        public void Draw()
        {
            foreach (Point p in _toRemove)
            {
                Tile tile = _tiles[p];
                if (tile.IsAlivePossessed() || tile.IsEnvironPossessed())
                {
                    CommonFunctions.ResetGrid(p, ref _gameDisplay);
                    tile.Paint();
                    continue;
                }
                else
                {
                    _tiles.Remove(p);
                    CommonFunctions.ResetGrid(p, ref _gameDisplay);
                }
            }

            foreach (Point p in _newAdded)
            {
                _tiles[p].Paint();
            }

            _newAdded.Clear();

            foreach (KeyValuePair<Point, Tile> pair in _tiles)
            {
                pair.Value.Draw();
            }
        }

        public void Paint()
        {
            foreach (Point p in _toRemove)
            {
                Tile tile = _tiles[p];
                if (tile.IsAlivePossessed() || tile.IsEnvironPossessed())
                {
                    CommonFunctions.ResetGrid(p, ref _gameDisplay);
                    tile.Paint();
                    continue;
                }
                else
                {
                    _tiles.Remove(p);
                    CommonFunctions.ResetGrid(p, ref _gameDisplay);
                }

            }

            foreach (Point p in _newAdded)
            {
                _tiles[p].Paint();
            }

            _newAdded.Clear();

            foreach (KeyValuePair<Point, Tile> pair in _tiles)
            {
                pair.Value.Paint();
            }
        }

        public void SetPictureBox(ref GameDisplay gameDisplay)
        {
            _gameDisplay = gameDisplay;
        }

        public void Clear()
        {
            _tiles.Clear();
        }

        public void Save(string filename)
        {
            foreach (KeyValuePair<Point,Tile> tile in _tiles)
            {
                tile.Value.Save(filename);
            }
        }

        

        public List<Point> GetAliveIndexes()
        {
            List<Point> res=new List<Point>();
            foreach (KeyValuePair<Point, Tile> pair in _tiles)
            {
                if(pair.Value.IsAlivePossessed())
                    res.Add(pair.Key);
            }

            return res;
        }

        bool HasAllDeadMessage(MessageSet messageSet)
        {
            List<GameMessage> message_list = messageSet.GetMessages();

            foreach (GameMessage message in message_list)
            {
                if (message.GetMessageType() == ActionMessage.AllDead)
                {
                    return true;
                }
            }

            return false;
        }

        void SetEachTileNeighbour()
        {
            Dictionary<Point, Tile> newTiles = new Dictionary<Point, Tile>();

            foreach (KeyValuePair<Point,Tile> pair in _tiles)
            {
                List<Point> neighbourIndex = CommonFunctions.GetNeighbourIndex(pair.Key);
                List<Tile> validNeighbours = new List<Tile>();
                foreach (Point p in neighbourIndex)
                {
                    if (_tiles.ContainsKey(p))
                    {
                        validNeighbours.Add(_tiles[p]);
                    }
                }
                Tile tile = pair.Value.Clone();
                tile.SetNeighbours(validNeighbours);
                newTiles.Add(pair.Key, tile);
            }

            _tiles = newTiles;
        }

        
    }
}
