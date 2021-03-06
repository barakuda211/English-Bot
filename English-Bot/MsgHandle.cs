﻿using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using VkNet.Model;
using System.Collections.Generic;
using System.Threading;
using VkApi;

namespace English_Bot
{
    public partial class EngBot
    {
        const string ACCESS_IS_DENIED = "ACCESS IS DENIED";

        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {
            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId.Value;
            var fromId = eventArgs.Message.FromId.Value;
            var text = GetFormatedWord(eventArgs.Message.Text);
            var answer = "Извини, я понимаю только текстовые сообщения 🤔";

            if (!users.HasUser(fromId) || users[fromId].regId != 1)
            {
                Registration(eventArgs.Message);
                return;
            }
            
            if (eventArgs.Message.Attachments != null && eventArgs.Message.Attachments.Count != 0 && eventArgs.Message.Attachments[0].Instance is VkNet.Model.Attachments.AudioMessage) // x.Type.IsInstanceOfType(VkNet.Enums.SafetyEnums.DocMessageType.AudioMessage)))
            {
                HandleAudioMessage(fromId, eventArgs.Message.Attachments[0]);
                return; 
            }

            if (text == null && text.Length == 0)
            {
                SendMessage(fromId, answer, null, true);
                return;
            }
 
            users[fromId].lastMsg = (text, false, eventArgs.Message.ConversationMessageId.Value);
                        
            if (users[fromId].on_Test)
                return;

            var ss = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (ss.Length == 2)
            {
                if (ss[0] == "/examples")
                {
                    var lst = GetSentenceExemples(ss[1]);
                    if (lst == null || lst.Count != 0)
                        foreach (var s in lst)
                            SendMessage(fromId, s);
                    else
                        SendMessage(fromId, "Не могу привести пример с таким словом.");
                    return;
                }
                else if (ss[0] == "/sound")
                {
                    if (dictionary.eng_ids.ContainsKey(ss[1]))
                    {
                        SendExample(fromId, dictionary.eng_ids[ss[1]]);
                        return; 
                    }
                    else
                    {
                        answer = "Я не знаю такого слова"; 
                        goto Answer; 
                    }
                }
                else if (ss[0] == "/description")
                {
                    if (dictionary.eng_ids.ContainsKey(ss[1]))
                    {
                        SendFullWordDescription(fromId, dictionary.eng_ids[ss[1]]);
                        return;
                    }
                    else
                        answer = "Описание слова отсутствует";

                    SendMessage(fromId, answer, null, true);
                    return;
                }

            }
            // ----------------------------------------------------------------------------
            switch (text)
            {
                case "команды бота":
                case "/help":
                    users[fromId].keyb = User.Help_Keyboard;
                    answer = 
                                "/examples \'слово\'- примеры использования слова\n" +
                                "/description \'слово\' - описание слова\n" + 
                                "/sound \'слово\' - пример с озвучиванием\n" +
                                "\'слово на русском\' - перевод на английский\n" +
                                "\'слово на английском\' - перевод на русский\n" + 
                                "\'аудиосообщение на английском\' - распознавание речи\n" + 
                                "\'текст на английском\' - перевод всех известных боту слов на русский\n";
                    break;
                case "моя статистика":
                case "/my_level":
                    answer = GetStatics(fromId);
                    break;
                case "сменить уровень":
                case "/change_level":
                    ChangingLevel_Start(fromId);
                    return;
                case "сменить сложность":
                case "/change_complexity":
                    ChangingComplexity_Start(fromId);
                    return;
                case "игра кроссворд":
                case "/crossword":
                    Games.Crossvord_start(fromId);
                    return;
                case "игра виселица":
                case "/gallows":
                    Games.Gallows_Start(fromId);
                    return; 
                case "/examples":
                    answer = "А к чему пример то?)";
                    break;
                case "/description":
                    answer = "Нужно написать и само слово";
                    break;
                case "хватит меня учить":
                case "/mute":
                    users[fromId].bot_muted = true;
                    answer = "Бот перешел в режим ожидания\nОн не будет присылать слова и тесты, но по прежнему будет выполнять команды";
                    break;
                case "нет, учи меня":
                case "/unmute":
                    users[fromId].bot_muted = false;
                    answer = "Боты вернулся к стандартному режиму работы"; 
                    break;
                case "добавить слова":
                case "/my_list":
                    AddingWords_Start(fromId);
                    return;
                case "/repeat":
                case "повторить слова":
                    if (users[fromId].learnedWords.Count < users[fromId].day_words)
                    {
                        answer = "Вы изучили недостаточно слов для проверки";
                        break;
                    }
                    else
                    {
                        Testing_Start(fromId, true);
                        return;
                    }
                case "вернуться назад":
                case "/back":
                    users[fromId].keyb = User.Main_Keyboard;
                    answer = "Теперь ты в главном меню";
                    break;
                case "/daywords":
                case "кол-во слов в день":
                    DayWords_Start(fromId);
                    return;

                case "admin::getpicture":
                    if (adminIDs.Contains(fromId))
                    {
                        foreach (var x in users[fromId].unLearnedWords)
                            SendPicture(fromId, x);
                    }
                    else answer = ACCESS_IS_DENIED;
                    return;
                case "admin::getсommands":
                    if (adminIDs.Contains(fromId))
                        answer = "getId, wantTest, getCommands, usersCount, forget_me, getpicture";
                    else answer = ACCESS_IS_DENIED;
                    break;
                case "admin::forget_me":
                    if (adminIDs.Contains(fromId))
                    {
                        SendMessage(fromId, "Я тебя забыл.");
                        users.DeleteUser(fromId);
                    }
                    else answer = ACCESS_IS_DENIED;
                    return;
                case "admin::getid":
                    if (adminIDs.Contains(fromId))
                        answer = fromId.ToString();
                    else answer = ACCESS_IS_DENIED;
                    break;
                case "admin::userscount":
                    if (adminIDs.Contains(fromId))
                        answer = "" + users.Dbase.Count;
                    else answer = ACCESS_IS_DENIED;
                    break;
                case "admin::wanttest":
                    if (adminIDs.Contains(fromId))
                    {
                        users[fromId].on_Test = true;
                        Testing_Start(fromId, false);
                        return;
                    }
                    else 
                        answer = ACCESS_IS_DENIED;
                    break;
                default:
                    if (ss.Length == 1)
                        answer = Translation(text);
                    else
                        answer = MultipleTranslation(ss, users[fromId].userLevel);
                    break;
            }  
            Answer:
            SendMessage(fromId, answer, null, true);
        }

        static void DayWords_Start(long id)
        {
            users[id].on_Test = true;
            users[id].keyb = User.DayWordsKeyb(users[id].day_words);
            Thread DayWordsthread = new Thread(new ParameterizedThreadStart(DayWords));
            DayWordsthread.Start(id);
        }

        static void DayWords(object obj)
        {
            long id = (long)obj;
            var user = users[id];
            SendMessage(id, "Выберите количество слов для изучения в день.", null, true);
            var x = WaitWordFromUser_with_Comments(id, new string[] { "1", "2", "3", "4", "5", "6","7","8","9","10" }, 3);
            user.keyb = User.Main_Keyboard;
            user.on_Test = false;
            if (x == "time")
                return;
            user.day_words = int.Parse(x);
            SendMessage(id, "Готово!", null, true);
            users.Save();
        }

        static string GetStatics(long id)
        {
            var user = users[id];
            string ans = $"Вы на {users.Place_in_rating(id)} месте по эффективности обучения.\n\n" +
                         $"Уровень: {user.userLevel}\n" +
                         $"Режим изучения: {(user.mode == Users.Mode.Easy ? "лёгкий" : "сложный")}\n" +
                         $"Слов изучено: {user.learnedWords.Count()}\n" +
                         $"Тестов пройдено: {user.tests_passed}\n" +
                         $"Кроссвордов решено: {user.cross_passed}\n" +
                         $"Виселиц решено: {user.gall_passed}\n" +
                         $"Кол-во слов в день: {user.day_words}\n" ;
            return ans;
        }


        static void AddingWords_Start(long id)
        {
            users[id].on_Test = true;
            users[id].keyb = User.Back_Keyboard;
            Thread AddingWordsthread = new Thread(new ParameterizedThreadStart(AddingWords));
            AddingWordsthread.Start(id);
        }

        static void AddingWords(object Idobj)
        {
            long id = (long)Idobj;
            var user = users[id];
            SendMessage(id, "Эта функция позволит изучать заданные тобою английские слова.", null, true);
            SendMessage(id, "Пришли мне слова на английском через запятую.", null, true);
            WaitWords(id);
            user.on_Test = false;
        }

        static void WaitWords(long id, int wait_time = 15, string error_msg = "Ладно, давай в другой раз")
        {
            var user = users.GetUser(id);
            var ind = Timers.IndicatorTimer(wait_time);

            long ident_msg = user.lastMsg.Item3;
            while (true)
            {
                if (ind.x)
                {
                    user.keyb = User.Main_Keyboard;
                    SendMessage(id, error_msg, null, true);
                    return;
                }
                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);
                    continue;
                }
                ident_msg = user.lastMsg.Item3;
                var text = GetFormatedWord(user.lastMsg.Item1);
                if (text == "вернуться назад")
                {
                    user.keyb = User.Main_Keyboard;
                    SendMessage(id, error_msg, null, true);
                    return;
                }
                var errors = user.AddWords(text);
                user.keyb = User.Main_Keyboard;

                if (errors.Item1.Length == 0 && errors.Item2 == 0)
                {
                    SendMessage(id, "Я тут ничего не разобрал🙃",null,true);
                    return;
                }

                if (errors.Item2 != 0)
                    SendMessage(id, $"Добавлено слов: {errors.Item2}.",null,true);
                if (errors.Item1.Length > 0)
                {
                    var str = "Данные слова я не распознал🙃: ";
                    for (int i = 0; i < errors.Item1.Length; i++)
                    {
                        if (i + 1 == errors.Item1.Length)
                            str += errors.Item1[i] + ".";
                        else
                            str += errors.Item1[i] + ", ";
                    }
                    SendMessage(id, str);
                    SendMessage(id, "Убедитесь в правильности их написания и попробуйте их добавить ещё раз.",null,true);

                }
                return;
            }
        }


        static void ChangingComplexity_Start(long id)
        {
            users[id].on_Test = true;
            users[id].keyb = User.Complexity_Keyboard;
            Thread changing_thread = new Thread(new ParameterizedThreadStart(ChangeComplexity));
            changing_thread.Start(id);
        }

        static void ChangeComplexity(object Idobj)
        {
            long id = (long)Idobj;
            var user = users[id];
            SendMessage(id, "Выберите один из вариантов:");
            SendMessage(id, "Лёгкий - выбор из предложенных вариантов ответов на тестировании.");
            SendMessage(id, "Сложный - полностью самостоятельный перевод слов на тестировании.", null, true);
            var x = WaitWordFromUser_with_Comments(id, new string[] { "легкий", "сложный" }, 2,"Ладно, потом...");
            user.keyb = User.Main_Keyboard;
            user.on_Test = false;
            if (x == "time") 
                return;
            if ((x == "легкий" && user.mode == Users.Mode.Easy)|| (x == "сложный" && user.mode == Users.Mode.Hard))
                SendMessage(id, "У вас уже выбрана данная сложность.", null, true);
            else
            {
                if (x == "легкий")
                    user.mode = Users.Mode.Easy;
                else
                    user.mode = Users.Mode.Hard;
                SendMessage(id, "Готово!", null, true);
            }
            users.Save();
        }

        static void ChangeLevel(object Idobj)
        {
            long id = (long)Idobj;
            var user = users[id];
            int userlevel = user.userLevel;
            var text =     "Вы на "+userlevel+" уровне изучения.\n"+
                           "Не нравится? Выберите один из следующих:\n"+
                           "1 - учил в школе немецкий\n"+
                           "2 - прогуливал английский\n"+
                           "3 - хорошист по английскому\n"+
                           "4 - занимался с репетитором\n"+
                           "5 - уверенный носитель языка\n"+
                           "-1 - ну точно иностранец";
            SendMessage(id,text,null,true);
            var x = WaitWordFromUser_with_Comments(id,new string[] { "1", "2", "3", "4", "5", "-1" }, 3);
            user.keyb = User.Main_Keyboard;
            user.on_Test = false;
            if (x == "time")
                return;
            user.ChangeLevel(int.Parse(x));
            SendMessage(id,"Готово!",null,true);
            users.Save();
        }

        static void ChangingLevel_Start(long id)
        {
            users[id].on_Test = true;
            users[id].keyb = User.ChangingLevelKeyb(users[id].userLevel);
            Thread changing_thread = new Thread(new ParameterizedThreadStart(ChangeLevel));
            changing_thread.Start(id);
        }

        //ждет ответа !определенного! ответа от юзера, 
        //Просит повторить ввод
        static string WaitWordFromUser_with_Comments(long userID, string[] words, int wait_time, string time_error_msg = "Ладно, давай потом.",string error_msg = "Этого я не ждал!")
        {
            var user = users.GetUser(userID);
            var ind = Timers.IndicatorTimer(wait_time);

            long ident_msg = user.lastMsg.Item3;
            while (true)
            {
                if (ind.x)
                {
                    user.keyb = User.Main_Keyboard;
                    SendMessage(userID, time_error_msg,null,true);
                    return "time";
                }
                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);  //ожидание согласия
                    continue;
                }
                ident_msg = user.lastMsg.Item3;
                var text = GetFormatedWord(user.lastMsg.Item1);
                foreach (var w in words)
                {
                    if (w == text)
                    { 
                        user.lastMsg.Item2 = true;
                        return w;
                    }
                }
                SendMessage(userID,error_msg);
            }
        }

        static string SendInfo(Message msg) => $"{msg.PeerId.Value}, i have captured your message: '{msg.Text}'. its length is {msg.Text.Length}. number of spaces: {msg.Text.Count(x => x == ' ')}";
    }
}