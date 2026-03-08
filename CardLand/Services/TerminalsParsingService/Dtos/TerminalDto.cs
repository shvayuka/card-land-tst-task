namespace CardLand.Services.TerminalsParsingService.Dtos
{
    public class TerminalDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string MainPhone { get; set; }

        public List<PhoneDto>? Phones { get; set; }
    }
}
