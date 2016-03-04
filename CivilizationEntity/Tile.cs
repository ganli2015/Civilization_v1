using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GameEntity;

namespace CivilizationEntity
{
    public class Tile
    {
        Alive _alive;
        Environ _environ;

        List<Tile> _neighbours;

        public Tile()
        {

        }

        public Alive GetAlive()
        {
            return _alive;
        }

        public void SetAlive(Alive alive)
        {
            _alive = alive;
            if(_environ==null)
            {
                _alive.SetEnviron(Element.None);
            }
            else
            {
                _alive.SetEnviron(_environ.GetElementType());
            }
        }

        public bool IsAlivePossessed()
        {
            if (_alive == null)
            {
                return false;
            }
            else
                return true;
        }

        public Environ GetEnviron()
        {
            return _environ;
        }

        public void SetEnviron(Environ environ)
        {
            _environ = environ;
        }

        public bool IsEnvironPossessed()
        {
            if (_environ == null)
            {
                return false;
            }
            else
                return true;
        }

        public void RemoveAlive()
        {
            _alive = null;
        }

        public void RemoveEnviron()
        {
            _environ = null;
        }

        public void SetNeighbours(List<Tile> tiles)
        {
            _neighbours = tiles;
        }

        public void Draw()
        {
            if (_environ != null)
                _environ.Draw();

            if (_alive != null)
                _alive.Draw();

        }

        public void Paint()
        {
            if (_environ != null)
                _environ.Paint();

            if (_alive != null)
                _alive.Paint();

        }

        public Tile Clone()
        {
            Tile tile = new Tile();
            if(_alive!=null)
                tile._alive = _alive.Clone();
            if(_environ!=null)
                tile._environ = _environ.Clone();

            return tile;
        }

        public void Save(string filename)
        {

            if(_alive!=null)
                _alive.Save(filename);

            if(_environ!=null)
                _environ.Save(filename);
        }

        public MessageSet Update()
        {
            UpdateCIVProperty();

            SimpleInteraction simpleInteraction=new SimpleInteraction(ref _alive,ref _environ);
            simpleInteraction.Perform();
            foreach (Tile tile in _neighbours)
            {
                Environ neigbour_environ=tile.GetEnviron();
                Alive neighbour_alive = tile.GetAlive();
                Interaction_Alive_NeighbourAlive(ref _alive, ref neighbour_alive);
                Interaction_Alive_NeighbourEnviron(ref _alive, ref neigbour_environ);
                Interaction_Environ_NeighbourAlive(ref _environ, ref neighbour_alive);
                Interaction_Environ_NeighbourEnviron(ref _environ, ref neigbour_environ);
            }

            MessageSet messageset_total = new MessageSet();
            DealAliveMessages(ref messageset_total);
            DealEnvironMessages(ref messageset_total);
            

            return messageset_total;
        }

        void UpdateCIVProperty()
        {
            Human myHuman = _alive as Human;
            if (myHuman == null || _neighbours.Count==0) return;

            double mean_agri=0;
            double mean_cul = 0;
            double mean_indu = 0;
            double mean_mili = 0;
            double mean_tech = 0;

            int validNeighbourCount = 0;
            foreach (Tile tile in _neighbours)
            {
                Human human=tile._alive as Human;
                if(human==null) continue;
                if (human.CivIndex != myHuman.CivIndex) continue;

                mean_agri += human.Agriculture;
                mean_cul += human.Culture ;
                mean_indu += human.Industry ;
                mean_mili += human.Military ;
                mean_tech += human.Technology;

                validNeighbourCount++;
            }

            if (validNeighbourCount != 0)
            {
                mean_agri /= validNeighbourCount;
                mean_cul /= validNeighbourCount;
                mean_indu /= validNeighbourCount;
                mean_mili /= validNeighbourCount;
                mean_tech /= validNeighbourCount;


                double k = GameParameter.CIVPropertyTranslationCoefficient;

                myHuman.Agriculture = myHuman.Agriculture + k * (mean_agri - myHuman.Agriculture);
                myHuman.Culture = myHuman.Culture + k * (mean_cul - myHuman.Culture);
                myHuman.Industry = myHuman.Industry + k * (mean_indu - myHuman.Industry);
                myHuman.Military = myHuman.Military + k * (mean_mili - myHuman.Military);
                myHuman.Technology = myHuman.Technology + k * (mean_tech - myHuman.Technology);
            }
            

        }

        void DealAliveMessages(ref MessageSet messageset_total)
        {
            if (_alive != null)
            {
                MessageSet messageset_alive = _alive.Update();
                List<GameMessage> messageList = messageset_alive.GetMessages();
                if (messageList.Count > 0)
                {
                    ActionMessage firstMessage = messageList[0].GetMessageType();
                    if (firstMessage == ActionMessage.AllDead)
                    {
                        _alive = null;
                    }
                }
                messageset_total.Add(messageList);
            }
        }

        void DealEnvironMessages(ref MessageSet messageset_total)
        {
            if (_environ != null)
            {
                MessageSet messageset_environ = _environ.Update();
                List<GameMessage> messageList = messageset_environ.GetMessages();
                if (messageList.Count > 0)
                {
                    ActionMessage firstMessage = messageList[0].GetMessageType();
                    if (firstMessage == ActionMessage.AllDead)
                    {
                        _environ = null;
                    }
                }
                messageset_total.Add(messageList);
            }
        }

        void Interaction_Alive_NeighbourAlive(ref Alive alive, ref Alive neighbour_alive)
        {

        }

        void Interaction_Alive_NeighbourEnviron(ref Alive alive, ref Environ neigbour_environ)
        {
            if (neigbour_environ == null || alive == null) return;

            if (alive.GetElementType() == Element.Human && neigbour_environ.GetElementType() == Element.Water)
            {
                SimpleInteraction inter_human_water = new SimpleInteraction(ref alive, ref neigbour_environ);
                inter_human_water.Perform();
            }
            
        }

        void Interaction_Environ_NeighbourAlive(ref Environ environ, ref Alive neighbour_alive)
        {
            if (environ == null || neighbour_alive == null) return;

            if (environ.GetElementType() == Element.Water && neighbour_alive.GetElementType() == Element.Human)
            {
                SimpleInteraction inter_human_water = new SimpleInteraction(ref neighbour_alive, ref environ);
                inter_human_water.Perform();
            }
        }

        void Interaction_Environ_NeighbourEnviron(ref Environ environ, ref Environ neigbour_environ)
        {

        }
        
    }
}
