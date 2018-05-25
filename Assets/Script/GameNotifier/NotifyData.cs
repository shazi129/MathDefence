
namespace ZWGames
{
    public class INotifyData
    {
        public int id = -1;
    }

    public class NotifyData<T> : INotifyData
    {
        public T data;
    }

}
