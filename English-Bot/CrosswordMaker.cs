using System;
using System.Collections.Generic;
using System.Text;
using English_Bot.Properties;
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
        public static List<long> word_list = new List<long>() { 1, 2, 3, 4, 5 };

        ///костыль для выхода
        static bool finded = false;

        ///<summary>
        ///лист возврата, не хранит слова или индексы! порядок кросс соответствует порядку слов исходного листа слов
        ///</summary>
        public static List<Cross> cross = new List<Cross>();

        ///<summary>
        ///можно использовать для сохранения пар общих букв, особой нужды не вижу, но на всякий случай вот заготовка
        ///</summary>
        // List<List<List<(int old_, int new_)>>> coupleMemory = new List<List<List<(int old_, int new_)>>>();

        ///<summary>
        ///сама рекурсивная функция
        ///</summary>
        public static void CrosswordMaker()
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
        static bool TryPutting(int idx, (int x, int y) pos, Cross.Direction dir, int parentIdx = -1)
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
        static bool HasWordGotCommonPoints(int idx, Cross that, int parentIdx = -1)
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
        static List<(int old_, int new_)> GetCommonLetters(string a, string b)
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
}
