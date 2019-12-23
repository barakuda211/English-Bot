using System.Collections.Generic;
using System.IO;

namespace English_Bot
{
    public class WordsDictionary
    {
        private Dictionary<int, Word> dict = new Dictionary<int, Word>();

        private Word WordFromLine(string line)
        {
            string[] x = line.Split("  ");
            return new Word(int.Parse(x[0]),x[1],x[2],x[3]);
        }

        public void Init_dict(string fname="..//5000.txt")
        {
            var lines = File.ReadAllLines(fname);
            foreach (string line in lines)
            {
                Word x = WordFromLine(line);
                dict.Add(x.id, x);
            }
            System.Console.WriteLine("wordsDictionary Inited");
        }

        public WordsDictionary() { }

        /*
        public Word this[long index]
        {

            set { dict[index] = value; }
            get { return dict[index]; }
        }
        */
        public Word GetWord(int id)
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
            //работает ,но вроде как такие запросы выполняются медленнее чем обычный цикл
            // var lst = dict.Select(t=>t.Value).Where(d => d.eng.Contains(word));

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

        //
        public bool AddWord(Word w)
        {
            if (!dict.ContainsKey(w.id))
            {
                dict.Add(w.id, w);
                return true;
            }
            return false;
        }

        //Удалить слово
        public bool DeleteWord(int id)
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