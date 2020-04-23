using System;
using System.Collections.Generic;
using System.Text;

namespace English_Bot
{
    public class Gallows
    {
        public long id { get; set; }
        public bool success { get; set; }
        private Random r = new Random();

        private (string, long) word { get; set; }
    }
}
