using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

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
            JsonSerializer serializer = new JsonSerializer();
            using (StreamReader sr = new StreamReader("usersJson.txt"))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                Dbase = (Dictionary<long, User>)serializer.Deserialize(reader);
            }
        }
        
        public async void Save()
        {
            
            JsonSerializer serializer = new JsonSerializer();
            using(StreamWriter sw = new StreamWriter("usersJson.txt"))
            using(JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, Dbase);
            }
        }

    }
}