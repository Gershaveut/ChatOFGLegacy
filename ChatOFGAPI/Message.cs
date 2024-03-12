using System;

namespace ChatOFGAPI
{
    public struct Message
    {
        public string text;
        public MessageType messageType;

        public Message(string text)
        {
            this.text = text.Substring(text.IndexOf(':') + 1);

            Enum.TryParse(text.Split(':')[0], out messageType);
        }

        public static implicit operator Message(string text)
        {
            return new Message(text);
        }

        public static implicit operator string(Message message)
        {
            return message.text;
        }

        public static implicit operator MessageType(Message message)
        {
            return message.messageType;
        }
    }
}
