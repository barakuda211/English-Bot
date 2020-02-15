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
            message += "" + word.eng.ToUpper() + "\n";
            message += "Определения:\n";
            foreach (var def in word.mean_rus.def)
            {
                message += def.text + " " + def.pos + ". -> " + def.tr[0].text + "\n";
            }
            SendMessage(userId, message);
        }
    }
}
