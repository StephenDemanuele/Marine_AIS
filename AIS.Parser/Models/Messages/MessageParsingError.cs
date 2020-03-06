using System;

namespace AIS.Parser.Models.Messages
{
    public class MessageParsingError : IMessage
    {
        public string Error { get ;}

        public string StackTrace { get ;}

        public int UserId { get ; set ;} = 0;

        public MessageParsingError(string error, string stackTrace)
        {
            Error = error;
            StackTrace = stackTrace;
        }

        public override string ToString() => $"Error parsing message.{Environment.NewLine}{Error}";
    }
}
