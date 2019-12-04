using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Net; 

namespace Dictionary
{
    /// <summary>
    /// Используем вебреквест
    /// </summary>
    public class TimedWebClient : WebClient
    {
        // Timeout in milliseconds, default = 600,000 msec
        public int Timeout { get; set; }

        public TimedWebClient()
        {
            this.Timeout = 600000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.Timeout;
            return objWebRequest;
        }
    }

    class Methods
    {
        /// <summary>
        /// Исправление кодировки
        /// </summary>
        public static void ChangeEncoding(ref string response)
        {
            Encoding fromEncodind = Encoding.GetEncoding(1251); //из какой кодировки
            byte[] bytes = fromEncodind.GetBytes(response);
            Encoding toEncoding = Encoding.UTF8; //в какую кодировку
            response = toEncoding.GetString(bytes);
        }

        /// <summary>
        /// Выполнение запроса и запись в файл по указанному пути -- устарело
        /// </summary>
        public static string Request(string url, string fName)
        {
            return "Сигнатура устарела";
        }
        /// <summary>
        /// Выполнение запроса и (запись в файл по указанному пути -- устарело) возврат строки-ответа
        /// </summary>
        public static string Request(string url)
        {
            // Выполняем запрос по адресу и получаем ответ в виде строки (Используем вебреквест!)
            try
            {
                string response = new TimedWebClient { Timeout = 1000 }.DownloadString(url);

                // Исправляем кодировку
                ChangeEncoding(ref response);
                // Пишем во временный файл -- устарело
                // File.WriteAllText(prefix + fName + ".json", response);

                // Возвращаем строку-ответ (формат JSON)
                return response;
            }
            catch (System.Net.WebException ex)
            {
                return '!' + ex.Message;
            }

            /*// Создаём объект WebClient
            using (WebClient webClient = new WebClient())
            {
                // Выполняем запрос по адресу и получаем ответ в виде строки
                string response = webClient.DownloadString(url);
                // Исправляем кодировку
                ChangeEncoding(ref response);
                // Пишем во временный файл
                File.WriteAllText(prefix + fName + ".json", response);
            }*/
        }

        /// <summary>
        /// Десериализация в массив объектов -- устарело
        /// </summary>
        public static T[] DeSerialization<T>(string fName)
        {
            T[] objs;
            //Десериализуем файл
            using (FileStream fs = new FileStream(fName + ".json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T[]));
                objs = (T[])jsonFormatter.ReadObject(fs);
            }
            return objs;
        }

        /// <summary>
        /// Десериализация в объект -- устарело
        /// </summary>
        public static T DeSerializationObj<T>(string fName)
        {
            // Объект, в который будет произведена десериализация
            T obj;
            //Десериализуем файл
            using (FileStream fs = new FileStream(fName + ".json", FileMode.OpenOrCreate))
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(T));
                obj = (T)jsonFormatter.ReadObject(fs);
            }
            return obj;
        }

        /// <summary>
        /// Десериализация в объект по строке
        /// </summary>
        public static T DeSerializationObjFromStr<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }

        /// <summary>
        /// Десериализация в массив объектов по строке
        /// </summary>
        public static T[] DeSerializationFromStr<T>(string str)
        {
            return JsonConvert.DeserializeObject<T[]>(str);
        }
    }
}
