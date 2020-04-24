using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using English_Bot;

namespace Crossword
{
    ///<summary>
    ///класс хранит направление, позицию,
    ///не хранит слова или индексы!
    ///</summary>
    public class Cross
    {
        public enum Direction
        {
            undefine,
            toRight,
            toDown
        }
        public Direction direction;
        public (int x, int y) pos;
        public long word_id;
        
        public Cross()
        {
            direction = Direction.undefine;
        }
        
        public Cross(Cross old)
        {
            direction = old.direction;
            pos = old.pos;
            word_id = old.word_id;
        }
        public Cross(Direction dir, (int x, int y) pos, long word_id)
        {
            direction = dir;
            this.pos = pos;
            this.word_id = word_id;
        }
        public Cross((int x, int y) pos, Direction dir, long word_id)
        {
            direction = dir;
            this.pos = pos;
            this.word_id = word_id;
        }
    }

    public class CrossMaker
    {
        ///тестовая строка
        public List<long> word_list = new List<long>() { 1, 2, 3, 4, 5 };

        ///костыль для выхода
        private bool finded = false;

        ///<summary>
        ///лист возврата, не хранит слова или индексы! порядок кросс соответствует порядку слов исходного листа слов
        ///</summary>
        public List<Cross> cross = new List<Cross>();

        ///<summary>
        ///можно использовать для сохранения пар общих букв, особой нужды не вижу, но на всякий случай вот заготовка
        ///</summary>
        // List<List<List<(int old_, int new_)>>> coupleMemory = new List<List<List<(int old_, int new_)>>>();

        public CrossMaker() { }

        ///<summary>
        ///сама рекурсивная функция
        ///</summary>
        public void CrosswordMaker()
        {
            if (finded) 
                return;
            int cur = -1;
            for (int j = 0; j < cross.Count; j++)
            {
                if (cross[j].direction == Cross.Direction.undefine)
                {
                    cur = j;
                    break;
                }
            }

            if (cur == -1) 
                finded = true; //все слова на своих местах

            bool fl = false;
            for (int j = 0; j < cross.Count; j++)
            {
                if (finded) return;
                if (cross[j].direction != Cross.Direction.undefine)
                {
                    fl = true;
                    var newDir = Cross.Direction.toDown;
                    if (newDir == cross[j].direction) newDir = Cross.Direction.toRight;
                    foreach (var couple in GetCommonLetters(EngBot.dictionary[word_list[j]].eng, EngBot.dictionary[word_list[cur]].eng))
                    {
                        if (finded) return;
                        (int x1, int y1) newPos = (0, 0);
                        if (newDir == Cross.Direction.toDown)
                        {
                            newPos.x1 = cross[j].pos.x + couple.old_;
                            newPos.y1 = cross[j].pos.y - couple.new_;
                        }
                        if (newDir == Cross.Direction.toRight)
                        {
                            newPos.y1 = cross[j].pos.y + couple.old_;
                            newPos.x1 = cross[j].pos.x - couple.new_;
                        }
                        bool happened = TryPutting(cur, newPos, newDir, j);
                        if (happened)
                        {
                            CrosswordMaker();
                            if (finded) 
                                return;
                            cross[cur] = new Cross();
                        }
                    }
                }
            }
            if (fl == false)
            {
                TryPutting(cur, pos: (0, 0), Cross.Direction.toRight);
                CrosswordMaker();
            }
        }

        //пытается вставить слово, возвращает получилось или нет
        bool TryPutting(int idx, (int x, int y) pos, Cross.Direction dir, int parentIdx = -1)
        {
            if (HasWordGotCommonPoints(idx, new Cross(pos, dir, word_list[idx]), parentIdx) == false)
            {
                cross[idx] = new Cross(pos, dir, word_list[idx]);
                return true;
            }
            else return false;
        }

        ///<summary>
        ///ищет пересечения с другими словами в кроссворде(кроме parent, то есть кроме слова, из которого идет),
        ///возвращает отсутствие пересечений или наличие(обращаю внимание на false\true)
        ///</summary>
        bool HasWordGotCommonPoints(int idx, Cross that, int parentIdx = -1)
        {
            for (int i = 0; i < cross.Count; i++)
            {
                if (i != idx && i != parentIdx)
                {
                    if (cross[i].direction != Cross.Direction.undefine)
                    {
                        if (cross[i].direction == that.direction)
                        {
                            if (cross[i].direction == Cross.Direction.toRight)
                                if (that.pos.y == cross[i].pos.y)
                                {
                                    int distFromThatToCur = cross[i].pos.x - that.pos.x;
                                    if (distFromThatToCur > 0 && distFromThatToCur - EngBot.dictionary[word_list[idx]].eng.Length > 0) continue;
                                    if (distFromThatToCur < 0 && distFromThatToCur + EngBot.dictionary[word_list[i]].eng.Length < 0) continue;
                                }
                                else continue;


                            if (cross[i].direction == Cross.Direction.toDown)
                                if (that.pos.x == cross[i].pos.x)
                                {
                                    int distFromThatToCur = cross[i].pos.y - that.pos.y;
                                    if (distFromThatToCur > 0 && distFromThatToCur - EngBot.dictionary[word_list[idx]].eng.Length > 0) continue;
                                    if (distFromThatToCur < 0 && distFromThatToCur + EngBot.dictionary[word_list[i]].eng.Length < 0) continue;
                                }
                                else continue;

                            return true;
                        }
                        else
                        {
                            Cross down, right;
                            int dIdx, rIdx;
                            if (that.direction == Cross.Direction.toDown)
                            {
                                down = new Cross(that);
                                right = new Cross(cross[i]);
                                dIdx = idx;
                                rIdx = i;
                            }
                            else
                            {
                                down = new Cross(cross[i]);
                                right = new Cross(that);
                                dIdx = i;
                                rIdx = idx;
                            }

                            if (down.pos.y > right.pos.y + 1 || down.pos.y + EngBot.dictionary[word_list[dIdx]].eng.Length < right.pos.y) continue;
                            if (right.pos.x > down.pos.x + 1 || right.pos.x + EngBot.dictionary[word_list[rIdx]].eng.Length < down.pos.x) continue;
                            return true;
                        }
                    }
                }

            }
            return false;//заглушка
        }

        ///<summary>
        ///ищет пары общих букв, выдает лист пар индексов
        ///сделано обычным перебором O(n*m)
        ///</summary>
        public List<(int old_, int new_)> GetCommonLetters(string a, string b)
        {
            List<(int, int)> res = new List<(int, int)>(a.Length * 2);
            for (int j = 0; j < a.Length; j++)
            {
                for (int i = 0; i < b.Length; i++)
                {
                    if (a[j] == b[i]) res.Add((j, i));
                }
            }
            return res;

        }
    }

    //Простая версия кроссворда
    public class SimpleCross
    {
        public long id { get; set; }
        public bool is_all_answered {get; set;}
        private int up_height { get; set; }
        private int down_height { get; set; }
        private int border { get; set; }
        private int block_sz { get; set; }
        private Random rand = new Random();
        //Слово на английском, которое открывается при решении, его id
        private (string, long) main_word { get; set; }
        //Слова, пересекающиеся с основным, их id, и индекс буквы, которая пересекается
        public List<(string,long,int)> words { get; set; }
        //Список русских значений слов с указанием части речи
        public List<string> legend { get; set; }
        //Список отвеченных слов
        public List<bool> is_answered {get;set;}

        public (string,long) MainWord => main_word;

        //id пользователя, границы размера основного слова, максимальная высота поля
        public SimpleCross(long id, int min_sz = 4, int max_sz = 10, int area_height = 11)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();

            block_sz = 100;
            down_height = area_height / 2;
            up_height = area_height / 2;
            border = block_sz;
            this.id = id;
            
            var user = EngBot.users[id];

            //делаем списки выученных/невыученных слов
            List<long> learned = new List<long>();
            List<long> unlearned = new List<long>();
            foreach (var x in user.learnedWords)
                learned.Add(x);
            foreach (var x in user.unLearnedWords)
                unlearned.Add(x);
            randomize_list(learned);
            randomize_list(unlearned);

            if (!init_main_word(learned, min_sz, max_sz)) //заполнение главного слова
                if (!init_main_word(unlearned, min_sz, max_sz))
                    init_main_word(EngBot.dictionary.GetKeysByLevel_hash(user.userLevel),min_sz,max_sz);

            words = new List<(string, long, int)>(main_word.Item1.Length);
            is_answered = new List<bool>(main_word.Item1.Length);
            legend = new List<string>(main_word.Item1.Length);
            for (int i = 0; i < main_word.Item1.Length; i++)
            {
                words.Add(("", -1, -1));
                is_answered.Add(false);
                legend.Add("");            
            }

            if (!init_words(learned, area_height)) //заполнение главного слова
                if (!init_words(unlearned, area_height))
                    init_words(EngBot.dictionary.GetKeysByLevel_hash(user.userLevel), area_height);

            correct_height();
            try
            {
                init_legend();
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("init_legend null ref!");
            }
            stp.Stop();
            Console.WriteLine("Elapsed for creating Crossword: " + stp.ElapsedMilliseconds);

            DrawPicture();
            //for (int i = 0; i < words.Count; i++)
            //    DrawWord(i);
        }

        private void correct_height()
        {
            int max_up = 0, max_down = 0;
            foreach (var x in words)
            {
                if (x.Item3 > max_up)
                    max_up = x.Item3;
                if (x.Item1.Length - 1 - x.Item3 > max_down)
                    max_down = x.Item1.Length - 1 - x.Item3;
            }
            up_height = max_up + 1;
            down_height = max_down + 1;
        }

        private void init_legend()
        {
            for (int i=0;i<words.Count;i++)
            {
                string w = EngBot.dictionary[words[i].Item2].rus;
                legend[i] = (i+1).ToString() +") "+ w + "\n";
            }
        }

        private void randomize_list(List<long> lst)
        {
            for (int i=0;i<lst.Count;i++)
            {
                long x = lst[i];
                int j = rand.Next(0, lst.Count-1);
                lst[i] = lst[j];
                lst[j] = x;
            }

        }

        private bool init_words(List<long> w, int area_height)
        {
            int half_area = area_height / 2;
            bool is_inited = false;
            foreach (var x in w)
            {
                string cur_word = EngBot.dictionary[x].eng;
                if (cur_word == main_word.Item1)
                    continue;
                if (cur_word.Length > area_height)
                    continue;
                if (EngBot.dictionary[x].tags.Contains("rus_only") || EngBot.dictionary[x].tags.Contains("eng_only"))
                    continue;
                int front_end = rand.Next(0, 1); //спереди или сзади
                for (int j = 0; j < words.Count; j++)
                {
                    if (words[j].Item3 != -1)
                        continue;
                    bool f = false;
                    int i = front_end == 0 ? 0 : cur_word.Length - 1;
                    while (i >= 0 && i < cur_word.Length)
                    {
                        if (cur_word[i] != main_word.Item1[j] || i >= half_area || cur_word.Length - i > half_area)
                        {
                            i = front_end == 0 ? i + 1 : i - 1;
                            continue;
                        }
                        words[j] = (cur_word, x, i);
                        f = true;
                        break;
                    }
                    if (f)
                        break;
                }
                if (is_words_inited())
                    break;
            }
            if (!is_words_inited())
                return false;
            return true;
        }

        private bool init_words(HashSet<long> w, int area_height)
        {
            int half_area = area_height / 2;
            int k = 0;
            while (true)    //подбираем слова
            {
                if (k > 1000)
                    break;
                k++;
                var x = w.GetEnumerator();
                for (int i = 0; i < rand.Next(1, w.Count); i++)
                    x.MoveNext();

                foreach (var y in words)    //убираем повторяющееся слово
                    if (y.Item2 == x.Current)
                        continue;

                string cur_word = EngBot.dictionary[x.Current].eng;
                if (cur_word == main_word.Item1)
                    continue;
                if (cur_word.Length > area_height)
                    continue;
                if (EngBot.dictionary[x.Current].tags.Contains("rus_only") || EngBot.dictionary[x.Current].tags.Contains("eng_only"))
                    continue;
                int front_end = rand.Next(0, 1); //спереди или сзади
                for (int j = 0; j < words.Count; j++)
                {
                    if (words[j].Item3 != -1)
                        continue;
                    bool f = false;
                    int i = front_end == 0 ? 0 : cur_word.Length-1;
                    while (i >= 0 && i < cur_word.Length)
                    {
                        if (cur_word[i] != main_word.Item1[j] || i >= half_area || cur_word.Length - i > half_area)
                        {
                            i = front_end == 0 ? i + 1 : i - 1;
                            continue;
                        }
                        words[j] = (cur_word, x.Current, i);
                        f = true;
                        break;
                    }
                    if (f)
                        break;
                }
                if (is_words_inited())
                    break;
            }
            if (k > 1000)
                return false;
            return true;
        }

        private bool is_words_inited()
        {
            foreach (var x in words)
                if (x.Item3 == -1)
                    return false;
            return true;
        }

        private bool init_main_word(List<long> w, int min_sz, int max_sz)
        {
            bool f = false;
            foreach (var x in w)   //подбираем главное слово
            {
                string cur_word = EngBot.dictionary[x].eng;
                if (cur_word.Length < min_sz || cur_word.Length > max_sz)
                    continue;
                if (EngBot.dictionary[x].tags.Contains("rus_only") || EngBot.dictionary[x].tags.Contains("eng_only"))
                    continue;                
                main_word = (cur_word, x);
                f = true;
                break;
            }
            return f;
        }

        private bool init_main_word(HashSet<long> w, int min_sz, int max_sz)
        {
            int k = 0;
            while (true)    //подбираем главное слово
            {
                if (k > 1000)
                    break;
                k++;
                var x = w.GetEnumerator();
                for (int i = 0; i < rand.Next(1, w.Count - 1); i++)
                    x.MoveNext();
                string cur_word = EngBot.dictionary[x.Current].eng;
                if (cur_word.Length < min_sz || cur_word.Length > max_sz)
                    continue;
                if (EngBot.dictionary[x.Current].tags.Contains("rus_only") || EngBot.dictionary[x.Current].tags.Contains("eng_only"))
                    continue;
                main_word = (cur_word, x.Current);
                break;
            }
            if (k > 1000)
                return false;
            return true;
        }

        //Рисует указанное слово
        public void DrawWord(int n)
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();

            if (n < 0 || n >= words.Count)
                throw new IndexOutOfRangeException();

            if (n < 0 || n >= words.Count)
                throw new IndexOutOfRangeException();

            is_answered[n] = true;
            is_all_answered = true;
            foreach (var b in is_answered)
                if (!b)
                    is_all_answered = b;

            Bitmap bmp = new Bitmap(@"users\" + id + @"\cross.jpg");
            Graphics g = Graphics.FromImage(bmp);

            int y = up_height * block_sz - words[n].Item3 * block_sz;
            int x = border + n * block_sz;
            Font f = new Font("Comic Sans Ms", 150);
            Brush bb = Brushes.Black;


            foreach (var letter in words[n].Item1)
            {
                g.DrawString(letter.ToString(), f, bb, x + 20, y - 10);
                y += block_sz;
            }

            stp.Stop();
            Console.WriteLine("Elapsed for drawing word: "+stp.ElapsedMilliseconds);

            stp.Reset();
            stp.Start();

            bmp.Save(@"users\" + id + @"\cross_temp.jpg", ImageFormat.Jpeg);
            g.Dispose();
            bmp.Dispose();

            File.Delete(@"users\" + id + @"\cross.jpg");
            File.Move(@"users\" + id + @"\cross_temp.jpg", @"users\" + id + @"\cross.jpg");

            stp.Stop();
            Console.WriteLine("Elapsed for saving drawn word: " + stp.ElapsedMilliseconds);
        }

        //Рисует все ненарисованные слова
        public void DrawWords()
        {
            Bitmap bmp = new Bitmap(@"users\" + id + @"\cross.jpg");
            Graphics g = Graphics.FromImage(bmp);

            Font f = new Font("Comic Sans Ms", 150);
            Brush bb = Brushes.Black;

            for (int n = 0; n < is_answered.Count; n++)
            {
                if (is_all_answered)
                    break;
                if (is_answered[n])
                    continue;

                is_answered[n] = true;
                is_all_answered = true;
                foreach (var b in is_answered)
                    if (!b)
                        is_all_answered = b;

                int y = up_height * block_sz - words[n].Item3 * block_sz;
                int x = border + n * block_sz;

                foreach (var letter in words[n].Item1)
                {
                    g.DrawString(letter.ToString(), f, bb, x + 20, y-10);
                    y += block_sz;
                }
            }


            bmp.Save(@"users\" + id + @"\cross_temp.jpg", ImageFormat.Jpeg);
            g.Dispose();
            bmp.Dispose();

            File.Delete(@"users\" + id + @"\cross.jpg");
            File.Move(@"users\" + id + @"\cross_temp.jpg", @"users\" + id + @"\cross.jpg");
        }

        //Рисует пустой кроссворд
        private void DrawPicture()
        {
            Stopwatch stp = new Stopwatch();
            stp.Start();

            int height = (up_height + down_height + 1) * block_sz;
            int width = words.Count * block_sz + block_sz*2;
            if (width < height - (block_sz+ block_sz/2))
            {
                width = height - (block_sz + block_sz / 2);
                border = (width - words.Count * block_sz) / 2;
            }

            int resolution = block_sz/3;
            
            Bitmap bmp = new Bitmap(width, height);
            bmp.SetResolution(resolution, resolution);
            Graphics g = Graphics.FromImage(bmp);

            Pen p = new Pen(Color.Black, block_sz/20);
            Font f = new Font("Impact", 150);
            Brush bb = Brushes.Black;

            g.FillRectangle(Brushes.White, 0, 0, width, height);
            //g.DrawRectangle(new Pen(Color.Black,20.0f), 0, 0, width, height);

            {
                int x = border; int y = up_height * block_sz;
                for (int i = 0; i < words.Count; i++)
                {
                    g.FillRectangle(Brushes.Yellow, x, y, block_sz, block_sz);
                    x += block_sz;
                }
            }
            for (int i = 0; i<words.Count;i++)
            {
                int x = border + i* block_sz; int y = up_height* block_sz - words[i].Item3* block_sz - block_sz;
                g.DrawString((i + 1).ToString(), f, bb, x+20, y);
                y += block_sz;
                for (int j = 0; j < words[i].Item1.Length; j++)
                {
                    g.DrawRectangle(p, x, y, block_sz, block_sz);
                    y += block_sz;
                }
            }

            stp.Stop();
            Console.WriteLine("Elapsed for drawing Crossword: " + stp.ElapsedMilliseconds);

            stp.Reset();
            stp.Start();

            if (!Directory.Exists("users"))
                Directory.CreateDirectory("users");
            if (!Directory.Exists(@"users\"+id.ToString()))
                Directory.CreateDirectory(@"users\" + id.ToString());
            bmp.Save(@"users\"+id+@"\cross.jpg",ImageFormat.Jpeg);
            bmp.Dispose();

            stp.Stop();
            Console.WriteLine("Elapsed for saving Crossword: " + stp.ElapsedMilliseconds);
        }
    }
}
