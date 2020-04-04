using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace English_Bot 
{
    public class Users
    {
        public static int UNLearned = 10;

        public Dictionary<long, User> Dbase;
        //индексатор
        public User this[long index]
        {

            set { Dbase[index] = value; }
            get { return Dbase[index]; }
        }

        public Users()
        {
            Dbase = new Dictionary<long, User>();
            Load();
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
            foreach (var k in Dbase)
            {
                if (k.Value.userId == id)
                    return GetUser(k.Key);
            }
            return null;
        }

        public bool AddUser(User sr)
        {
            if (Dbase == null)
                Dbase = new Dictionary<long, User>();
            if (Dbase.ContainsKey(sr.userId))
                return false;
            else
            {
                //Dbase.Add(Dbase.Count + 1, sr);
                Dbase.Add(sr.userId, sr);
                Save();
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
            // JsonSerializer serializer = new JsonSerializer();
            // StreamReader sr = new StreamReader("UserData.json");
            string path = GetPathOfFile(Environment.CurrentDirectory) + "UsersData.txt";
            if (File.Exists(path))
            {
                string temp = File.ReadAllText(path);
                Dbase = JsonConvert.DeserializeObject<Dictionary<long, User>>(temp);
            }
        }

        public void Save()
        {

            string path = GetPathOfFile(Environment.CurrentDirectory) + "UsersData.txt";


            JsonSerializer serializer = new JsonSerializer();
            using (StreamWriter sw = new StreamWriter(path))
            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                serializer.Serialize(writer, Dbase);
            }


            /*  тоже рабочий варик 
             *  
            string path = Environment.CurrentDirectory+@"\UsersData.txt";
           StreamWriter sw = new StreamWriter(path,false);
                string s = JsonConvert.SerializeObject(Dbase);
            sw.WriteLine(s);
            sw.Close();
            */

        }

        public static string GetPathOfFile(string path)
        {
            return path.Substring(0, path.IndexOf("bin")); // костыль 
        }

        public bool HasUser(long id) => Dbase == null ? false : Dbase.ContainsKey(id);
    }
}