using System;
using System.Collections.Generic;

namespace Project_Word
{
    public class Word

    {

        public long id { get; set; }
        /// <summary>
        /// слово на eng 
        /// </summary>
        public string eng { get; set; }
        /// <summary>
        /// слово на русском 
        /// </summary>
        public string rus { get; set; }
        /// <summary>
        /// значение слова на англе 
        /// </summary>
        public string mean_eng { get; set; }
        /// <summary>
        /// значение слова на русском  
        /// </summary>
        public string mean_rus { get; set; }
       public string transcript { get; set; }
        /// <summary>
        /// мб блок с идиомами слова ,примерами  
        /// </summary>
        public string addition { get; set; }
        public int level { get; set; }
        /// <summary>
        /// теги слова 
        /// <example>
        /// c1,c2,b1
        /// </example>
        /// </summary>
        public HashSet<string> tags { get; set; }


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
        
     

    }
}
