using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using VkApi;

namespace English_Bot 
{
    public class User
    {
        //Vk id юзера
        public long? userId { get; set; }
        //Для адекватной регистрации пользователя
        public int regId { get; set; }
        //Имя юзера на всякий случай
        public string name { get; set; }
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

        public User(VkUser vk_user)
        {
            regId = 0;
            userId = vk_user.id;
            name = vk_user.first_name;
            //TODO: распарсить поля для заполнения списков слов
        }

        private void parseWordsFields(VkUser vk_user)
        {
            var fields = string.Join(' ', vk_user.interests, vk_user.music, vk_user.movies, vk_user.quotes, vk_user.status, vk_user.games, vk_user.books);
            var newFields = Regex.Split(fields, @"\b\w{2,}\b");
            
        }

        public User(long? Userid=null, int regId=0, int Uslev=0, HashSet<string> UsTags=null, HashSet<long> learWrds=null, HashSet<long> UnlearWrds=null)
        {
            userId = Userid;
            this.regId = regId;
            userLevel = Uslev;
            userTags = UsTags;
            learnedWords = learWrds;
            unLearnedWords = UnlearWrds;
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

