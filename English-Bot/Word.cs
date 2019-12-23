using System;
using System.Collections.Generic;

namespace English_Bot
{
    public class Word

    {

        public int id { get; set; }
        /// <summary>
        /// слово на eng 
        /// </summary>
        public string eng { get; set; }
        /// <summary>
        /// слово на русском 
        /// </summary>
        public string rus { get; set; }
       public string transcript { get; set; }
        /// <summary>
        /// мб блок с идиомами слова ,примерами  
        /// </summary>
        public string addition { get; set; }
        /// <summary>
        /// теги слова 
        /// <example>
        /// c1,c2,b1
        /// </example>
        /// </summary>
        public HashSet<string> tags { get; set; }

        public Word(int Id,string Eng,string Tran,string Rus)
        {
            id = Id;
            eng = Eng;
            transcript = Tran;
            rus = Rus;
        }

        public Word(int Id, string Eng, string Rus, string Trans, string Add, HashSet<string> Tags)
        {
            id = Id;
            eng = Eng.ToLower();
            rus = Rus.ToLower();
            transcript = Trans;
            addition = Add;
            tags = Tags;

        }
  
        public override string ToString()
        {
            return id+" "+eng + " " + transcript + " " + rus;
        }
        
     

    }
}
