namespace AIS.WebApi.DTO
{
    public class VesselHeaderDto
    {
        public VesselHeaderDto(int mmsi, string name, Talker talker)
        {
            MMSI = mmsi;
            Name = name;
            Talker = talker;
        }

        public int MMSI { get; set; }

        public string Name { get; private set; }

        public Talker Talker { get; private set; }
    }
}
