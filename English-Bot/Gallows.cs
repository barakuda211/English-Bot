﻿using System;
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
        public List<string> tr { get; set; }

        public Gallows(long user_id)
        {
            this.user_id = user_id;
            var user = EngBot.users[user_id];
            success = false; 
            var list = EngBot.users[user_id].learnedWords;

            long word_id; 
            do
            {
                if (list == null || list.Count == 0)
                {
                    var keys = EngBot.dictionary.GetKeysByLevelWithTr(EngBot.users[user_id].userLevel);
                    word_id = keys.ElementAt(r.Next(keys.Count));
                }
                else
                    word_id = list.ElementAt(r.Next(list.Count));
            }
            while (EngBot.dictionary[word_id].mean_rus == null);

            var def = EngBot.dictionary[word_id].mean_rus.def.ElementAt(r.Next(EngBot.dictionary[word_id].mean_rus.def.Count));
            uint i = 0;
            tr = new List<string>(); 
            foreach (var tr in def.tr)
            {
                this.tr.Add(tr.text);
                ++i;
                if (i == 5)
                    break; 
            }

            word = EngBot.dictionary[word_id].eng;
            show = new List<char>(new string('?', word.Length));
            used = new List<char>(); 
            attempts_remain = word.Length / 2 + 2; 
        }

        public void OpenLetter(char c, bool hint = false)
        {
            for (int i = 0; i < word.Length; ++i)
            {
                if (show[i] == '?' && word[i] == c)
                    show[i] = c;
                success = string.Join("", show) == word;
            }
            used.Add(c);
            if (hint)
                --attempts_remain;
        }
    }
}
