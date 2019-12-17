using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace VkApi
{
    /// <summary>
	/// Профиль вк
	/// </summary>
	[DataContract]
    public class VkUser
    {
        [DataMember]
        // ID
        public int id { get; set; }

        [DataMember]
        // Имя
        public string first_name { get; set; }

        [DataMember]
        // Фамилия 
        public string last_name { get; set; }

        [DataMember]
        // deleted или banned  
        public string deactivated { get; set; }

        [DataMember]
        // Скрыт ли профиль 
        public bool is_closed { get; set; }

        [DataMember]
        // Интересы 
        public string interests { get; set; }

        [DataMember]
        // Любимая музыка 
        public string music { get; set; }

        [DataMember]
        // Любимые фильмы 
        public string movies { get; set; }

        [DataMember]
        // Любимые цитаты 
        public string quotes { get; set; }

        [DataMember]
        // Статус 
        public string status { get; set; }

        [DataMember]
        // Любимые игры 
        public string games { get; set; }

        [DataMember]
        // Любимые книги 
        public string books { get; set; }
    }
}
