using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using English_Bot;

namespace English_Bot
{
    public static class Timers
    {
        /*
        /// <summary>
        /// Метод ждёт указанное время, после чего меняет поле onTest указанного юзера на false и выводит сообщение о прекращении работы
        /// </summary>
        /// <param name="Время в минутах"></param>
        /// <param name="Id пользователя"></param>
        /// <param name="Сообщение для вывода"></param>
        public static Thread TestTimer(int minutes,long id, string message = "Ну раз не хочешь, то ладно(" )
        {
            if (!EngBot.users[id].on_Test)
                return null;
            Thread timer_thread = new Thread(new ParameterizedThreadStart(MinutesIdMessage));
            timer_thread.Start((minutes,id,message));
            return timer_thread;
        }


        public static void MinutesIdMessage(object obj)
        {
            var tuple = (ValueTuple<int,long,string>)obj;
            int minutes = tuple.Item1;
            long id = tuple.Item2;
            string message = tuple.Item3;
            for (int i =0;i<minutes*60;i++)
            {
                Thread.Sleep(1000);
                if (!EngBot.users[id].on_Test)
                    return;
            }
            EngBot.users[id].on_Test = false;
            EngBot.SendMessage(id, message);
        }
        */

        //Класс для индикации
        public class Indicator
        {
            public bool x;

            public Indicator()
            {
                x = false;
            }
                
        }

        /// <summary>
        /// Создаёт таймер, который меняет индикатор на true по истечении указанного времени
        /// Останавливается сам, если индикатору присвоить true  
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static Indicator IndicatorTimer(int minutes)
        {
            Indicator ind = new Indicator();
            Thread timer_thread = new Thread(new ParameterizedThreadStart(MinutesIndicator));
            timer_thread.Start((minutes,ind));
            return ind;
        }

        public static void MinutesIndicator(object obj)
        {
            var tuple = (ValueTuple<int, Indicator>)obj;
            int minutes = tuple.Item1;
            Indicator ind = tuple.Item2;
            for (int i =0;i<minutes*60;i++)
            {
                Thread.Sleep(1000);
                if (ind.x)
                    return;
            }
            ind.x = true;
        }
    }
}
