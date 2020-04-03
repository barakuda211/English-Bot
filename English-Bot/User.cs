using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VkApi;

namespace English_Bot 
{
    public class User
    {
       public long userId { get; set; }
        public int userLevel { get; set; }
        //для корректной регистрации
        public int regId { get; set; }
        //Имя юзера на всякий случай
        public string name { get; set; }
        public HashSet<string> userTags { get; set; }
        /// <summary>
        /// индексы слов , введенных юзером ,которые он запомнил
        /// </summary>
        public HashSet<long> learnedWords { get; set; }
        /// <summary>
        /// индексы слов ,введенных юзером ,которые он не запомнил  
        /// </summary>
        public HashSet<long> unLearnedWords { get; set; }
        /// <summary>
        /// последнее слово(текст), написанное пользователем
        /// bool избавляет от повторного считывания слова(текста)
        /// 3-ий параметр - это индификатор сообщения
        /// </summary>
        public (string, bool, long) lastMsg;
        /// <summary>
        /// тестируется ли в данный момент юзер
        /// </summary>
        public bool on_Test { get; set; }

        public User(VkUser vk_user, WordsDictionary dict)
        {
            regId = 0;
            userLevel = 1;
            userId = vk_user.id;
            name = vk_user.first_name;
            unLearnedWords = new HashSet<long>();
            parseWordsFields(vk_user, dict);
            on_Test = false;
            lastMsg = ("", false, 0);
        }

        private void parseWordsFields(VkUser vk_user, WordsDictionary dict)
        {
            var fields = string.Join(' ', vk_user.interests, vk_user.music, vk_user.movies, vk_user.quotes, vk_user.status, vk_user.games, vk_user.books).ToLower();
            var newFields = Regex.Split(fields, @"\b\W+\b").Where(x => x.Length > 1).Distinct();
            foreach (var x in newFields)
            {
                foreach (var y in dict.GetEngId(x))
                    unLearnedWords.Add(y);
                foreach (var y in dict.GetRusId(x))
                    unLearnedWords.Add(y);
            }
            foreach (var x in unLearnedWords)
            {
                Console.WriteLine(dict[x]);
            }
            if (unLearnedWords.Count == 0)
            {
                unLearnedWords = new HashSet<long>(dict.eng_ids.Values.Where(x => Math.Sin(Math.Sqrt(x)) > 0).Take(10));
            }
        }
        public User(long Userid, int rgId, string nm, int Uslev, HashSet<string> UsTags, HashSet<long> learWrds, HashSet<long> UnlearWrds)
        {
            userId = Userid;
            regId = rgId;
            name = nm;
            userLevel = Uslev;
            userTags = UsTags;
            learnedWords = learWrds;
            unLearnedWords = UnlearWrds;
            lastMsg = ("", true, 0);
            on_Test = false;
        }

        public User() { }

        public override string ToString()
        {
            return "your level " + userLevel.ToString();
        }

        public bool ChangeLevel(int level)
        {
            if (level == userLevel) return true;
            int temp = userLevel;
            userLevel = level;
            return (temp != userLevel);
        }

       

        public bool AddTags(string[] s)
        {
            bool TagIsAdded = false;

            foreach (var t in s)
            { 
                    if (!userTags.Contains(t))
                    {
                        TagIsAdded = true;
                        userTags.Add(t);
                    }
            }
           
            return TagIsAdded;
        }

        public bool DeleteTags(string[] s)
        {
            bool TagIsDeleted = false;

            foreach (var t in s)
            {
                if (!userTags.Contains(t))
                {
                    TagIsDeleted = true;
                    userTags.Remove(t);
                }
            }

            return TagIsDeleted;
        }

       

    }
}

