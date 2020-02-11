using System.Collections.Generic;
using System.IO;

namespace English_Bot
{
    public class WordsDictionary
    {
        private Dictionary<long, Word> dict = new Dictionary<long, Word>();

        private int added_id = 0; //костыль для инициализации словаря

        private void AddWordsFromLine(string line)
        {
            int n = line.IndexOf(']');
            string eng = line.Substring(0, n + 1);
            string rus = line.Substring(n + 3, line.Length - n - 4);
            string[] x = eng.Split("  ");
            string[] y = rus.Split(", ");
            foreach (var z in y)
            {
                int id = int.Parse(x[0]);
                dict.Add(id + added_id, new Word(id+added_id, x[1], x[2], z));
                added_id++;
            }
        }

        public void Init_dict(string fname = "..//5000.txt")
        {
            var lines = File.ReadAllLines(fname);
            foreach (string line in lines)
                AddWordsFromLine(line);
            System.Console.WriteLine("wordsDictionary Inited");
        }

        public WordsDictionary() { }


        /// <summary>
        /// индексация с 1
        /// </summary>
        /// <param name="idex"></param>
        /// <returns></returns>
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
        /// <summary>
        /// получаем английские слова  по нашему запросу  
        /// </summary>
        public Word[] GetWordEng(string word)
        {
            List<Word> lst = new List<Word>();
            
            foreach (var t in dict)
            {
                if (t.Value.eng.Contains(word.ToLower()))
                    lst.Add(t.Value);
            }
            
            return lst.ToArray();
        }
        /// <summary>
        /// получаем русские слова  по нашему запросу  
        /// </summary>
        public Word[] GetWordRus(string word)
        {
            List<Word> lst = new List<Word>();

            foreach (var t in dict)
            {
                if (t.Value.rus.Contains(word.ToLower()))
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

        /// <summary>
        /// Возвращает множество слов от английского
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public HashSet<long> GetEngId(string str)
        {
            var set = new HashSet<long>();
            foreach (var x in dict)
                if (x.Value.eng == str)
                    set.Add(x.Key);
            return set;
        }
        /// <summary>
        /// Возвращает множество слов от русского
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public HashSet<long> GetRusId(string str)
        {
            var set = new HashSet<long>();
            foreach (var x in dict)
                    if (str == x.Value.rus)
                    {
                        set.Add(x.Key);
                        break;
                    }
            return set;
        }
    }

}