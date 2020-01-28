using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace SpaceYandexEnEn
{
    [DataContract]
    public class Head
    {
    }

    [DataContract]
    public class Syn
    {
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public string pos { get; set; }
    }

    [DataContract]
    public class Tr
    {
        [DataMember]
        public string pos { get; set; }
        [DataMember]
        public List<Syn> syn { get; set; }
        [DataMember]
        public string text { get; set; }
    }

    [DataContract]
    public class Def
    {
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public string pos { get; set; }
        [DataMember]
        public string ts { get; set; }
        [DataMember]
        public List<Tr> tr { get; set; }
    }

    [DataContract]
    public class YandexEnEn
    {
        [DataMember]
        public Head head { get; set; }
        [DataMember]
        public List<Def> def { get; set; }
    }
}
