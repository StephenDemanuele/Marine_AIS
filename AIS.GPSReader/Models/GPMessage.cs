namespace AIS.GPSReader.Models
{
    public abstract class GPMessage
    {
        public string MessageId { get; protected set; }

        public string Checksum { get; protected set; }
    }
}
