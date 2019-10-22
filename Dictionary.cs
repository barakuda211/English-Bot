using System;
using System.Collections.Generic;
using Project_Word;
using System.Linq;
namespace Project
{
    public class Dictionary
    {
        private Dictionary<long, Word> dict;

        public Dictionary()
        {
            dict = new Dictionary<long, Word>();
        }

        public Word this[long idex]
        {

            set { dict[idex] = value; }
            get { return dict[idex]; }
        }

        public Word GetWord(long id)
        {
            if (dict.ContainsKey(id))
                return dict[id];
            else
                return null;

        }

        public Word[] GetWordEng(string word)
        {
            List<Word> lst = new List<Word>();
            
            foreach (var t in dict)
            {
                if (t.Value.GetEnginlishWord().Contains(word.ToLower()))
                    lst.Add(t.Value);
            }
            return lst.ToArray();
        }
        public Word[] GetWordRus(string word)
        {
            List<Word> lst = new List<Word>();

            foreach (var t in dict)
            {
                if (t.Value.GetRussianWord().Contains(word.ToLower()))
                    lst.Add(t.Value);
            }
            return lst.ToArray();
        }

        public bool AddWord(Word w)
        {
            if (dict.ContainsValue(w) == false)
            {
                dict.Add(dict.Count + 1, w);
                return true;
            }

            return false;

        }

        public bool DeleteWord(long id)
        {
            if (dict.ContainsKey(id) == true)
            {
                dict.Remove(id);
                return true;
            }

            return false;
        }
    }

}