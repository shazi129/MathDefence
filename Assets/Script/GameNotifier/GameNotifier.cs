using System.Collections.Generic;

namespace ZWGames
{
    public class GameNotifier
    {
        //单例
        private static GameNotifier _instance = null;

        public static GameNotifier getInstance()
        {
            if (_instance == null)
            {
                _instance = new GameNotifier();
            }
            return _instance;
        }

        //增加接收者
        Dictionary<int, List<Recipient>> _recipientsMap = new Dictionary<int, List<Recipient>>();
        public void addRecipient(List<int> ids, Recipient recipient)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                int id = ids[i];
                addRecipient(id, recipient);
            }
        }

        //增加接收者
        public void addRecipient(int id, Recipient recipient)
        {
            if (!_recipientsMap.ContainsKey(id) || _recipientsMap[id] == null)
            {
                _recipientsMap[id] = new List<Recipient>();
            }

            List<Recipient> recipients = _recipientsMap[id];
            if (recipients.IndexOf(recipient) == -1)
            {
                recipients.Add(recipient);
            }
        }

        public void removeRecipient(Recipient recipient)
        {
            foreach(var item in _recipientsMap)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    item.Value.Remove(recipient);
                }
            }
        }

        public void notify(INotifyData msg)
        {
            if (_recipientsMap.ContainsKey(msg.id))
            {
                List<Recipient> recipients = _recipientsMap[msg.id];
                for (int i = 0; i < recipients.Count; i++)
                {
                    recipients[i].onNotify(msg);
                }
            }
        }

        public void notifydata<T>(int id, T data)
        {
            NotifyData<T> notifyData = new NotifyData<T>();
            notifyData.id = id;
            notifyData.data = data;
            notify(notifyData);
        }

        public void notifyStateChange(int id)
        {
            INotifyData notifyData = new INotifyData();
            notifyData.id = id;
            notify(notifyData);
        }

    }
}

