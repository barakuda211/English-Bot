using System;
using System.Collections.Generic;
using System.Text;
using Project_Word;

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
    }
}
