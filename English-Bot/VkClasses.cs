using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json;
using VkNet.Model.Keyboard;
using VkNet.Enums.SafetyEnums;

namespace VkApi
{
    public class Keyboard
    {
        public bool one_time { get; set; }
        public Button[][] buttons { get; set; }
        public bool inline { get; set; }

        public bool Is_Empty => buttons == null ? true : false;
        public Keyboard(Button[][] buttons = null, bool one_time = true, bool inline = false)
        {
            this.buttons = buttons;
            this.one_time = one_time;
            this.inline = inline;
        }

        public Keyboard(Button[] buttons, bool one_time = true, bool inline = false)
        {
            this.buttons = new Button[][] { buttons };
            this.one_time = one_time;
            this.inline = inline;
        }

        public Keyboard()
        {
            this.buttons = null;
            this.one_time = true;
            this.inline = false;
        }

        public MessageKeyboard ToMessageKeyboard()
        {
            if (buttons == null)
                return null;
            MessageKeyboard kb = new MessageKeyboard();
            kb.OneTime = one_time;
            MessageKeyboardButton[][] buts = new MessageKeyboardButton[buttons.Length][];
            for (int i=0;i<buttons.Length;i++)
            {
                buts[i] = new MessageKeyboardButton[buttons[i].Length];
                for (int j = 0; j < buttons[i].Length; j++)
                {
                    buts[i][j] = new MessageKeyboardButton();

                    buts[i][j].Action = new MessageKeyboardButtonAction();
                    buts[i][j].Action.Label = buttons[i][j].action.label;
                    buts[i][j].Action.Payload = buttons[i][j].action.payload;
                    buts[i][j].Action.Type = KeyboardButtonActionType.Text;

                    switch (buttons[i][j].color)
                    {
                        case "primary":
                            buts[i][j].Color = KeyboardButtonColor.Primary;
                            break;
                        case "negative":
                            buts[i][j].Color = KeyboardButtonColor.Negative;
                            break;
                        case "positive":
                            buts[i][j].Color = KeyboardButtonColor.Positive;
                            break;
                        default:
                            buts[i][j].Color = KeyboardButtonColor.Default;
                            break;
                    }
                }
            }

            kb.Buttons = buts;
            return kb;
        }
    }

    public class Button
    {
        public Button_obj action { get; set; }
        public string color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action">Color only for text action</param>
        /// <param name="color">primary - blue, secondary - white, negative - red, positive - green </param>
        public Button(Button_obj action, string color= "secondary")
        {
            this.action = action;
            this.color = color;
        }

        public Button(string text, string color = "secondary")
        {
            action = new Button_obj(text);
            this.color = color;
        }

        public Button()
        {
            action = null;
            color = null;
        }
    }

    public class Button_obj
    {
        public string type { get; set; }
        public string label { get; set; }
        public string link { get; set; }
        public string payload { get; set; }

        public Button_obj()
        {
            type = null;
            label = null;
            link = null;
            payload = null;
        }

        public Button_obj(string label, string type="text", string link="", string payload="")
        {
            if (type != "text" && type != "open_link")
                throw new ButtonTypeException();
            this.type = type;
            this.label = label;
            this.link = link;
            this.payload = payload;
        }
    }

    public class ButtonTypeException : Exception
    {
        public override string Message => "Only reserved \"text\"  type of buttons";
    }

    public class Size
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string type { get; set; }
    }

    [DataContract]
    public class VkAlbum
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int size { get; set; }
    }

    [DataContract]
    public class VkPicture
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int album_id { get; set; }

        [DataMember]
        public int owner_id { get; set; }
        [DataMember]
        public int user_id { get; set; }
        [DataMember]
        public string text { get; set; }
        [DataMember]
        public int date { get; set; }
        [DataMember]
        public Size[] sizes { get; set; }
        [DataMember]
        public int width { get; set; }
        [DataMember]
        public int height { get; set; }
    }


    [DataContract]
    public class VkUsers
    {
        [DataMember]
        public VkUser[] response { get; set; }
    }
    /// <summary>
    /// Профиль вк
    /// </summary>
    [DataContract]
    public class VkUser
    {
        [DataMember]
        // ID
        public int id { get; set; }

        [DataMember]
        // Имя
        public string first_name { get; set; }

        [DataMember]
        // Фамилия 
        public string last_name { get; set; }

        [DataMember]
        // deleted или banned  
        public string deactivated { get; set; }

        [DataMember]
        // Скрыт ли профиль 
        public bool is_closed { get; set; }

        [DataMember]
        // можем ли мы видеть профиль 
        public bool can_access_closed { get; set; }

        [DataMember]
        // Интересы 
        public string interests { get; set; }

        [DataMember]
        // Любимая музыка 
        public string music { get; set; }

        [DataMember]
        // Любимые фильмы 
        public string movies { get; set; }

        [DataMember]
        // Любимые цитаты 
        public string quotes { get; set; }

        [DataMember]
        // Статус 
        public string status { get; set; }

        [DataMember]
        // Любимые игры 
        public string games { get; set; }

        [DataMember]
        // Любимые книги 
        public string books { get; set; }
    }
}
