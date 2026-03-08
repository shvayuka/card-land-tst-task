namespace CardLand.Services.TerminalsParsingService.Dtos
{
    public class CityDto
    {
        public int CityID { get; set; }

        public string Name { get; set; }

        public TerminalContainerDto Terminals { get; set; }
    }
}
