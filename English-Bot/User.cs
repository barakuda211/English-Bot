using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using VkApi;

namespace English_Bot 
{
    public class User
    {
        public long userId { get; set; }
        //Уровень пользователя (1,2,3,4,5,-1)
        public int userLevel { get; set; }
        //для корректной регистрации
        public int regId { get; set; }
        //Имя юзера на всякий случай
        public string name { get; set; }
        public HashSet<string> userTags { get; set; }
        /// <summary>
        /// индексы слов , введенных юзером ,которые он запомнил
        /// </summary>
        public HashSet<long> learnedWords { get; set; }
        /// <summary>
        /// индексы слов ,введенных юзером ,которые он не запомнил  
        /// </summary>
        public HashSet<long> unLearnedWords { get; set; }
        /// <summary>
        /// последнее слово(текст), написанное пользователем
        /// bool избавляет от повторного считывания слова(текста)
        /// 3-ий параметр - это индификатор сообщения
        /// </summary>
        public (string, bool, long) lastMsg;
        /// <summary>
        /// тестируется ли в данный момент юзер (не только тестируется))))))
        /// </summary>
        public bool on_Test { get; set; }
        //Для смены уровня пользователя 
        public int ch_lvl_id { get; set; }
        /// <summary>
        /// Сколько слов пользователь выучил за неделю
        /// </summary>
        public int week_words { get; set; }
        /// <summary>
        /// Сложность 
        /// </summary>
        public Users.Mode mode { get; set; }
        /// <summary>
        /// Клавиатура, активная в текущий момент
        /// </summary>
        public Keyboard keyb { get; set; }
        /// <summary>
        /// Если true, то боте не будет присылать слова и тесты
        /// </summary>
        public bool bot_muted { get; set; }
        /// <summary>
        /// Слова в день на изучение
        /// </summary>
        public int day_words { get; set; }
        /// <summary>
        /// Кол-во пройденных тестов
        /// </summary>
        public int tests_passed = 0;
        /// <summary>
        /// Кол-во пройденных кроссвордов
        /// </summary>
        public int cross_passed = 0;
        /// <summary>
        /// Кол-во пройденных виселиц
        /// </summary>
        public int gall_passed = 0;

        public List<long> words_to_learn { get; set; }

        public static Keyboard Ready_Keyboard = new Keyboard(new Button[] { new Button("Готов", "primary") }, true);
        public static Keyboard ReadyOrNot_Keyboard = new Keyboard(new Button[] { new Button("Готов", "positive"), new Button("Не готов", "negative") }, true);
        public static Keyboard Main_Keyboard = new Keyboard(new Button[][] {
            new Button[] { new Button("Команды бота") },
            new Button[] { new Button("Игра кроссворд"), new Button("Игра виселица") },
            new Button[] { new Button("Повторить слова") } }, false);
        public static Keyboard Help_Keyboard = new Keyboard(new Button[][] {
            new Button[] { new Button("Моя статистика"), new Button("Добавить слова")},
            new Button[] { new Button("Сменить уровень"), new Button("Сменить сложность") },
            new Button[] { new Button("Хватит меня учить"), new Button("Нет, учи меня") },
            new Button[] { new Button("Вернуться назад", "negative") }}, false);
        public static Keyboard ChangingLevel_Keyboard = new Keyboard(new Button[][]{
            new Button[] { new Button("1"),new Button("2"), new Button("3")},
            new Button[] { new Button("4"), new Button("5"),new Button("-1") }}, false);
        public static Keyboard Crossword1_Keyboard = new Keyboard(new Button[] { new Button("Подсказать слово", "positive"), new Button("Я сдаюсь", "negative") }, false);
        public static Keyboard Crossword2_Keyboard = new Keyboard(new Button[] {  new Button("Я сдаюсь", "negative") }, false);
        public static Keyboard Gallows_KeyBoard = new Keyboard(new Button[] { new Button("Подсказать букву", "positive"), new Button("Я сдаюсь", "negative") }, false);
        // public static Keyboard Gallows_KeyBoard2 = new Keyboard(new Button[] { new Button("Я сдаюсь", "negative")}, false);
        public static Keyboard Complexity_Keyboard = new Keyboard(new Button[] {new Button("Лёгкий","positive"), new Button("Сложный","negative")},false);
        public static Keyboard Back_Keyboard = new Keyboard(new Button[] { new Button("Вернуться назад", "negative") });

        public User(VkUser vk_user, WordsDictionary dict)
        {
            regId = 0;
            userLevel = 0;
            userId = vk_user.id;
            name = vk_user.first_name;
            learnedWords = new HashSet<long>();
            unLearnedWords = new HashSet<long>();
            Random r = new Random(); 
            for (int i = 1; i <= 5; ++i)
            {
                var list = dict.GetKeysByLevel(i).Where(x => EngBot.dictionary[x].mean_rus != null && EngBot.dictionary[x].tags != null && !EngBot.dictionary[x].tags.Contains("added")).ToList();
                for (int j = 1; j <= 3; ++j)
                {
                    unLearnedWords.Add(list.ElementAt(r.Next(list.Count)));
                }
            }
            unLearnedWords = unLearnedWords.Distinct().ToHashSet();
            //parseWordsFields(vk_user, dict);
            on_Test = false;
            ch_lvl_id = 0;
            week_words = 0;
            day_words = 10;
            // vk_User = vk_user;
            lastMsg = ("", false, 0);
            mode = Users.Mode.Hard;
            keyb = Ready_Keyboard;
            bot_muted = false;
            userLevel = 1;
            words_to_learn = new List<long>();
        }

        private void parseWordsFields(VkUser vk_user, WordsDictionary dict)
        {
            var fields = string.Join(' ', vk_user.interests, vk_user.music, vk_user.movies, vk_user.quotes, vk_user.status, vk_user.games, vk_user.books).ToLower();
            var newFields = Regex.Split(fields, @"\b\W+\b").Where(x => x.Length > 1).Distinct();
            foreach (var x in newFields)
            {
                foreach (var y in dict.GetEngId(x))
                    unLearnedWords.Add(y);
                foreach (var y in dict.GetRusId(x))
                    unLearnedWords.Add(y);
            }
            foreach (var x in unLearnedWords)
            {
                Console.WriteLine(dict[x]);
            }
            if (unLearnedWords.Count == 0)
            {
                unLearnedWords = new HashSet<long>(dict.eng_ids.Values.Where(x => Math.Sin(Math.Sqrt(x)) > 0).Take(10));
            }
            
        }
        public User(long Userid, int rgId, string nm, int Uslev, HashSet<string> UsTags, HashSet<long> learWrds, HashSet<long> UnlearWrds)
        {
            userId = Userid;
            regId = rgId;
            name = nm;
            userLevel = Uslev;
            userTags = UsTags;
            learnedWords = learWrds;
            unLearnedWords = UnlearWrds;
            lastMsg = ("", true, 0);
            on_Test = false;
            ch_lvl_id = 0;
            mode = Users.Mode.Hard; 
            keyb = Ready_Keyboard;
            bot_muted = false;
            week_words = 0;
            day_words = 10;
            words_to_learn = new List<long>();
        }

        public User() 
        {
            mode = Users.Mode.Easy;
            words_to_learn = new List<long>();
            userLevel = 1;
            day_words = 10;
        }

        public override string ToString()
        {
            return "your level " + userLevel.ToString();
        }

        //Смена уровня пользователя
        public bool ChangeLevel(int level)
        {
            if ((level>0 && level<6)|| level == -1 )
            {
                userLevel = level;
                return true;
            }
            return false;
        }

        public bool AddTags(string[] s)
        {
            bool TagIsAdded = false;

            foreach (var t in s)
            { 
                    if (!userTags.Contains(t))
                    {
                        TagIsAdded = true;
                        userTags.Add(t);
                    }
            }
           
            return TagIsAdded;
        }

        public bool DeleteTags(string[] s)
        {
            bool TagIsDeleted = false;

            foreach (var t in s)
            {
                if (!userTags.Contains(t))
                {
                    TagIsDeleted = true;
                    userTags.Remove(t);
                }
            }

            return TagIsDeleted;
        }

        public (string[],int) AddWords(string text)
        {
            string[] words = text.Split(new char[] { ',',' ' },StringSplitOptions.RemoveEmptyEntries);
            int added = 0;
            List<string> error_words = new List<string>();

            if (words_to_learn == null)
                words_to_learn = new List<long>();

            foreach (var x in words)
            {
                if (EngBot.dictionary.eng_ids.ContainsKey(x))
                {
                    words_to_learn.Add(EngBot.dictionary.GetEngWordId(x));
                    added++;
                }
                else
                    error_words.Add(x);
            }
            return (error_words.ToArray(),added);
        }

    }
}

