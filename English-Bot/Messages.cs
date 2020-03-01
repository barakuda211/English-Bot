using System;
using System.Collections.Generic;
using System.Text;
using Project_Word;
using Dictionary;
using System.Net;
using English_Bot.Properties;

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
            string an = "Я не знаю такого слова :(";
            if (word[0] > 'a' && word[0] < 'Z')
            {
                var list = dictionary.GetEngWordIds(word);
                return list == null ? an : dictionary[list[0]].rus;
            }

            else
            {
                var list = dictionary.GetRusWordIds(word);
                return list == null ? an : dictionary[list[0]].eng;
            }
        }

        static void SendPicture(long id, long word)
        {
            Pictures pics = Methods.DeSerializationObj<Pictures>(Methods.Request(@"https://pixabay.com/api/?key=15427273-eddca1835086f92624a5b62a0&q=" + dictionary[word].eng + @"&image_type=photo&pretty=true"));
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(pics.hits[0].webformatURL, "picture.jpg");
            }
            string url = bot.Api.Photo.GetUploadServer(1).UploadUrl;
            string s = @"https://api.vk.com/method/METHOD_NAME?PARAMETERS&access_token=" + Resources.AccessToken;
            VkPhotoInfo answer = Methods.DeSerializationObj<VkPhotoInfo>(VkApi.VkRequests.Request(s));
            //bot.Api.Photo.Save(answer.server, answer.photos_list, answer.aid, answer.hash);
            //bot.Api.Messages.Send("photo" + id + "");
            //bot.Api.Photo.SaveMessagesPhoto(url);
            
        }
    }
}
