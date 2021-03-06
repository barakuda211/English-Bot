﻿using System; 
using System.Collections.Generic;
using Dictionary; 
using Project_Word;
using System.IO;
using System.Linq;

namespace English_Bot
{
    public class WordsDictionary
    {
        // Основной словарь
        private Dictionary<long, Word> dict; 
        // Словарь получения ID слова по английскому слову
        public Dictionary<string, long> eng_ids;
        // Словарь получения ID слова по русскому слову слову (может быть много значений)
        public Dictionary<string, List<long>> rus_ids;
        
        public WordsDictionary()
        {
            string dir = Users.GetPathOfFile(Environment.CurrentDirectory);
            dict = Methods.DeSerializationObj<Dictionary<long, Word>>(dir + @"..\Json dicts\w_bank");
            
            eng_ids = new Dictionary<string, long>();
            rus_ids = new Dictionary<string, List<long>>();

            foreach (var word in dict)
            {
                eng_ids.Add(word.Value.eng, word.Key);
            }

            rus_ids = Methods.DeSerializationObj<Dictionary<string, List<long>>>(dir + @"..\Json dicts\rus_ids");
            
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

        /// <summary>
        /// Возвращает список случайных слов одного уровня 
        /// </summary>
        /// <param name="count">Количество ключей</param>
        /// <param name="level">Уровень слов</param>
        /// <returns></returns>
        public List<long> GetRandomEngWords(int count, int level)
        {
            if (level != -1 && (level > 5 || level < 1))
                throw new ArgumentException("level should be = -1, 1, 2, 3, 4, 5");
            if (count < 1 || count > 50)
                throw new ArgumentException("count should be > 0 and < 51");
            List<long> keys = GetKeysByLevel(level);
            List<long> result = new List<long>();
            Random r = new Random();
            while (result.Count < count)
            {
                long key = keys.ElementAt(r.Next(keys.Count));
                if (result.Contains(key))
                    continue;
                else
                    result.Add(key);
            }
            return result;
        }

        /// <summary>
        /// Возвращает случайные русские слова в виде строк
        /// </summary>
        /// <param name="count">Кол-во слов</param>
        /// <param name="level">Уровень слов</param>
        /// <returns></returns>
        public List<string> GetRandomRusWords(int count, int level)
        {
            if (level != -1 && (level > 5 || level < 1))
                throw new ArgumentException("level should be = -1, 1, 2, 3, 4, 5");
            if (count < 1 || count > 50)
                throw new ArgumentException("count should be > 0 and < 51");
            List<string> keys = rus_ids.Keys.ToList();
            List<string> result = new List<string>();
            Random r = new Random();
            while (result.Count < count)
            {
                string key = keys.ElementAt(r.Next(keys.Count));
                if (result.Contains(key))
                    continue;
                else
                    result.Add(key);
            }
            return result;
        }

        public List<long> GetKeys()
        {
            long[] arr = new long[dict.Keys.Count];
            dict.Keys.CopyTo(arr, 0);
            return new List<long>(arr);
        } 

        /// <summary>
        /// Список слов определённого уровня (deprecated)
        /// </summary>
        /// <param name="level">Уровень слова</param>
        /// <returns></returns>
        public List<long> GetKeysByLevel(int level)
        {
            return dict.Values.Where(x => x.level == level).Select(x => x.id).ToList();
        }

        /// <summary>
        /// Возвращает идентификаторы слов определенного уровня
        /// </summary>
        /// <param name="level">Уровень слова</param>
        /// <returns></returns>
        public List<long> GetKeysByLevelWithTr(int level)
        {
            return dict.Values.Where(x => x.level == level && x.mean_rus != null && x.mean_rus.def.Count !=0 && (x.tags == null || !x.tags.Contains("added"))).Select(x => x.id).ToList();
        }

        /// <summary>
        /// Список слов определённого уровня (множеством)
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public HashSet<long> GetKeysByLevel_hash(int level)
        {
            HashSet<long> hs = new HashSet<long>();
            foreach (var x in GetKeysByLevelWithTr(level))
                hs.Add(x);
            return hs;
        }

        /// <summary>
        /// получаем id английских слова  по нашему запросу  
        /// </summary>
        public long GetEngWordId(string word)
        {
            return eng_ids.ContainsKey(word) ? eng_ids[word] : -1;
        }


        /// <summary>
        /// получаем id русских слова  по нашему запросу  
        /// </summary>
        public List<long> GetRusWordIds(string word)
        {
            return rus_ids.ContainsKey(word) ? rus_ids[word] : null;
        }

        public Word GetWord(long word)
        {
            return dict.ContainsKey(word) ? dict[word] : null;
        }

        public bool AddWord(Word w)
        {
            if (dict.ContainsValue(w) == false)
            {
                dict.Add(w.id, w);
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

        /// <summary>
        /// Возвращает множество слов от русского
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<long> GetRusKeys(string str) // ? 
        {
            var set = new List<long>();
            foreach (var x in dict)
                if (str == x.Value.rus)
                {
                    set.Add(x.Value.id);
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