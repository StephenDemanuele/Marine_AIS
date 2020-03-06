namespace AIS.GPSReader.Models
{
    public class MessageNotImplemented : GPMessage
    {
        public string RawMessage { get; }

        public MessageNotImplemented(string message)
        {
            RawMessage = message;
        }
    }
}
