using System;
using System.Collections.Generic;

namespace Project_Word
{
    public class Word
    {
        /// <summary>
        /// ID слова
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// Часть речи
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// слово на eng 
        /// </summary>
        public string eng { get; set; }
        /// <summary>
        /// Синонимы на английском
        /// </summary>
        public List<string> synonyms { get; set; }
        /// <summary>
        /// слово на русском 
        /// </summary>
        public string rus { get; set; }
        /// <summary>
        /// значение слова на англе 
        /// </summary>
        public List<string> mean_eng { get; set; }
        /// <summary>
        /// значение слова на русском  
        /// </summary>
        public List<string> mean_rus { get; set; }
        /// <summary>
        /// Транскрипция с внешними скобочками "[transcription]"
        /// </summary>
        public string transcription { get; set; }
        /// <summary>
        /// мб блок с идиомами слова ,примерами  
        /// </summary>
        public List<string> eng_examples { get; set; }
        /// <summary>
        /// Уровень слова
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// теги слова 
        /// <example>
        /// c1,c2,b1
        /// </example>
        /// </summary>
        public HashSet<string> tags { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="ty">Часть речи</param>
        /// <param name="Eng">Слово на английском</param>
        /// <param name="syn">Синонимы слова на английском</param>
        /// <param name="Rus">Слово на русском</param>
        /// <param name="MeanE">Значение слова на английском</param>
        /// <param name="MeanR">Значение слова на русском</param>
        /// <param name="Trans">Транскрипция</param>
        /// <param name="eng_ex">Примерв на английском</param>
        /// <param name="lev">Уровень слова</param>
        /// <param name="Tags">Тэги</param>
        public Word(long Id, string ty, string Eng, List<string> syn, string Rus, List<string> MeanE, List<string> MeanR, string Trans, List<string> eng_ex, int lev, HashSet<string> Tags)
        {
            id = Id;
            type = ty;
            eng = Eng.ToLower();
            synonyms = syn; 
            rus = Rus.ToLower();
            mean_eng = MeanE;
            mean_rus = MeanR;
            transcription = Trans;
            eng_examples = eng_ex;
            level = lev;
            tags = Tags;
        }
  
        public override string ToString()
        {
            return eng + "---" + mean_eng + "перевод и значение " + rus + mean_rus;
        }
    }
}
