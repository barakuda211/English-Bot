using System;
using System.Collections.Generic;

namespace English_Bot 
{
    public class User
    {
       public long userId { get; set; }
        public static int userLevel { get; set; }
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
        public User(long Userid, int Uslev, HashSet<string> UsTags, HashSet<long> learWrds, HashSet<long> UnlearWrds)
        {
            userId = Userid;
            userLevel = Uslev;
            userTags = UsTags;
            learnedWords = learWrds;
            unLearnedWords = UnlearWrds;
            lastMsg = ("", true, 0);
        }

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

