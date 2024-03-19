using System;

namespace ChatOFGAPI
{
    public struct Message
    {
        public string text;
        public MessageType messageType;

        public Message(string text)
        {
            if (Enum.TryParse(text.Split(':')[0], out messageType))
                this.text = text.Substring(text.IndexOf(':') + 1);
            else
                this.text = text;
        }

        public Message(string text, MessageType messageType)
        {
            this.text = text;
            this.messageType = messageType;
        }

        public static implicit operator Message(string text)
        {
            return new Message(text);
        }

        public static implicit operator string(Message message)
        {
            return message.ToString();
        }

        public static implicit operator MessageType(Message message)
        {
            return message.messageType;
        }

        public override string ToString()
        {
            return messageType + ":" + text;
        }
    }
}
