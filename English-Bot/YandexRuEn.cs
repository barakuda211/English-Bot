using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceYandexRuEn
{
    public class Head
    {
    }

    public class Mean
    {
        public string text { get; set; }
    }

    public class Tr2
    {
        public string text { get; set; }
    }

    public class Ex
    {
        public string text { get; set; }
        public List<Tr2> tr { get; set; }
    }

    public class Syn
    {
        public string text { get; set; }
        public string pos { get; set; }
    }

    public class Tr
    {
        public string text { get; set; }
        public string pos { get; set; }
        public List<Mean> mean { get; set; }
        public List<Ex> ex { get; set; }
        public List<Syn> syn { get; set; }
    }

    public class Def
    {
        public string text { get; set; }
        public string pos { get; set; }
        public string gen { get; set; }
        public string anm { get; set; }
        public List<Tr> tr { get; set; }
    }

    public class YandexRuEn
    {
        public Head head { get; set; }
        public List<Def> def { get; set; }
    }
}
