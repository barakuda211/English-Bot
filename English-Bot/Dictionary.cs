﻿using System.Collections.Generic;
using Dictionary; 
//using Project_Word;

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
            foreach (var word in Methods.DeSerialization<Word>(@"eng_words"))
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
        /// получаем id английских слова  по нашему запросу  
        /// </summary>
        public List<long> GetWordEng(string word)
        {
            return eng_ids[word];
        }
        /// <summary>
        /// получаем id русских слова  по нашему запросу  
        /// </summary>
        public List<long> GetWordRus(string word)
        {
            return rus_ids[word];
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