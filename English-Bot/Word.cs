using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Project_Word
{
    [DataContract]
    public class Word
    {
        [DataMember]
        /// <summary>
        /// ID слова
        /// </summary>
        public long id { get; set; }
        [DataMember]
        /// <summary>
        /// слово на eng 
        /// </summary>
        public string eng { get; set; }
        [DataMember]
        /// <summary>
        /// Транскрипция
        /// </summary>
        public string trans { get; set; }
        [DataMember]
        /// <summary>
        /// слово на русском 
        /// </summary>
        public string rus { get; set; }
        [DataMember]
        /// <summary>
        /// Уровень слова
        /// </summary>
        public int level { get; set; }
        [DataMember]
        /// <summary>
        /// теги слова 
        /// <example>
        /// c1,c2,b1
        /// </example>
        /// </summary>
        public HashSet<string> tags { get; set; }
        [DataMember]
        /// <summary>
        /// Описание на английском
        /// </summary>
        public SpaceYandexEnEn.YandexEnEn mean_eng { get; set; }
        [DataMember]
        /// <summary>
        /// Описание на русском
        /// </summary>
        public SpaceYandexEnRu.YandexEnRu mean_rus { get; set; }
        [DataMember]
        /// <summary>
        /// Описание русского перевода на аглийском
        /// </summary>
        public SpaceYandexRuEn.YandexRuEn mean_rus_eng { get; set; }
        [DataMember]
        /// <summary>
        /// Описание русского перевода на русском
        /// </summary>
        public SpaceYandexRuRu.YandexRuRu mean_rus_rus { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">ID</param>
        /// <param name="Eng">Слово на английском</param>
        /// <param name="Rus">Слово на русском</param>
        /// <param name="MeanE">Значение слова на английском</param>
        /// <param name="MeanR">Значение слова на русском</param>
        /// <param name="MeanRE">Значение русского перевода на аглийском</param>
        /// <param name="MeanRR">Значение русского перевода на русском</param>
        /// <param name="Trans">Транскрипция</param>
        /// <param name="lev">Уровень слова</param>
        /// <param name="Tags">Тэги</param>
        public Word(long Id, string Eng, string tr, string Rus, SpaceYandexEnEn.YandexEnEn MeanE, SpaceYandexEnRu.YandexEnRu MeanR, 
            SpaceYandexRuEn.YandexRuEn MeanRE, SpaceYandexRuRu.YandexRuRu MeanRR, int lev, HashSet<string> Tags)
        {
            id = Id;
            eng = Eng.ToLower();
            trans = tr;
            rus = Rus.ToLower();
            mean_eng = MeanE;
            mean_rus = MeanR;
            mean_rus_eng = MeanRE;
            mean_rus_rus = MeanRR;
            level = lev;
            tags = Tags;
        }

        public Word(int Id, string Eng, string Tran, string Rus)
        {
            id = Id;
            eng = Eng.ToLower();
            trans = Tran;
            rus = Rus;
        }

        public override string ToString()
        {
            return eng + ": " + mean_rus.def[0].tr[0].text;
        }
    }
}
