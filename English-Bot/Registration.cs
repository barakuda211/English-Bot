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
            //UsersDictionary.AddUser(new User(msg.FromId));
            User user = new User(VkRequests.VkRequestUser(msg.FromId));
            
            return "Registration Error";
        }
    }
}
