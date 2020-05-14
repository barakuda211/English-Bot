using Project_Word;
using Dictionary;
using System.Net;
using English_Bot.Properties;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System;
using System.Text;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using NAudio.Wave;
using Alvas.Audio;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using static System.Math;
using VkNet.Enums;

namespace English_Bot
{
    partial class EngBot
    {
        /// <summary>
        /// Отправояет пользователю картинку, описание и примеры
        /// </summary>
        /// <param name="wordId">Идентификатор слова</param>
        /// <param name="userId">Идентификатор пользователя</param>
        public static void SendWord(long userId, long wordId)
        {
            SendPicture(userId, wordId);
            SendFullWordDescription(userId, wordId);
            SendExample(userId, wordId);
        }

        /// <summary>
        /// Добавляет в словарь [русское слово, список переводов] ключи по данному слову
        /// </summary>
        /// <param name="word">Слово классо Word</param>
        /// <returns>Возвращает true, если слова были добавлены</returns>
        private static bool AddRusIds(Word word)
        {
            try
            {
                if (word.mean_rus == null)
                    return false;
                else
                    foreach (var def in word.mean_rus.def)
                        if (def.tr != null)
                            foreach (var tr in def.tr)
                            {
                                if (!dictionary.rus_ids.ContainsKey(tr.text))
                                {
                                    dictionary.rus_ids.Add(tr.text, new List<long> { word.id });
                                }
                                else
                                    dictionary.rus_ids[tr.text].Add(word.id);
                                if (tr.syn != null)
                                    foreach (var syn in tr.syn)
                                    {
                                        if (!dictionary.rus_ids.ContainsKey(syn.text))
                                        {
                                            dictionary.rus_ids.Add(syn.text, new List<long> { word.id });
                                        }
                                        else
                                            dictionary.rus_ids[syn.text].Add(word.id);
                                    }
                            }
                return true; 
            }
            catch (Exception e)
            {
                Console.WriteLine("Error with adding values to rus ids");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false; 
            }
        }

        /// <summary>
        /// Пробует добавить в словарь данное слово на английском
        /// </summary>
        /// <param name="word">Строка, содержащая слово</param>
        /// <returns>Возвращает true, если слово было добавлено</returns>
        private static bool TryToAddEnglishWord(string word)
        {
            if (dictionary.eng_ids.ContainsKey(word))
                return false;
            try
            {
                string request1 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + Resources.tr_key + @"&lang=en-en&text=" + word;
                string request2 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + Resources.tr_key + @"&lang=en-ru&text=" + word;

                string response1 = Methods.Request(request1);
                string response2 = Methods.Request(request2);

                SpaceYandexEnEn.YandexEnEn mean_eng = Methods.DeSerializationObjFromStr<SpaceYandexEnEn.YandexEnEn>(response1);
                SpaceYandexEnRu.YandexEnRu mean_rus = Methods.DeSerializationObjFromStr<SpaceYandexEnRu.YandexEnRu>(response2);

                if (mean_eng.def.Count == 0 && mean_rus.def.Count == 0)
                {
                    return false;
                }

                Word add_word;
                long add_id = dictionary.GetKeys().Max() + 1;

                if (mean_rus.def.Count > 0 && mean_eng.def.Count == 0)
                    add_word = new Word(add_id, word, mean_rus.def[0].ts, mean_rus.def[0].tr[0].text, mean_eng, mean_rus, null, null, -1, new HashSet<string> { "rus_only", "added" });
                else if (mean_rus.def.Count == 0 && mean_eng.def.Count > 0)
                    add_word = new Word(add_id, word, mean_eng.def[0].ts, null, mean_eng, mean_rus, null, null, -1, new HashSet<string> { "eng_only", "added" });
                else add_word = new Word(add_id, word, mean_eng.def[0].ts, mean_rus.def[0].tr[0].text, mean_eng, mean_rus, null, null, -1, new HashSet<string> { "added" });

                bool add_good = dictionary.AddWord(add_word);
                if (!add_good)
                    return false;

                dictionary.eng_ids.Add(word, add_id);
                
                if (add_word.mean_rus != null)
                {
                    bool b = AddRusIds(add_word);
                    if (!b)
                        add_word.tags.Add("no_in_rus_ids");
                }
                
                return true; 
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in adding english word: " + word);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// Добавляет в список [русское слово, список переводов] ключ по заданниму слову
        /// </summary>
        /// <param name="word">Строка, содержащая русское слово</param>
        /// <returns>Возвращает true, если слово было добавлено</returns>
        private static bool TryToAddRussianWord(string word)
        {
            try
            {
                if (dictionary.rus_ids.ContainsKey(word))
                {
                    return false;
                }

                string request1 = @"https://dictionary.yandex.net/api/v1/dicservice.json/lookup?key=" + Resources.tr_key + @"&lang=ru-en&text=" + word;

                string response1 = Methods.Request(request1);

                SpaceYandexRuEn.YandexRuEn rus_mean = Methods.DeSerializationObjFromStr<SpaceYandexRuEn.YandexRuEn>(response1);

                dictionary.rus_ids.Add(word, new List<long>());

                if (rus_mean.def != null)
                    foreach (var def in rus_mean.def)
                    {
                        if (def.tr != null)
                            foreach (var tr in def.tr)
                            {
                                if (dictionary.eng_ids.ContainsKey(tr.text))
                                {
                                    dictionary.rus_ids[word].Add(dictionary.eng_ids[tr.text]);
                                }
                                if (tr.syn != null)
                                    foreach (var syn in tr.syn)
                                    {
                                        //if (!dictionary.eng_ids.ContainsKey(syn.text) )
                                            //TryToAddEnglishWord(syn.text);
                                        if (dictionary.eng_ids.ContainsKey(syn.text))
                                        {
                                            dictionary.rus_ids[word].Add(dictionary.eng_ids[syn.text]);
                                        }
                                    }
                            }
                    }
                return true; 
            }
            catch (Exception e)
            {
                Console.WriteLine("Some error with adding rus word");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false; 
            }
        }

        /// <summary>
        /// Отправляет пользователю полную информацию о слове по его ID
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="wordId">Идентификатор слова</param>
        static void SendFullWordDescription(long userId, long wordId)
        {
            string message = "";
            Word word = dictionary[wordId];
            try
            {
                if (word.mean_eng != null && word.mean_eng.def != null)
                    message += word.eng.ToUpper() + " [" + ((word.tags != null && word.tags.Contains("eng_only")) ? word.mean_eng.def[0].ts : word.mean_rus.def[0].ts) + "]\n";
                message += "Определения:\n";
                bool exps = false;
                foreach (var def in word.mean_rus.def)
                {
                    message += "-----------" + def.text + " " + def.pos + ".:\n";
                    //string syns = "";
                    foreach (var tr in def.tr)
                    {
                        //syns += "Синонимы:\n";
                        message += "-> " + tr.text + /*" (" + dictionary[dictionary.GetRusWordIds(tr.text)[0]]?.eng + ")" + */ "\n";
                        if (tr.syn != null)
                        {
                            //foreach (var syn in tr.syn)
                            //syns += tr.syn[0].text + ", ";
                            //syns = syns.Substring(0, syns.Length - 2);
                        }
                    }
                    if (!exps && def.tr[0].ex != null)
                    {
                        message += "Примеры: \n";
                        foreach (var ex in def.tr[0].ex)
                            message += ex.text + " - " + ex.tr[0].text + "\n";
                        exps = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Отправка описания слова неудачна: ID = " + userId + ", Word id = " + wordId);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            SendMessage(userId, message == "" ? "Произошла ошибка" : message);
            //return message; 
        } 

        /// <summary>
        /// Краткий список слов, которыми данное словов можно прервести
        /// </summary>
        /// <param name="word">Строка, содержащая слово</param>
        /// <returns>Список переводов</returns>
        public static string Translation(string word)
        {
            word = GetFormatedWord(word); 

            string no_word = "Я не знаю такого слова :(";
            string no_tr = "Перевод отсутствует";

            if (word[0] >= 'A' && word[0] <= 'z')
            {
                try
                {
                    if (!dictionary.eng_ids.ContainsKey(word))
                    {
                        bool success = TryToAddEnglishWord(word);
                        if (!success)
                            return no_word; 
                    }

                    if (dictionary[dictionary.eng_ids[word]].mean_rus == null)
                        return no_tr;
                    if (dictionary[dictionary.eng_ids[word]].mean_rus.def.Count == 0)
                        return no_tr;

                    /*
                    string res = "";
                    foreach (var def in dictionary[dictionary.eng_ids[word]].mean_rus.def)
                        res = res + ", " + def.tr[0].text;
                    return res;
                    */

                    string res = string.Join(", ", from def in dictionary[dictionary.eng_ids[word]].mean_rus.def select def.tr[0].text);
                    if (res == string.Empty)
                        return "Перевод отсутствует";
                    return res;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
            else
            {
                try
                {
                    //word = string.Join("", word.Select(x => x == 'ё' ? 'е' : x));
                    List<long> list = null;
                    if (!dictionary.rus_ids.ContainsKey(word))
                    {
                        bool success = TryToAddRussianWord(word);
                        if (!success)
                            return no_word;
                    }

                    if (dictionary.rus_ids.ContainsKey(word))
                        list = dictionary.rus_ids[word];
                    //var list = dictionary.rus_ids.ContainsKey(word) ? dictionary.rus_ids[word] : null;
                    if (list == null || list.Count == 0)
                        return no_word;
                    else
                        return string.Join(", ", list.Select(x => dictionary[x].eng));
                    //return (list == null || list.Count == 0) ? an : string.Join(", ", list.Select(x => dictionary[x]?.eng));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
            return no_word; 
        }

        public static string GetEngTranslation(long wordId) => string.Join(", ", from def in dictionary[wordId].mean_rus.def select def.tr[0].text);

        public static string GetRusTranslation(long wordId) 
        {
            List<long> rus = null;
            if (dictionary.rus_ids.ContainsKey(dictionary[wordId].rus))
                rus = dictionary.rus_ids[dictionary[wordId].rus];

            if (rus == null)
                return dictionary[wordId].eng;
            else
            return string.Join(", ", rus.Select(x => dictionary[x].eng)); 
        }

        /// <summary>
        /// Перевод всех английских слов, найденных в тексте
        /// </summary>
        /// <param name="text">Массив слов</param>
        /// <param name="level">Минимальный уровень слова</param>
        /// <returns>Список переводов слов</returns>
        static string MultipleTranslation(string[] text, int level = 1)
        {
            string answer = "";
            foreach (var word in text)
            {
                if (dictionary.eng_ids.ContainsKey(word.ToLower()))
                    answer += word + " -> " + Translation(word.ToLower()) + "\n";
            }
            return answer == "" ? "Не было найдено английских слов для перевода" : answer;
        }

        /// <summary>
        /// Отправляет пользователю картинку со словом, транскрипцией и переводом
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="word">Идентификатор слова</param>
        /// <returns>Возвращает true, если отправка прошла успешно</returns>
        static bool SendPicture(long id, long word)
        {
            try
            {
                // Получаем адрес изображения 
                string answer = Methods.Request(@"https://pixabay.com/api/?key=15427273-eddca1835086f92624a5b62a0&q=" + dictionary[word].eng + @"&image_type=photo&pretty=true");
                // Console.WriteLine(answer);
                Pictures pics = Methods.DeSerializationObjFromStr<Pictures>(answer);
                
                // Выбираем картинку 
                Random r = new Random(17);
                int pic_num = r.Next(pics.hits.Count);
                Hit pic = pics.hits[pic_num];

                // Имя картинки юез текста
                string name = word + "_" + pic_num + "_picture.jpg";

                // Скачиваем картинку
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(pic.webformatURL, name);
                }

                // Создаем обЪекты для рисования
                Image bitmap = Image.FromFile(name);
                
                //Bitmap pic = new Bitmap(bitmap);
                //pic.SetResolution(300, 300);

                //string text = dictionary[word].eng + "\n" + dictionary[word].mean_eng.def[0].ts + "\n" + dictionary[word].rus;
                Graphics graphImage = Graphics.FromImage(bitmap);
                graphImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Определяем шрифт, а также сохраняем ширину и длину изображения
                int font = r.Next(0, FontFamily.Families.Length - 1);
                int width = pic.webformatWidth;
                int height = pic.webformatHeight;

                // Затемняем фон
                Color col = Color.FromArgb(63, 0, 0, 0);
                graphImage.FillRectangle(new SolidBrush(col), new Rectangle(0, 0, width, height));
                // bitmap.Save(word + "_dark.jpg");

                // Выравнивание текста по центру 
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.FormatFlags = StringFormatFlags.FitBlackBox;

                // Размер текста
                float emSize = Min(width * 125 / graphImage.DpiX, height * 125 / graphImage.DpiY);
                uint pixSize = (uint)Math.Floor((double)width / 10); // 10 % ширины картинки будет занимать текст
                //int size = (int)(125 / graphImage.DpiX);
                // float maxf = System.Single.MaxValue;
                
                // Выюираем белый цвет
                var tBrush = new SolidBrush(Color.White);

                // Наносим слово на английском
                string text = dictionary[word].eng;
                graphImage.DrawString(
                    text,
                    new Font(new FontFamily(genericFamily: System.Drawing.Text.GenericFontFamilies.SansSerif)/*FontFamily.Families[font].Name*/, emSize / (text.Length < 7 ? 7 : text.Length) /* pixSize */ /*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular /* , GraphicsUnit.Pixel */ ),
                    //new SolidBrush(ColorTranslator.FromHtml("#FFFFFF")),                   
                    tBrush,
                    new Point(width / 2,
                            height / 2 - (height / 4)),
                    new StringFormat(stringFormat));

                // Пишем транскрипцию 
                text = "[" + ((dictionary[word].tags != null && dictionary[word].tags.Contains("eng_only")) ? dictionary[word].mean_eng.def[0].ts : dictionary[word].mean_rus.def[0].ts) + "]";
                if (text.Length == 2)
                    goto Translation;
                graphImage.DrawString(
                    @text,
                    new Font(new FontFamily(genericFamily: System.Drawing.Text.GenericFontFamilies.SansSerif)/*FontFamily.Families[font].Name*/, emSize / (text.Length < 7 ? 7 : text.Length) /* pixSize */ /*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular /* , GraphicsUnit.Pixel */ ),
                    //new SolidBrush(ColorTranslator.FromHtml("#FFFFFF")),
                    tBrush,
                    new Point(width / 2,
                            height / 2 /* - (height / 10)*/),
                    new StringFormat(stringFormat));

                Translation: 
                // Добавляем перевод 
                if (!(dictionary[word].tags != null && dictionary[word].tags.Contains("eng_only")))
                {
                    var def = dictionary[word].mean_rus.def[r.Next(dictionary[word].mean_rus.def.Count)];
                    text = def.tr[r.Next(def.tr.Count)].text; //string.Join('/', dictionary[word].mean_rus.def.Select(x => x.tr[0].text));
                    graphImage.DrawString(
                        text,
                        new Font(new FontFamily(genericFamily: System.Drawing.Text.GenericFontFamilies.SansSerif),  emSize / (text.Length < 7 ? 7 : text.Length) /* pixSize */ /*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular /* , GraphicsUnit.Pixel */ ),
                        //new SolidBrush(ColorTranslator.FromHtml("#FFFFFF")),
                        tBrush,
                        new Point(width / 2,
                        height / 2 + (height / 4)),
                        new StringFormat(stringFormat));
                }

                // Имя файла картинки с нанесенным текстом
                string text_name = word + "_" + pic_num + "_picture_with_str.jpg";

                // Сохраняем преобразованное изображение
                bitmap.Save(text_name);

                // System.Threading.Thread.Sleep(100); 

                // Отправляем сообщение пользователю
                string url = bot.Api.Photo.GetMessagesUploadServer(id).UploadUrl;
                var uploader = new WebClient();
                var uploadResponseInBytes = uploader.UploadFile(url, text_name);
                var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
                // VKRootObject response = Methods.DeSerializationObjFromStr<VKRootObject>(uploadResponseInString);
                var photos = bot.Api.Photo.SaveMessagesPhoto(uploadResponseInString);
                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = id,
                    Attachments = photos
                });

                // Удаляем сохраненные фотографии
                try
                {
                    File.Delete(text_name);
                    // File.Delete(word + "_picture.jpg");
                }
                catch (DirectoryNotFoundException ex)
                {
                    Console.WriteLine("Error with deleting photo");
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error downloading photo: ID = " + id + ", Word id = " + word + ", Word = " + dictionary[word].eng);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
            Console.WriteLine("Photo sent to " + id /*graphImage.DpiX + " " + graphImage.DpiY*/ );
            return true;
        }

        /// <summary>
        /// Вспомогательная функция преобразования Ogg в Wav
        /// </summary>
        /// <param name="file">Имя файла</param>
        static void ToWav(string file)
        {
            using (DsReader dr = new DsReader(file))
            {
                if (dr.HasAudio)
                {
                    IntPtr format = dr.ReadFormat();
                    using (WaveWriter ww = new WaveWriter(File.Create(file + ".wav"),
                        AudioCompressionManager.FormatBytes(format)))
                    {
                        byte[] data = dr.ReadData();
                        ww.WriteData(data);
                    }
                }
            }
        }

        /// <summary>
        /// Вспомогательная функция преобразования Wav в Ogg
        /// </summary>
        /// <param name="file">Имя файла</param>
        /// <returns>Новое имя файла</returns>
        static string ToOgg(string file)
        {
            using (DsReader dr = new DsReader(file))
            {
                if (dr.HasAudio)
                {
                    IntPtr format = dr.ReadFormat();
                    using (WaveWriter ww = new WaveWriter(File.Create(file + ".ogg"),
                        AudioCompressionManager.FormatBytes(format)))
                    {
                        byte[] data = dr.ReadData();
                        ww.WriteData(data);
                    }
                    Console.WriteLine("Audio created successfully!");
                }
                else
                {
                    Console.WriteLine("No audio in this file");
                }
            }
            return file + ".ogg";
        }

        /// <summary>
        /// Отправляет пользователю аудиосообщение с примером на английском
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="word">Идентификатор слова</param>
        static void SendSound(long id, long word)
        {
            try
            {
                Process sound = new Process();
                Console.WriteLine(Environment.CurrentDirectory);
                sound.StartInfo.FileName = Users.GetPathOfFile(Environment.CurrentDirectory) + @"..\Speech\SpeechSynthesis.exe";
                string mes = GetSentenceExemples(dictionary[word].eng)[0];
                sound.StartInfo.Arguments = word + " \"" + mes + "\"";
                sound.Start();

                sound.WaitForExit();

                string file_name = word + "_sound.wav";

                string url = bot.Api.Docs.GetMessagesUploadServer(id, VkNet.Enums.SafetyEnums.DocMessageType.AudioMessage).UploadUrl;

                var uploader = new WebClient();
                var uploadResponseInBytes = uploader.UploadFile(url, file_name);
                var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
                var mess =  bot.Api.Docs.Save(uploadResponseInString, "voice" + word); 

                List<VkNet.Model.Attachments.MediaAttachment> atts = new List<VkNet.Model.Attachments.MediaAttachment>();
                foreach (var a in mess)
                    atts.Add(a.Instance);

                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = id,
                    Attachments = atts, 
                    Message = mes
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong in Sending Sound");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        } 

        /// <summary>
        /// Отправляет пользователю текст по его голосовому сообщению
        /// </summary>
        /// <param name="message">Событие, содержащее звук</param>
        static void SendRecognition(EventArgs message)
        {
            SpeechRecognizer rec = new SpeechRecognizer(); 
            
        }

        /// <summary>
        /// Получает список примеров использования слова
        /// </summary>
        /// <param name="word">Само слово (Строка)</param>
        /// <param name="cnt">Количество примеров</param>
        /// <returns>Список примеров (Строк)</returns>
        static List<string> GetSentenceExemples(string word, int cnt = 5)
        {
            //HashSet<string> s_Exs = new HashSet<string>();
            List<string> s_Exs = new List<string>();
            //if (dictionary.GetRusWordIds(word).Count > 0)

            if (!word.All(x => x >= 'a' && x <= 'z'))       // если слово не аннглийское
                return new List<string>() { "Я ожидал английское слово." };

            try
            {

                WebClient webclient = new WebClient();
                webclient.Headers.Add(HttpRequestHeader.UserAgent, "Only a test!");
                string html = webclient.DownloadString("https://context.reverso.net/translation/english-russian/" + word);

                Regex r_Exs = new Regex(@"(.+\W" + word + @"\W.+)<\/span>");

                foreach (Match m in r_Exs.Matches(html))
                {
                    s_Exs.Add(m.Groups[1].Value.Replace("<em>", "").Replace("</em>", ""));
                }
                return s_Exs.Take(cnt).ToList();
            }
            catch (WebException e)
            {
                Console.WriteLine(e.Message);
            }

            return s_Exs.ToList();
        }

        public static bool SendExample(long userId, long wordId)
        {
            // Получаем пример
            List<string> exams = GetSentenceExemples(dictionary[wordId].eng, 5);
            if (exams == null || exams.Count == 0)
                return false;
            Random r = new Random(); 
            string ex = exams[r.Next(exams.Count)];

            // Получаем перевод 
            string translation = "";
            try
            {
                string request = "https://translate.yandex.net/api/v1.5/tr.json/translate?lang=en-ru&key=trnsl.1.1.20200331T105452Z.ece56d99f19664a4.384a062311e0d7549c5a10da8a9bd445752e64ab&text=" + ex;
                YandexTranslation tr = Methods.DeSerializationObjFromStr<YandexTranslation>(Methods.Request(request));
                translation = tr.text[0];
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in gettting translation in SendExample: wordId = " + wordId + ", userId = " + userId);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            // Получаем звук 
            try
            {
                Process sound = new Process();
                // Console.WriteLine(Environment.CurrentDirectory);
                sound.StartInfo.FileName = Users.GetPathOfFile(Environment.CurrentDirectory) + @"..\Speech\SpeechSynthesis.exe";
                sound.StartInfo.Arguments = wordId + " \"" + ex + "\"";
                sound.Start();

                sound.WaitForExit();

                string file_name = wordId + "_sound.wav";

                string url = bot.Api.Docs.GetMessagesUploadServer(userId, VkNet.Enums.SafetyEnums.DocMessageType.AudioMessage).UploadUrl;

                var uploader = new WebClient();
                var uploadResponseInBytes = uploader.UploadFile(url, file_name);
                var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
                var mess = bot.Api.Docs.Save(uploadResponseInString, "voice" + wordId + "_" + 0);

                List<VkNet.Model.Attachments.MediaAttachment> atts = new List<VkNet.Model.Attachments.MediaAttachment>();
                foreach (var a in mess)
                    atts.Add(a.Instance);

                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = userId,
                    Message = "Пример использования " + dictionary[wordId].eng.ToUpper() + ":\n" +
                    ex + "\n" + translation, 
                    Attachments = atts
                }) ;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong in Sending Sound in SendExample");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }

            Console.WriteLine("Sound example send to " + userId);
            return true; 
        }

        public static void ConvertMp3ToWav(string _inPath_, string _outPath_) 
        { 
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_)) 
            { 
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                { 
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm); 
                } 
            } 
        }

        public static void HandleAudioMessage(long userId, VkNet.Model.Attachments.Attachment message)
        {
            try
            {
                VkNet.Model.Attachments.AudioMessage mes = message.Instance as VkNet.Model.Attachments.AudioMessage;
                // bot.Api.Docs.Save(message.Type.);
                WebClient web = new WebClient();
                string file_sound_mp3 = userId + "_audio.mp3";
                string file_sound_wav = userId + "_audio.wav";
                
                // string file_sound_wav = userId + "_audio.ogg.wav";
                string file_text = userId + "_audio.txt";
                web.DownloadFile(mes.LinkMp3, file_sound_mp3);
                ConvertMp3ToWav(file_sound_mp3, file_sound_wav);

                // FileInfo f = new FileInfo(file_sound);
                // f.Replace(f.FullName, f.FullName + ".wav");
                // ToWav(file_sound);

                Process recognition = new Process();
                recognition.StartInfo.FileName = Users.GetPathOfFile(Environment.CurrentDirectory) + @"..\Speech\Recognition.exe";
                recognition.StartInfo.Arguments = file_sound_wav + " " + file_text;
                recognition.Start();
                recognition.WaitForExit();

                SendMessage(userId, "Вот, что я услышал: \n" + File.ReadAllText(file_text));
            }
            catch (Exception e)
            {
                Console.WriteLine("Something happened with recognizing audiomessage");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace); 
            }
            Console.WriteLine("Send recognition to " + userId); 
        }
    }
}
