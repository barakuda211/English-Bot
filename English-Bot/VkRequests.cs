﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Runtime.Serialization;
using English_Bot;

namespace VkApi
{
    public class TimedWebClient : WebClient
    {
        public int Timeout { get; set; }

        public TimedWebClient(int time = 600000)
        {
            Timeout = time;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = this.Timeout;
            return request;
        }
    }
    public class VkRequests
    {
        /// <summary>
        /// Сам запрос, возвращает строку
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Request(string url)=> new TimedWebClient(5000).DownloadString(url);

        /// <summary>
        /// Преобразование JSON в объект заданного класса
        /// </summary>
        public static T ObjectFromStr<T>(string str) => JsonConvert.DeserializeObject<T>(str);

        public static T[] ArrayFromStr<T>(string str) => JsonConvert.DeserializeObject<T[]>(str);

        public static VkUser VkRequestUser(long? id)
        {
            if (id == null)
                throw new NullReferenceException();
            string url = $"https://api.vk.com/method/users.get?user_id=" + id +$"&fields=interests,status,games,music,quotes,movies,books" ;
            url= url+ $"&v=5.89&access_token="+EngBot.Token;
            return ObjectFromStr<VkUsers>(Request(url)).response[0];
        }

    }
}
