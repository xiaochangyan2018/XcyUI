namespace XchyUI.Components.utils
{
    public class IdGenerator
    {
        private static int _lastId;
        private static readonly object _lock = new object();

        public static int NextId()
        {
            lock (_lock)
            {
                return ++_lastId;
            }
        }
        public static int CurrentId => _lastId;
    }
}
