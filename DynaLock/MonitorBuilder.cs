using DynaLock.Framework;

namespace DynaLock
{
    public class MonitorBuilder : IBuilder<Monitor>
    {
        private string _name = string.Empty;
        private Context.Monitor _context = null;

        public Monitor Build()
        {
            return new Monitor(_context, _name);
        }

        public MonitorBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        public MonitorBuilder Context(Context.Monitor value)
        {
            this._context = value;
            return this;
        }
    }
}
