﻿using Project_Word;
using Dictionary;
using System.Net;
using English_Bot.Properties;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Text;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using NAudio; 

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
            if (word.mean_eng != null && word.mean_eng.def != null)
                message += word.eng.ToUpper() + " [" + word.mean_eng.def[0].ts + "]\n";
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
            SendMessage(userId, message);
            //return message; 
        } 

        static string Translation(string word)
        {
            word = word.Trim().ToLower(); 
            string an = "Я не знаю такого слова :(";
            if (word[0] >= 'A' && word[0] <= 'z')
            {
                return !dictionary.eng_ids.ContainsKey(word) ? an : string.Join('/', from def in dictionary[dictionary.eng_ids[word]].mean_rus.def select def.tr[0].text);
            }
            else
            {
                var list = dictionary.rus_ids.ContainsKey(word) ? dictionary.rus_ids[word] : null;
                return (list == null || list.Count == 0) ? an : string.Join('/', list.Select(x => dictionary[x]?.eng));
            }
        }

        static void SendPicture(long id, long word)
        {
            try
            {
                Pictures pics = Methods.DeSerializationObjFromStr<Pictures>(Methods.Request(@"https://pixabay.com/api/?key=15427273-eddca1835086f92624a5b62a0&q=" + dictionary[word].eng + @"&image_type=photo&pretty=true"));

                using (WebClient webClient = new WebClient())
                {
                    webClient.DownloadFile(pics.hits[0].webformatURL, word + "_picture.jpg");
                }
                string url = bot.Api.Photo.GetMessagesUploadServer(id).UploadUrl;

                Image bitmap = (Image)Bitmap.FromFile(word + "_picture.jpg");
                //string text = dictionary[word].eng + "\n" + dictionary[word].mean_eng.def[0].ts + "\n" + dictionary[word].rus;
                Graphics graphImage = Graphics.FromImage(bitmap);
                Random r = new Random(17);
                int font = r.Next(0, FontFamily.Families.Length - 1);
                string text = dictionary[word].eng;
                int width = pics.hits[0].webformatWidth;
                int height = pics.hits[0].webformatHeight;
                int font_size = 36; // 3 * (width / 50) - 4;
                int tr_size = 32; // 2 * (width / 100); 
                graphImage.DrawString(text, new Font(FontFamily.Families[font].Name, font_size, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml("#000000")), new Point(width / 2 - (int)((text.Length / (double)2) * font_size * 2), height / 2 - (height / 4)), new StringFormat(StringFormatFlags.NoClip));
                text = "[" + dictionary[word].mean_eng.def[0].ts + "]";
                graphImage.DrawString(@text, new Font(FontFamily.Families[font].Name, font_size, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml("#000000")), new Point(width / 2 - (int)((text.Length / (double)2) * font_size * 2), height / 2 + 50), new StringFormat(StringFormatFlags.NoClip));
                text = string.Join('/', dictionary[word].mean_rus.def.Select(x => x.tr[0].text));
                graphImage.DrawString(text, new Font(FontFamily.Families[font].Name, tr_size, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml("#000000")), new Point(width / 2 - (int)((text.Length / (double)2) * font_size) + 10, height / 2 + 25*(height / 100)), new StringFormat(StringFormatFlags.NoClip));
                bitmap.Save(word + "_picture_with_str.jpg");

                // System.Threading.Thread.Sleep(100); 

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
                }) ;
            }
            catch (Exception)
            { return; }
        }
        /*
        static void ToWav()
        {
            string fileName = @"e:\down\male.ogg";
            using (DsReader dr = new DsReader(fileName))
            {
                if (dr.HasAudio)
                {
                    IntPtr format = dr.ReadFormat();
                    using (WaveWriter ww = new WaveWriter(File.Create(fileName + ".wav"),
                        AudioCompressionManager.FormatBytes(format)))
                    {
                        byte[] data = dr.ReadData();
                        ww.WriteData(data);
                    }
                }
            }
        }

        static void OggToWavWithOgg()
        {
            string fileName = @"e:\Down\fortuna.ogg";
            using (DsReader dr = new DsReader(fileName))
            {
                if (dr.HasAudio)
                {
                    short oggFormatTag = 26479;
                    IntPtr format = dr.ReadFormat();
                    WaveFormat waveFormat = AudioCompressionManager.GetWaveFormat(format);
                    IntPtr formatOgg = AudioCompressionManager.GetCompatibleFormat(format, oggFormatTag);
                    using (WaveWriter ww = new WaveWriter(File.Create(fileName + ".wav"),
                        AudioCompressionManager.FormatBytes(formatOgg)))
                    {
                        AudioCompressionManager.Convert(dr, ww, true);
                    }
                }
            }
            */
        static void SendSound(long id, long word)
        {
            SpeechSynthesizer speechSynth = new SpeechSynthesizer();
            speechSynth.Volume = 50;
            PromptBuilder p = new PromptBuilder(System.Globalization.CultureInfo.GetCultureInfo("en-IO"));
            p.AppendText("Hello world, I'm programming very well, and its great!!");
            speechSynth.Speak(p);
            string file_name = id + "_sound";
            speechSynth.SetOutputToWaveFile(file_name + ".wav");

            using (NAudio.Wave.WaveFileReader reader = new NAudio.Wave.WaveFileReader(file_name))
            {
                // :3
            }
        }
        
    }
}
