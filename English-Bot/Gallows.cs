using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace English_Bot
{
    public class Gallows
    {
        public long user_id { get; set; }
        public bool success { get; set; }
        public Random r = new Random();
        public long word_id { get; set; }
        public string word { get; set; }
        public long attempts_remain { get; set; }
        public List<char> show { get; set; }
        public List<char> used { get; set; } 

        public Gallows(long user_id)
        {
            this.user_id = user_id;
            success = false; 
            var list = EngBot.users[user_id].learnedWords;
            word_id = list == null ? EngBot.dictionary.GetKeysByLevel(3).ElementAt(r.Next(EngBot.dictionary.GetKeysByLevel(3).Count)) : list.ElementAt(r.Next(list.Count));
            word = EngBot.dictionary[word_id].eng;
            show = new List<char>(new string('_', word.Length));
            used = new List<char>(); 
            attempts_remain = word.Length / 2 + 2; 
        }

        public void OpenLetter(char c, bool hint = false)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                if (show[i] == '*' && word[i] == c)
                    show[i] = c;
                used.Add(c);
                if (!hint)
                    --attempts_remain;
                success = string.Join("", show) == word;
            }
        }
    }
}
