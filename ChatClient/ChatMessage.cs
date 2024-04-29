using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TChatClient
{
    internal class ChatMessage
    {
        public int Id { get; set; }
        public string? FromName { get; set; }
        public string? ToName { get; set; }
        public string? Text { get; set; }
        public Command Command { get; set; }



        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static ChatMessage FromJson(string str)
        {
            return JsonSerializer.Deserialize<ChatMessage>(str)!;
        }
    }
}
