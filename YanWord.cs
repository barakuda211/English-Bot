using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json; 

namespace Classes
{
    [DataContract]
    class Head
    { }

    [DataContract]
    class Synonym
    {
        [DataMember]
        public string text;

        [DataMember]
        public string pos;
        /*
        [DataMember]
        public Translation[] tr;*/

        public string GetText() => text;

        public string GetPos() => pos;

        //public Translation[] GetTr() => tr;
    }

    [DataContract]
    class Meaning
    {
        [DataMember]
        public string text;

        [DataMember]
        public string pos;

        //[DataMember]
        //public Translation[] tr;

        public string GetText() => text;

        public string GetPos() => pos;

        //public Translation[] GetTr() => tr;
    }

    [DataContract]
    class Example
    {
        [DataMember]
        public string text;

        [DataMember]
        public string pos;

        //[DataMember]
        //public Translation[] tr;

        public string GetText() => text;

        public string GetPos() => pos;

        //public Translation[] GetTr() => tr;
    }

    [DataContract]
    class Translation
    {
        [DataMember]
        public string text;

        [DataMember]
        public string pos;

        [DataMember]
        public Synonym[] syn; 

        [DataMember]
        public Meaning[] mean;

        [DataMember]
        public Example[] ex;

        public string GetText() => text;

        public string GetPos() => pos;

        public Synonym[] GetSyn() => syn;

        public Meaning[] GetMean() => mean;

        public Example[] GetEx() => ex; 
    }

    [DataContract]
    class Match
    {
        [DataMember]
        public string text;

        [DataMember]
        public string pos;

        [DataMember]
        public string ts;

        [DataMember]
        public Translation[] tr;

        public string GetText() => text;

        public string GetPos() => pos;

        public string GetTs() => ts;

        public Translation[] GetTr() => tr; 
    }

    /// <summary>
    /// Слово в формате Яндекс словаря
    /// </summary>
    [DataContract]
    class YanWordEng
    {
        [DataMember]
        public Head head;

        [DataMember]
        public Match[] def;

        public Match[] GetDef() => def; 
    }
}
