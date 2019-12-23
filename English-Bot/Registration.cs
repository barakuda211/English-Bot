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
            VkUser vk = VkRequests.VkRequestUser(msg.FromId);
            User user = new User(vk);
            if (UsersDictionary.AddUser(user))
                return "Привет, " + user.name + "! Твой id = " + user.userId + ". Теперь я тебя знаю!";
            switch (UsersDictionary[msg.FromId].regId)
            {
                case 0: return "redId ="+UsersDictionary[msg.FromId].regId++;
                    break;
                case 1:
                    return "redId =" + UsersDictionary[msg.FromId].regId++;
                    break;
                default: return "end!"; 
                    break;
            }
        }
    }
}
