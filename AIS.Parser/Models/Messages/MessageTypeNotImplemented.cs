namespace AIS.Parser.Models.Messages
{
    public class MessageTypeNotImplemented :  IMessage
    {
        public MessageTypeNotImplemented(string payload)
        {
            Payload = payload;
        }

        public string Payload { get; }

        public int UserId { get ;set;} = 0;

        public override string ToString() => $"Not implemmented: {Payload}";
    }
}
