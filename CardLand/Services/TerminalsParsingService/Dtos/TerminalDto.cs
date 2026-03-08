namespace CardLand.Services.TerminalsParsingService.Dtos
{
    public class TerminalDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string FullAddress { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public bool IsPVZ { get; set; }

        public bool CashOnDelivery { get; set; }

        public bool Storage { get; set; }

        public bool IsOffice { get; set; }

        public bool ReceiveCargo { get; set; }

        public bool GiveoutCargo { get; set; }

        public string MainPhone { get; set; }

        public string Mail { get; set; }

        public double MaxWeight { get; set; }

        public double MaxLength { get; set; }

        public double MaxWidth { get; set; }

        public double MaxHeight { get; set; }

        public double MaxVolume { get; set; }

        public double MaxShippingWeight { get; set; }

        public double MaxShippingVolume { get; set; }

        public bool Default { get; set; }

        public AddressCode? AddressCode { get; set; }

        public List<PhoneDto> Phones { get; set; }
        public WorkTables Worktables { get; set; }
    }
}
