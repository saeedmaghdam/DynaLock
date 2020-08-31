using System;
using System.Collections.Generic;
using System.Text;

namespace DynaLock
{
    public class SemaphoreBuilder
    {
        private string _name = string.Empty;
        private Context.Semaphore _context = null;
        private int _initialCount = 1;
        private int _maximumCount = 1;

        public Semaphore Build()
        {
            return new Semaphore(_context, _name, _initialCount, _maximumCount);
        }

        public SemaphoreBuilder Name(string value)
        {
            this._name = value;
            return this;
        }

        public SemaphoreBuilder Context(Context.Semaphore value)
        {
            this._context = value;
            return this;
        }

        public SemaphoreBuilder InitialCount(int value)
        {
            this._initialCount = value;
            return this;
        }

        public SemaphoreBuilder MaximumCount(int value)
        {
            this._maximumCount = value;
            return this;
        }
    }
}
