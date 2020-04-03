using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing; 

namespace English_Bot
{
    public class Hit
    {
        public int id { get; set; }
        public string pageURL { get; set; }
        public string type { get; set; }
        public string tags { get; set; }
        public string previewURL { get; set; }
        public int previewWidth { get; set; }
        public int previewHeight { get; set; }
        public string webformatURL { get; set; }
        public int webformatWidth { get; set; }
        public int webformatHeight { get; set; }
        public string largeImageURL { get; set; }
        public int imageWidth { get; set; }
        public int imageHeight { get; set; }
        public int imageSize { get; set; }
        public int views { get; set; }
        public int downloads { get; set; }
        public int favorites { get; set; }
        public int likes { get; set; }
        public int comments { get; set; }
        public int user_id { get; set; }
        public string user { get; set; }
        public string userImageURL { get; set; }
    }

    public class Pictures
    {
        public int total { get; set; }
        public int totalHits { get; set; }
        public List<Hit> hits { get; set; }
    }

    public class VkPhotoInfo
    {
        public int server { get; set; }
        public string photos_list { get; set; }
        public int aid { get; set; }
        public string hash { get; set; }
    }

    public class VKRootObject
    {
        public int server { get; set; }
        public string photo { get; set; }
        public string hash { get; set; }
    }

    public class VkVoice
    {
        public string file { get; set; }
    }

    // ----------------------------- 

    public class AudioMsg
    {
        public int duration { get; set; }
        public List<int> waveform { get; set; }
        public string link_ogg { get; set; }
        public string link_mp3 { get; set; }
    }

    public class Preview
    {
        public AudioMsg audio_msg { get; set; }
    }

    public class Response
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public string url { get; set; }
        public int date { get; set; }
        public int type { get; set; }
        public Preview preview { get; set; }
    }

    public class RootObject
    {
        public List<Response> response { get; set; }
    }
}
