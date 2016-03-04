using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEntity
{
    public class MessageSet
    {
        List<GameMessage> _messages;

        public MessageSet()
        {
            _messages = new List<GameMessage>();
        }

        public void Add(GameMessage message)
        {
            _messages.Add(message);
        }

        public void Add(List<GameMessage> messages)
        {
            foreach (GameMessage mes in messages)
            {
                _messages.Add(mes);
            }
        }

        public List<GameMessage> GetMessages()
        {
            return _messages;
        }
    }
}
