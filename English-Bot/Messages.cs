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
            SendMessage(userId, message == "" ? "Ошибка" : message);
            //return message; 
        } 

        static string Translation(string word)
        {
            word = word.Trim().ToLower(); 
            string an = "Я не знаю такого слова :(";
            if (word[0] >= 'A' && word[0] <= 'z')
            {
                try
                {
                    if (!dictionary.eng_ids.ContainsKey(word))
                        return an;
                    else
                    {
                        if (dictionary[dictionary.eng_ids[word]].mean_rus == null)
                            return "Перевод отсуттвует"; 
                        else 
                            return string.Join(", ", from def in dictionary[dictionary.eng_ids[word]].mean_rus.def select def.tr[0].text);
                    }
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
                    word = string.Join("", word.Select(x => x == 'ё' ? 'е' : x));
                    var list = dictionary.rus_ids.ContainsKey(word) ? dictionary.rus_ids[word] : null;
                    return (list == null || list.Count == 0) ? an : string.Join(", ", list.Select(x => dictionary[x]?.eng));
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }
            }
            return an; 
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
                Image bitmap = (Image)Bitmap.FromFile(word + "_picture.jpg");
                //string text = dictionary[word].eng + "\n" + dictionary[word].mean_eng.def[0].ts + "\n" + dictionary[word].rus;
                Graphics graphImage = Graphics.FromImage(bitmap);

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
                float emSize = height * 125 / graphImage.DpiY;
                //int size = (int)(125 / graphImage.DpiX);
                // float maxf = System.Single.MaxValue;
                
                // Выюираем белый цвет
                var tBrush = new SolidBrush(Color.White);
                
                // Наносим слово на английском
                string text = dictionary[word].eng;
                graphImage.DrawString(
                    text,
                    new Font(FontFamily.Families[font].Name, emSize / text.Length/*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular),
                    //new SolidBrush(ColorTranslator.FromHtml("#FFFFFF")),                   
                    tBrush,
                    new Point(width / 2,
                            height / 2 - (height / 4)),
                    new StringFormat(stringFormat));

                // Пишем транскрипцию 
                text = "[" + ((dictionary[word].tags != null && dictionary[word].tags.Contains("eng_only")) ? dictionary[word].mean_eng.def[0].ts : dictionary[word].mean_rus.def[0].ts) + "]";
                graphImage.DrawString(
                    @text,
                    new Font(FontFamily.Families[font].Name, emSize / text.Length/*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular),
                    //new SolidBrush(ColorTranslator.FromHtml("#FFFFFF")),
                    tBrush,
                    new Point(width / 2,
                            height / 2 /* - (height / 10)*/),
                    new StringFormat(stringFormat));

                // Добавляем перевод 
                if (!(dictionary[word].tags != null && dictionary[word].tags.Contains("eng_only")))
                {
                    text = dictionary[word].mean_rus.def[0].tr[0].text; //string.Join('/', dictionary[word].mean_rus.def.Select(x => x.tr[0].text));
                    graphImage.DrawString(
                        text,
                        new Font(FontFamily.Families[font].Name, emSize / text.Length/*Min((float)width / text.Length * size, 80)*/, FontStyle.Regular),
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
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error downloading photo: ID = " + id + ", Word id = " + word + ", Word = " + dictionary[word].eng);
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }
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
            SpeechSynthesizer speechSynth = new SpeechSynthesizer();
            speechSynth.Volume = 50;
            PromptBuilder p = new PromptBuilder(System.Globalization.CultureInfo.GetCultureInfo("en-IO"));
            p.AppendText(dictionary[word].eng + ". " + dictionary[word].mean_rus.def.Select(x => x.tr[0].ex[0].text + ". "));
            //speechSynth.Speak(p);
            string file_name = word + "_sound";
            speechSynth.SetOutputToWaveFile(file_name + ".wav");

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
            var mess = bot.Api.Docs.SaveAsync(uploadResponseInString, "voice" + word);
            
            /*
            bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                RandomId = Environment.TickCount64,
                UserId = id,
                Attachments = mess
            }) ;
            */
        } 

        static void SendRecognition(EventArgs message)
        {
            SpeechRecognizer rec = new SpeechRecognizer(); 
            
        }

        static List<string> GetWordExemples(string word)
        {
            //consist ? ok! : throw new Ex or return null

            Regex r_Exs = new Regex(@"(?<1>[^>]*)\<em\>" + word + @"\<\/em\>(?<2>[^<]*)");
            string html = new WebClient().DownloadString("https://context.reverso.net/translation/english-russian/swim");
            List<string> s_Exs = new List<string>(html.Length);
            foreach (Match m in r_Exs.Matches(html))
            {
                s_Exs.Add(m.Groups["1"].Value.Substring(1, m.Length-2).TrimStart(' ') +
                            word +
                            m.Groups["2"].Value.Substring(1, m.Length-2));
            }
            return s_Exs;
        }
    }
}
