using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace English_Bot 
{
    public class Users
    {
        public const int UNLearned = 10;

        public enum Mode { Easy = 1, Medium = 3, Hard = 4 };

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
            try
            {
                string path = GetPathOfFile(Environment.CurrentDirectory) + "UsersData.txt";
                if (File.Exists(path))
                {
                    string temp = File.ReadAllText(path);
                    Dbase = JsonConvert.DeserializeObject<Dictionary<long, User>>(temp);
                    foreach(var user in Dbase.Values)
                    {
                        if (user.on_Test)
                            user.on_Test = false; 
                    }
                }

                if (Dbase != null)
                {
                    foreach (var key in Dbase.Keys)
                    {
                        Dbase[key].on_Test = false;
                        Dbase[key].keyb = User.Main_Keyboard;
                        if (Dbase[key].tests_passed == 0)
                            DeleteUser(key);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Load problem!!!   " + e.Message);
            }
        }

        public void Save()
        {

            try
            {
                string path = GetPathOfFile(Environment.CurrentDirectory) + "UsersData.txt";
                JsonSerializer serializer = new JsonSerializer();
                using (StreamWriter sw = new StreamWriter(path))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, Dbase);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Save problem!!!   " + e.Message);
            }
        }

        public static string GetPathOfFile(string path)
        {
            return path.Substring(0, path.IndexOf("bin")); // костыль 
        }

        public bool HasUser(long id) => Dbase == null ? false : Dbase.ContainsKey(id);

        public int Place_in_rating(long id)
        {
            var user = Dbase[id];
            if (user.tests_passed == 0)
                return -1;
            double rating = user.learnedWords.Count / user.tests_passed;
            int place = 1;
            foreach (var us in Dbase.Values)
            {
                if (us.tests_passed == 0)
                    continue;
                double r = us.learnedWords.Count / us.tests_passed;
                if (r > rating)
                    place++;
            }
            return place;
        }
    }
}