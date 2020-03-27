using Project_Word;
using Dictionary;
using System.Net;
using English_Bot.Properties;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Text;
using System.Linq; 

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
        } 

        static string Translation(string word)
        {
            word = word.Trim().ToLower(); 
            string an = "Я не знаю такого слова :(";
            if (word[0] >= 'A' && word[0] <= 'z')
            {
                var list = dictionary.GetEngWordIds(word);
                return list == null ? an : (string.Join('/', from def in dictionary[list[0]].mean_rus.def select def.tr[0].text));
            }
            else
            {
                var list = dictionary.GetRusWordIds(word);
                return list == null ? an : dictionary[list[0]].eng;
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
                graphImage.DrawString(text, new Font(FontFamily.Families[font].Name, 46, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml("#000000")), new Point(pics.hits[0].webformatWidth / 2 - (int)((text.Length / (double)2) * 30), pics.hits[0].webformatHeight / 2 - 70), new StringFormat(StringFormatFlags.NoClip));
                text = "[" + dictionary[word].mean_eng.def[0].ts + "]";
                graphImage.DrawString(@text, new Font(FontFamily.Families[font].Name, 32, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml("#000000")), new Point(pics.hits[0].webformatWidth / 2 - (int)((text.Length / (double)2) * 18), pics.hits[0].webformatHeight / 2), new StringFormat(StringFormatFlags.NoClip));
                text = string.Join('/', dictionary[word].mean_rus.def.Select(x => x.tr[0].text));
                graphImage.DrawString(text, new Font(FontFamily.Families[font].Name, 36, FontStyle.Regular), new SolidBrush(ColorTranslator.FromHtml("#000000")), new Point(pics.hits[0].webformatWidth / 2 - (int)((text.Length / (double)2) * 30), pics.hits[0].webformatHeight / 2 + 50), new StringFormat(StringFormatFlags.NoClip));
                bitmap.Save(word + "_picture_with_str.jpg");

                System.Threading.Thread.Sleep(100); 

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

        static void SendSound(long id, long word)
        {
            
        }
    }
}
