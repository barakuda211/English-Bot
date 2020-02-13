using System; 
using System.Collections.Generic;
using Dictionary; 
using Project_Word;
using System.IO;

namespace English_Bot
{
    public class Dictionary
    {
        private Dictionary<long, Word> dict;
        private Dictionary<string, List<long>> eng_ids; 
        private Dictionary<string, List<long>> rus_ids; 
        
        public Dictionary()
        {
            dict = new Dictionary<long, Word>();
            eng_ids = new Dictionary<string, List<long>>();
            rus_ids = new Dictionary<string, List<long>>();
            Console.WriteLine(Environment.CurrentDirectory);
            string dir = Environment.CurrentDirectory;
            for (int i = 1; i <= 4; ++i)
                dir = Directory.GetParent(dir).ToString();
            Console.WriteLine(dir);
            foreach (var word in Methods.DeSerialization<Word>(dir + @"/Json dicts/eng_words_100"))
            {
                dict.Add(word.id, word);
                if (eng_ids.ContainsKey(word.eng))
                    eng_ids[word.eng].Add(word.id);
                else
                {
                    var l = new List<long>();
                    l.Add(word.id);
                    eng_ids.Add(word.eng, l);
                }

                if (rus_ids.ContainsKey(word.rus))
                    rus_ids[word.rus].Add(word.id);
                else
                {
                    var l = new List<long>();
                    l.Add(word.id);
                    rus_ids.Add(word.rus, l);
                }
            }
        }
        /// <summary>
        /// индексация с 1
        /// </summary>
        /// <param name="idex"></param>
        /// <returns></returns>
        public Word this[long idex]
        {

            set { dict[idex] = value; }
            get { return dict.ContainsKey(idex) ? dict[idex] : null; }
        }

        public List<long> GetIds()
        {
            long[] arr = new long[dict.Count];
            dict.Keys.CopyTo(arr, 0);
            return new List<long>(arr);
        }

        public Word GetWord(long id)
        {
            if (dict.ContainsKey(id))
                return dict[id];
            else
                return null;
        }

        /// <summary>
        /// получаем id английских слова  по нашему запросу  
        /// </summary>
        public List<long> GetWordEng(string word)
        {
            if (eng_ids.ContainsKey(word))
                return eng_ids[word];
            else
                return null;
        }
        /// <summary>
        /// получаем id русских слова  по нашему запросу  
        /// </summary>
        public List<long> GetWordRus(string word)
        {
            if (rus_ids.ContainsKey(word))
                return rus_ids[word];
            else
               return null; 
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