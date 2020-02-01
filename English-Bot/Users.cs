using System;
using System.Collections.Generic;

namespace English_Bot 
{
    public class Users
    {
       private  Dictionary<long,User>  Dbase;
        //индексатор
        public User this[long idex]
        {

            set { Dbase[idex] = value; }
            get { return Dbase[idex]; }
        }

        public Users()
        {
            Dbase = new Dictionary<long, User>(); 
        }


       public User GetUser(long id)
        {
            if (Dbase.ContainsKey(id))
                return Dbase[id];
            else
                return null;
        }

        public User GetUserVKID(long id)
        {
            foreach(var k in Dbase)
            {
                if (k.Value.userId == id)
                    return GetUser(k.Key);
            }
            return null;
        }

      public  bool AddUser(User sr)
        {
            if (Dbase.ContainsValue(sr))
                return false;
            else
            {
                //Dbase.Add(Dbase.Count + 1, sr);
                Dbase.Add(sr.userId, sr);
                return true;
            }
        }

        public bool DeleteUser(long id)
        {
            if (!Dbase.ContainsKey(id))
                return false;
            else
            {
                Dbase.Remove(id);
                return true;
            }
        }

        public void Load()
        {

        }
        
        public void Save()
        {

        }

    }
}