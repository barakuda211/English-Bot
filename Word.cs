using System;
using System.Collections.Generic;

namespace Project_Word
{
    public class Word
    {
       long id;
       public   string eng;
        string rus;
        string mean_eng;
        string mean_rus;
        string transcript;
        string addition;
        int level;
        HashSet<string> tags;


        public Word(long Id, string Eng, string Rus, string MeanE, string MeanR, string Trans, string Add, int lev, HashSet<string> Tags)
        {
            id = Id;
            eng = Eng.ToLower();
            rus = Rus.ToLower();
            mean_eng = MeanE;
            mean_rus = MeanR;
            transcript = Trans;
            addition = Add;
            level = lev;
            tags = Tags;

        }
  
        public override string ToString()
        {
            return eng + "---" + mean_eng + "перевод и значение " + rus + mean_rus;
        }
        
        public  string GetEnginlishWord()
        {
            return  eng;
            
        }

        public   string GetRussianWord()
        {
            return rus;
        }

    }
}
