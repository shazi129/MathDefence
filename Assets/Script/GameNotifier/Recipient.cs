
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZWGames
{
    public class Recipient
    {

        private Dictionary<int, Action<INotifyData>> _notifyAction = new Dictionary<int, Action<INotifyData>>();


        public void removeAllNotify()
        {
            //释放所有监听的数据
            GameNotifier.getInstance().removeRecipient(this);

            //_notifyAction.Clear();
        }

        public void addNotify(NotifyId id, Action<INotifyData> action)
        {
            if (_notifyAction.ContainsKey((int)id))
            {
                Debug.LogError("NotifyId:" + id + "already existed");
                return;
            }
            _notifyAction.Add((int)id, action);

            GameNotifier.getInstance().addRecipient((int)id, this);
        }


        virtual public void onNotify(INotifyData msg)
        {
            if (_notifyAction.ContainsKey(msg.id) && _notifyAction[msg.id] != null)
            {
                _notifyAction[msg.id](msg);
            }
        }
    }
}
