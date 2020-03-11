﻿using System; 
using System.Collections.Generic;
using Dictionary; 
using Project_Word;
using System.IO;

namespace English_Bot
{
    public class WordsDictionary
    {
        private Dictionary<long, Word> dict;
        private Dictionary<string, List<long>> eng_ids; 
        private Dictionary<string, List<long>> rus_ids;
        private int id = 0;
        
        public WordsDictionary()
        {
            dict = new Dictionary<long, Word>();
            
            eng_ids = new Dictionary<string, List<long>>();
            rus_ids = new Dictionary<string, List<long>>();
            string dir = Users.GetPathOfFile(Environment.CurrentDirectory);
            //foreach (var word in Methods.DeSerialization<Word>(dir + @"/eng_words_100"))
            foreach (var word in Methods.DeSerialization<Word>(dir + @"..\Json dicts\eng_words"))
            {
                dict.Add(id, word);
                if (eng_ids.ContainsKey(word.eng))
                    eng_ids[word.eng].Add(id);
                else
                {
                    var l = new List<long>();
                    l.Add(id);
                    eng_ids.Add(word.eng, l);
                }

                if (rus_ids.ContainsKey(word.rus))
                    rus_ids[word.rus].Add(id);
                else
                {
                    var l = new List<long>();
                    l.Add(id);
                    rus_ids.Add(word.rus, l);
                }
                id++;
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

        /// <summary>
        /// получаем id английских слова  по нашему запросу  
        /// </summary>
        public List<long> GetEngWordIds(string word)
        {
            if (eng_ids.ContainsKey(word))
                return eng_ids[word];
            else
                return null;
        }

        /// <summary>
        /// получаем id русских слова  по нашему запросу  
        /// </summary>
        public List<long> GetRusWordIds(string word)
        {
            if (rus_ids.ContainsKey(word))
                return rus_ids[word];
            else
                return null;
        }

        public Word GetWord(long id)
        {
            if (dict.ContainsKey(id))
                return dict[id];
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

        private void AddWordsFromLine(string line)
        {
            int n = line.IndexOf(']');
            string eng = line.Substring(0, n + 1);
            string rus = line.Substring(n + 3, line.Length - n - 4);
            string[] x = eng.Split("  ");
            string[] y = rus.Split(", ");
            foreach (var z in y)
            {
                dict.Add(id, new Word(id, x[1], x[2], z));
                id++;
            }
        }

        public void Init_dict(string fname = "..//5000.txt")
        {
            var lines = File.ReadAllLines(fname);
            foreach (string line in lines)
                AddWordsFromLine(line);
            Console.WriteLine("wordsDictionary Inited");
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

        /// <summary>
        /// Кол-во слов в словаре
        /// </summary>
        /// <returns></returns>
        public int Count() => dict.Count;
    }

}