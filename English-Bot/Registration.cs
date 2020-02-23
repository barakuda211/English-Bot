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

        static void Registration(Message msg)
        {
            var id = msg.FromId.Value;
            if (!users.HasUser(id))
            {
                VkUser vk = VkRequests.VkRequestUser(id);  //получаем юзера из вк
                User us = new User(vk, dictionary);
                users.AddUser(us);  //создаём нового юзера
            }
            User user = users[id];
            user.regId++;
            Console.WriteLine("Registered: " + user.name + " " + user.userId);
            Testing_Start(id);
            /*
            switch (user.regId++)
            {
                case 0:
                    Console.WriteLine("Registered: "+ user.name+" "+user.userId);
                    Testing_Start(id);
                    return "Приветствую, " + user.name + "!\n" +
                        "Вам будет предложен тест на знание английских слов.\n" +
                        "Не стоит подсматривать, от результатов теста зависит ваша дальнейшая программа обучения.\n";
                case 1:
                    if(msg.Text.ToLower() == "готов")
                        return "redId =" + user.regId;
                    break;
                default: return "end!"; 
            }

            return "You cannot see it!";
            */
        }
    }
}
