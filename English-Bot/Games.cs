using System;
using System.Collections.Generic;
using System.Text;
using Crossword;

namespace English_Bot
{
    public static class Games
    {
        public static void PlayCrossword(long user_id)
        {
            Crossword.CrossMaker.word_list = new List<long>();
            foreach (long word_id in EngBot.users[user_id].unLearnedWords)
            {
                Crossword.CrossMaker.word_list.Add(word_id);
            }
            Crossword.CrossMaker.CrosswordMaker();

            char[,] field = new char[10,10]; 
            foreach(var cross in CrossMaker.cross)
            {
                if (cross.direction == Cross.Direction.toRight)
                {
                    int x = cross.pos.x;
                    int y = cross.pos.y;
                    foreach (char c in EngBot.dictionary[cross.word_id].eng)
                    {
                        field[x, y] = c;
                        ++x;
                    }
                }
                else
                {
                    int x = cross.pos.x;
                    int y = cross.pos.y;
                    foreach (char c in EngBot.dictionary[cross.word_id].eng)
                    {
                        field[x, y] = c;
                        ++y;
                    }
                }
            }

            string result = "";
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                    result += field[i, j];
                result += "\n";
            }

            EngBot.SendMessage(user_id, result);
        }

    }
}
