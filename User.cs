using System;
using System.Collections.Generic;

namespace Project {
    public class User
    {
       public long userId;
        static int userLevel;
        HashSet<string> userTags;
        HashSet<long> learnedWords;
        HashSet<long> unLearnedWords;

        public User(long Userid, int Uslev, HashSet<string> UsTags, HashSet<long> learWrds, HashSet<long> UnlearWrds)
        {
            userId = Userid;
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

        public int GetUserLevel()
        {
            return userLevel;
        }

        public bool AddTags(string[] s)
        {
            bool TagIsAdded = false;
            for (int i = 0; i < s.Length; i++)
                if (!userTags.Contains(s[i]))
                {
                    TagIsAdded = true;
                    userTags.Add(s[i]);
                }

            return TagIsAdded;
        }

        public bool DeleteTags(string[] s)
        {
            bool TagIsDeleted = false;
            for (int i = 0; i < s.Length; i++)
                if (userTags.Contains(s[i]))
                {
                    TagIsDeleted = true;
                    userTags.Remove(s[i]);
                }

            return TagIsDeleted;
        }

        public long GerUserId()
        {
            return userId;
        }



    }
}

