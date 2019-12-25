using System;
using System.Collections.Generic;
using System.Text;
using VkBotFramework;
using English_Bot.Properties;
using VkNet.Model;
using VkApi;

namespace English_Bot
{
    partial class EngBot
    {

        static string Registration(Message msg)
        {
            var id = msg.FromId;
            if (!UsersDictionary.HasUser(msg.FromId))
            {
                VkUser vk = VkRequests.VkRequestUser(msg.FromId);  //получаем юзера из вк
                User user = new User(vk, wordsDictionary);
                UsersDictionary.AddUser(user);  //создаём нового юзера
            }
            switch (UsersDictionary[msg.FromId].regId++)
            {
                case 0:
                    Console.WriteLine("Registered: "+UsersDictionary[id]);
                    return "redId ="+UsersDictionary[msg.FromId].regId;
                    break;
                case 1:
                    return "redId =" + UsersDictionary[msg.FromId].regId;
                    break;
                default: return "end!"; 
                    break;
            }
        }
    }
}
