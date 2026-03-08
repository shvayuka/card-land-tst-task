namespace CardLand.Services.TerminalsParsingService.Dtos
{
    public class CityDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public int? CityID { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public TerminalContainerDto Terminals { get; set; }
    }
}
