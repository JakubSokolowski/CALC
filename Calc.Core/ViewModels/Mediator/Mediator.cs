using System;

namespace Calc.Core
{
    public sealed class Mediator
    {
        static readonly Mediator mInstance = new Mediator();
        private volatile object mLocker = new object();

        MultiDictionary<ViewModelMessages, Action<Object>> mInternalList = new MultiDictionary<ViewModelMessages, Action<object>>();

        static Mediator() { }
        private Mediator() { }

        public static Mediator Instance => mInstance;

        public void Register(Action<object> callback, ViewModelMessages message)
        {
            mInternalList.AddValue(message, callback);
        }

        public void NotifyColleagues(ViewModelMessages message, object args)
        {
            if(mInternalList.ContainsKey(message))
            {
                // forward the message to all listeners
                foreach (var callback in mInternalList[message])
                    callback(args);              
            }
        }
    }
}
