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
using NAudio;
using Alvas.Audio;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using static System.Math;

namespace English_Bot
{
    partial class EngBot
    {
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
                    add_word = new Word(add_id, word, mean_rus.def[0].ts, mean_rus.def[0].tr[0].text, mean_eng, mean_rus, null, null, -1, new HashSet<string> { "rus_only" });
                else if (mean_rus.def.Count == 0 && mean_eng.def.Count > 0)
                    add_word = new Word(add_id, word, mean_eng.def[0].ts, null, mean_eng, mean_rus, null, null, -1, new HashSet<string> { "eng_only" });
                else add_word = new Word(add_id, word, mean_eng.def[0].ts, mean_rus.def[0].tr[0].text, mean_eng, mean_rus, null, null, -1, null);

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
        /// <param name="wordId"></param>
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

        static string Translation(string word)
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

        static string MultipleTranslation(string[] text)
        {
            string answer = "";
            foreach (var word in text)
            {
                if (dictionary.eng_ids.ContainsKey(word.ToLower()))
                    answer += word + " -> " + Translation(word.ToLower()) + "\n";
            }
            return answer;
        }

        static bool SendPicture(long id, long word)
        {
            try
            {
                // Получаем адрес изображения 
                string answer = Methods.Request(@"https://pixabay.com/api/?key=15427273-eddca1835086f92624a5b62a0&q=" + dictionary[word].eng + @"&image_type=photo&pretty=true");
                // Console.WriteLine(answer);
                Pictures pics = Methods.DeSerializationObjFromStr<Pictures>(answer);

                // Скачиваем картинку
                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(pics.hits[0].webformatURL, word + "_picture.jpg");
                }

                // Создаем обЪекты для рисования
                Image bitmap = Image.FromFile(word + "_picture.jpg");
                
                //Bitmap pic = new Bitmap(bitmap);
                //pic.SetResolution(300, 300);

                //string text = dictionary[word].eng + "\n" + dictionary[word].mean_eng.def[0].ts + "\n" + dictionary[word].rus;
                Graphics graphImage = Graphics.FromImage(bitmap);
                graphImage.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // Определяем шрифт, а также сохраняем ширину и длину изображения
                Random r = new Random(17);
                int font = r.Next(0, FontFamily.Families.Length - 1);
                int width = pics.hits[0].webformatWidth;
                int height = pics.hits[0].webformatHeight;

                // Затемняем фон
                Color col = Color.FromArgb(63, 0, 0, 0);
                graphImage.FillRectangle(new SolidBrush(col), new Rectangle(0, 0, width, height));
                // bitmap.Save(word + "_dark.jpg");

                // Выравнивание текста по центру 
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;
                stringFormat.FormatFlags = StringFormatFlags.FitBlackBox;

                /*
                HatchBrush hBrush = 
                    new HatchBrush(
                        HatchStyle.Trellis,
                        Color.Red,
                        Color.FromArgb(255, 128, 255, 255));
   

                var path = new GraphicsPath();
                path.AddRectangle(new Rectangle(0, 0, 640, 480));
                PathGradientBrush pthGrBrush = new PathGradientBrush(path);
                Color[] colors = { Color.FromArgb(255, 0, 255, 255), Color.Black, Color.White };
                pthGrBrush.SurroundColors = colors;

                LinearGradientBrush brush =
                    new LinearGradientBrush(
                        new Point(0, 0),
                        new Point(640, 480),
                        Color.FromArgb(255, 0, 0),
                        Color.FromArgb(0, 155, 255)
                        );

                var t = (Image)bitmap.Clone();
                t.RotateFlip(RotateFlipType.RotateNoneFlipX);
                TextureBrush tBrush = new TextureBrush(t); 
                */

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
                    text = dictionary[word].mean_rus.def[0].tr[0].text; //string.Join('/', dictionary[word].mean_rus.def.Select(x => x.tr[0].text));
                    graphImage.DrawString(
                        text,
                        new Font(new FontFamily(genericFamily: System.Drawing.Text.GenericFontFamilies.SansSerif),  emSize / (text.Length < 7 ? 7 : text.Length) /* pixSize */ /*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular /* , GraphicsUnit.Pixel */ ),
                        //new SolidBrush(ColorTranslator.FromHtml("#FFFFFF")),
                        tBrush,
                        new Point(width / 2,
                        height / 2 + (height / 4)),
                        new StringFormat(stringFormat));
                }

                // Сохраняем преобразованное изображение
                bitmap.Save(word + "_picture_with_str.jpg");

                // System.Threading.Thread.Sleep(100); 

                // Отправляем сообщение пользователю
                string url = bot.Api.Photo.GetMessagesUploadServer(id).UploadUrl;
                var uploader = new WebClient();
                var uploadResponseInBytes = uploader.UploadFile(url, word + "_picture_with_str.jpg");
                var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
                // VKRootObject response = Methods.DeSerializationObjFromStr<VKRootObject>(uploadResponseInString);
                var photos = bot.Api.Photo.SaveMessagesPhoto(uploadResponseInString);
                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = id,
                    Attachments = photos
                });

                // Удаляем сохоаненные фотографии
                try
                {
                    File.Delete(word + "_picture_with_str.jpg");
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
                }
            }
            return file + ".ogg";
        }

        /*
        static void ToOgg(string file)
        {
            Sox.Convert(@"c:\Program Files (x86)\sox-14-4-1\sox.exe", file, file + ".ogg");
        }
        */

        static void SendSound(long id, long word)
        {
            try
            {
                SpeechSynthesizer speechSynth = new SpeechSynthesizer();
                string file_name = word + "_sound";
                {
                    speechSynth.SetOutputToWaveFile(file_name + ".wav");
                    speechSynth.Volume = 50;
                    PromptBuilder p = new PromptBuilder(System.Globalization.CultureInfo.GetCultureInfo("en-IO"));
                    // p.AppendText(dictionary[word].eng + ". " + dictionary[word].mean_rus.def.Select(x => x.tr[0].ex[0].text + ". "));
                    var list = GetSentenceExemples(dictionary[word].eng);
                    p.AppendText((list != null && list.Count > 0) ? list[0] : (dictionary[word].eng + ". " + dictionary[word].mean_rus.def.Select(x => x.tr[0].ex[0].text + ". ")));
                    speechSynth.Speak(p);
                }
                /*
                using (NAudio.Wave.WaveFileReader reader = new NAudio.Wave.WaveFileReader(file_name))
                {
                    // :3
                }
                */

                file_name = ToOgg(file_name + ".wav");

                string url = bot.Api.Docs.GetMessagesUploadServer(id, VkNet.Enums.SafetyEnums.DocMessageType.AudioMessage).UploadUrl;

                var uploader = new WebClient();
                var uploadResponseInBytes = uploader.UploadFile(url, file_name);
                var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
                // var voice = Methods.DeSerializationObjFromStr<VkVoice>(uploadResponseInString);
                // VKRootObject response = Methods.DeSerializationObjFromStr<VKRootObject>(uploadResponseInString);
                var mess = (System.Collections.ObjectModel.ReadOnlyCollection<VkNet.Model.Attachments.MediaAttachment>)bot.Api.Docs.Save(uploadResponseInString, "voice" + word).Select(x => x.Instance);
                //(System.Collections.ObjectModel.ReadOnlyCollection<VkNet.Model.Attachments.MediaAttachment>)

                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = id,
                    Attachments = mess
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Something wrong in Sending Sound");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        } 

        static void SendRecognition(EventArgs message)
        {
            SpeechRecognizer rec = new SpeechRecognizer(); 
            
        }

        static List<string> GetSentenceExemples(string word, int cnt = 5)
        {
            //HashSet<string> s_Exs = new HashSet<string>();
            List<string> s_Exs = new List<string>();
            //if (dictionary.GetRusWordIds(word).Count > 0)

            if (word[0] >= 'а' && word[0] <= 'я')       //заглушка
                return new List<string>() { "Я ожидал английское слово." };

            {
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
            }
            return s_Exs.ToList();
        }
    }
}
