using System;
using System.Text;

namespace AudioExCS
{
    public class PositionEventArgs : EventArgs
    {
        public PositionEventArgs(long position)
        {
            _position = position;
        }

        private long _position;
        public long Position
        {
            get { return _position; }
        }

    }
}
