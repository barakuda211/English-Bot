using System;
using System.Collections.Generic;

namespace English_Bot 
{
    public class Users
    {
       private  Dictionary<long?,User>  Dbase;

        //индексатор
        public User this[long? idex]
        {

            set { Dbase[idex] = value; }
            get { return Dbase[idex]; }
        }

        public Users()
        {
            Dbase = new Dictionary<long?, User>(); 
        }

        //Пользователь по id
       public User GetUser(long? id)
        {
            if (Dbase.ContainsKey(id))
                return Dbase[id];
            else
                return null;
        }

        //Добавление пользователя
      public  bool AddUser(User sr)
        {
            if (Dbase.ContainsKey(sr.userId))
                return false;
            else
            {
                Dbase.Add(sr.userId, sr);
                return true;
            }
        }

        //Удаление пользователя
        public bool DeleteUser(long? id)
        {
            if (!Dbase.ContainsKey(id))
                return false;
            else
            {
                Dbase.Remove(id);
                return true;
            }
        }

        //Проверка наличия пользователя
        public bool HasUser(long? id) => Dbase.ContainsKey(id);
    }
}