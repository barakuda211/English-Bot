using System;
using System.Text;

namespace AudioExCS
{
    public class DictaphoneStateEventArgs : EventArgs
    {
        public DictaphoneStateEventArgs(DictaphoneState state)
        {
            _state = state;
        }

        private DictaphoneState _state;
        public DictaphoneState State
        {
            get { return _state; }
        }

    }
}
