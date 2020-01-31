using System;
using System.Collections.Generic;
using Project_Word;
using System.Linq;
using System.Text.Json; 

namespace Project
{
    public class Dictionary
    {
        private WordsDictionary<long, Word> dict;
        private EngDictionary<string, long> eng_dict; 
        private RusDictionary<string, long> rus_dict;

        public Dictionary()
        {
            dict = new Dictionary<long, Word>();
            eng_dict = new Dictionary<string, long>(); 
            rus_dict = new Dictionary<string, long>(); 

            dict = JsonSerializer.Deserialize<Dictionary<long, Word>>(new FileStream("words_dict.json", FileMode.Open));
        }
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

            ////работает ,но вроде как такие запросы выполняются медленнее чем обычный цикл
            //var lst = dict.Select(t => t.Value).Where(d => d.rus.Contains(word));
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
    }

}