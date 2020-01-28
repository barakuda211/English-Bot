using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Project_Word;
using System.IO;
using System.Text; 
using System.Text.RegularExpressions;
using Classes;
using static System.Threading.Thread;
using static System.Random;
using SpaceYandexEnEn;
using SpaceYandexEnRu;
using SpaceYandexRuRu;
using SpaceYandexRuEn;

/// <summary>
/// Необходимо доработать запись всех данных из слова формата YanWordEn
/// в формат Word в функции DescriptEngFromYandex 
/// После чего можно будет раскомментировать оставшиеся строчки 
/// и записать все списки в файлы
/// </summary>

namespace Dictionary
{
    class Program
    {
        /// <summary>
        /// Ключ к яндекс словарю
        /// </summary>
        public static string key = "dict.1.1.20191110T125921Z.2e938b8f1af39304.8ce369b76d519181943a5643717495fb5cacec21";

        /// <summary>
        /// Генерим ID (Можно упростить)
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static long GenID(ref List<Word> list)
        {
            long id;
            do
            {
                Random r = new Random();
                id = r.Next(1, 10000);
            } while (list.Select(w => w.id).Contains(id));
            return id; 
        }

        /// <summary>
        /// Переводим слово Яндекса в наш формат Word 
        /// </summary>
        /// <param name="word"></param>
        /// <param name="enen"></param>
        /// <param name="enru"></param>
        /// <param name="ruen"></param>
        /// <param name="ruru"></param>
        /// <param name="rus">Значение на русском</param>
        static void DescriptYandexEn(string word, ref YandexEnEn enen, ref YandexEnRu enru, ref YandexRuEn ruen, ref YandexRuRu ruru, out string rus)
        {
            string request1 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + key + @"&lang=en-en&text=" + word;
            string request2 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + key + @"&lang=en-ru&text=" + word;
            string request3 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + key + @"&lang=ru-en&text=";
            string request4 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + key + @"&lang=ru-ru&text=";
            string response1 = Methods.Request(request1);
            Console.WriteLine("Length of response1 = " + response1.Length);
            string response2 = Methods.Request(request2);
            enen = Methods.DeSerializationObjFromStr<YandexEnEn>(response1);
            Console.WriteLine("Defenitions count = " + enen.def.Count);
            enru = Methods.DeSerializationObjFromStr<YandexEnRu>(response2);
            rus = enru.def[0].tr[0].text;
            request3 += rus;
            request4 += rus;
            string response3 = Methods.Request(request3);
            string response4 = Methods.Request(request4);
            ruen = Methods.DeSerializationObjFromStr<YandexRuEn>(response3);
            ruru = Methods.DeSerializationObjFromStr<YandexRuRu>(response4);
        }

        static void Main(string[] args)
        {
            // Создаём сериализаторы
            var jf_word = new DataContractJsonSerializer(typeof(List<Word>));
            var jf_yan_enen = new DataContractJsonSerializer(typeof(List<YandexEnEn>));
            var jf_yan_enru = new DataContractJsonSerializer(typeof(List<YandexEnRu>));
            var jf_yan_ruen = new DataContractJsonSerializer(typeof(List<YandexRuEn>));
            var jf_yan_ruru = new DataContractJsonSerializer(typeof(List<YandexRuRu>));

            // Файл for Word
            var writeAsWord = File.CreateText("eng_words.json");

            // Файл for Yandex (english to english)
            var writeAsYandexEnEn = File.CreateText("yan_dict_en-en.json");
            // Файл for Yandex (english to russian)
            var writeAsYandexEnRu = File.CreateText("yan_dict_en-ru.json");
            // Файл for Yandex (russian to english)
            var writeAsYandexRuEn = File.CreateText("yan_dict_ru-en.json");
            // Файл for Yandex (russian to russian)
            var writeAsYandexRuRu = File.CreateText("yan_dict_ru-ru.json");

            // Исходный список слов
            var base_words = File.ReadLines("5000.txt");
            // Список слов в формате Word
            List<Word> list = new List<Word>();
            // Список слов в формате YandexEnEn
            List<YandexEnEn> yan_list_en_en = new List<YandexEnEn>();
            // Список в формате YandexEnRu
            List<YandexEnRu> yan_list_en_ru = new List<YandexEnRu>();
            // Список в формате YandexRuEn
            List<YandexRuEn> yan_list_ru_en = new List<YandexRuEn>();
            // Список в формате YandexRuRu
            List<YandexRuRu> yan_list_ru_ru = new List<YandexRuRu>();

            Console.WriteLine("Serializers, files and lists were created!");
            int step = 1;
            int id = 1;
            foreach (var line in base_words)
            {
                // Выделяем слово
                string word = Regex.Match(line, @"\b[a-z]+\b").Value;
                //string word = line.Trim().ToLower();
                try
                {
                    if (!word.All(c => char.IsLetter(c)))
                    {
                        throw new ArgumentException("Bad word in line " + step);
                    }
                }
                catch (ArgumentException ar) { Console.WriteLine(ar.Message); }

                // Выводим сообщение о записи
                Console.WriteLine("Reading '" + word + "'" + " - " + step);

                // Номер слова (не путать с ID)
                //long id;
                // Получаем номер слова
                /*bool f = long.TryParse(Regex.Match(line, @"^\d+\b").Value, out id);
                if (!f)
                    Console.WriteLine("Bad word in: '" + line + "'");*/


                // Берём транскрипцию
                string trans = Regex.Match(line, @"\[\w*\]").Value;

                // Заводим слова в формате Yandex
                YandexEnEn en_en = new YandexEnEn();
                YandexEnRu en_ru = new YandexEnRu();
                YandexRuEn ru_en = new YandexRuEn();
                YandexRuRu ru_ru = new YandexRuRu();

                bool err = false;
                string rus = "";

                // В ru и en записались слова в нужных форматах
                try
                {
                    DescriptYandexEn(word, ref en_en, ref en_ru, ref ru_en, ref ru_ru, out rus);
                }
                catch { Console.WriteLine("Error in description"); err = true; }

                if (!err)
                {
                    // Добавляем слова в списки, если ошибки не было
                    try
                    {
                        Word add = new Word(id, word, trans, rus, en_en, en_ru, ru_en, ru_ru, 0, new HashSet<string>());
                        ++id;
                        list.Add(add);
                        yan_list_en_en.Add(en_en);
                        yan_list_en_ru.Add(en_ru);
                        yan_list_ru_en.Add(ru_en);
                        yan_list_ru_ru.Add(ru_ru);
                        Console.WriteLine("Count -------------> " + list.Count);
                    }
                    catch { Console.WriteLine("Error in step " + step); }
                }
                else
                {
                    Console.WriteLine("Error in step #" + step);
                    continue;
                }

                Console.WriteLine("Wrote word #" + step);
                Console.WriteLine(); 
                Sleep(1);
                ++step;
            }

            try
            {
                // Word
                jf_word.WriteObject(writeAsWord.BaseStream, list);
            }
            catch (Exception e){ Console.WriteLine("Error in writing our words");
                Console.WriteLine(e.Message);
            }
            try
            {
                // YandexEnEn
                jf_yan_enen.WriteObject(writeAsYandexEnEn.BaseStream, yan_list_en_en);
            }
            catch { Console.WriteLine("Error in writein english words from yandex"); }
            try
            {
                // YandexEnRu
                jf_yan_enru.WriteObject(writeAsYandexEnRu.BaseStream, yan_list_en_ru);
            }
            catch { Console.WriteLine("Error in writing russian words from yandex"); }
            try
            {
                // YandexRuEn
                jf_yan_ruen.WriteObject(writeAsYandexRuEn.BaseStream, yan_list_ru_en);
            }
            catch { Console.WriteLine("Error in writing russian defenitions"); }
            try
            {
                // YandexRuRu
                jf_yan_ruru.WriteObject(writeAsYandexRuRu.BaseStream, yan_list_ru_ru);
            }
            catch { Console.WriteLine("Error in writing english definitions"); }

            Console.WriteLine("Our lists were written down into files!");
            Console.WriteLine("Steps = " + (step - 1) + " | Good = " + (id - 1));
            Console.ReadLine();
        }
    }
}
