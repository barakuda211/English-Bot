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
            UsersDictionary.AddUser(user);
            return "Привет, "+user.name+"! Твой id = "+user.userId+". Теперь я тебя знаю!";
        }
    }
}
