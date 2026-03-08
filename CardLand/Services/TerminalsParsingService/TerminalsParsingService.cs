using CardLand.Migrations;
using CardLand.Models;
using CardLand.Services.TerminalsParsingService.Dtos;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CardLand.Services.TerminalsParsingService
{
    public static class TerminalsParsingService
    {
        public static async Task<IEnumerable<Office>> Parse()
        {
            var data = await ParseJson();

            return MapData(data);
        }

        private static async Task<TerminalDictionaryDto> ParseJson()
        {
            var json = await File.ReadAllTextAsync(Path.Combine(AppContext.BaseDirectory, Consts.TERMINALS_FILE));

            var data = JsonSerializer.Deserialize<TerminalDictionaryDto>(
                json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            if (data == null)
                throw new Exception("Error during json deserialization");

            return data;
        }

        private static IEnumerable<Office> MapData(TerminalDictionaryDto data)
        {
            List<Office> offices = new List<Office>();
            foreach(var city in data.City)
            {
                foreach(var terminal in city.Terminals.Terminal)
                {
                    var regionRegex = new Regex(@",\s*(?<Region>[^,]+(?:обл|край|респ))");
                    var streetRegex = new Regex(@",\s*(?<Street>[^,]+(?:ул|пр-кт|кв-л))");
                    var houseRegex = new Regex(@"дом\s*№\s*(?<House>[^,]+)");
                    var apartmentRegex = new Regex(@"(?:помещение|к\.)\s*(?<Apartment>.+)$");

                    string regionName = regionRegex.Match(terminal.FullAddress).Groups["Region"].Value;
                    string streetName = streetRegex.Match(terminal.FullAddress).Groups["Street"].Value;
                    string houseName = houseRegex.Match(terminal.FullAddress).Groups["House"].Value;
                    string apartmentName = apartmentRegex.Match(terminal.FullAddress).Groups["Apartment"].Value;
                    string apartmentNumber = "";

                    if(!string.IsNullOrEmpty(apartmentName))
                        apartmentNumber = Regex.Match(apartmentName, @"\d+").Value;

                    offices.Add(new Office()
                    {
                        Id = Convert.ToInt32(terminal.Id),
                        AddressRegion = string.IsNullOrEmpty(regionName) ? null : regionName,
                        AddressCity = city.Name,
                        AddressStreet = string.IsNullOrEmpty(streetName) ? null : streetName,
                        AddressHouseNumber = string.IsNullOrEmpty(houseName) ? null : houseName,
                        AddressApartment = string.IsNullOrEmpty(apartmentNumber) ? null : Convert.ToInt32(apartmentNumber),
                        CityCode = city.Code,
                        Code = terminal.AddressCode?.Street_Code,
                        Coordinates = new Coordinates()
                        {
                            Longitude = Convert.ToDouble(terminal.Longitude.Replace('.', ',')),
                            Latitude = Convert.ToDouble(terminal.Latitude.Replace('.', ','))
                        },
                        CountryCode = "",//??
                        Type = terminal.Storage //???
                            ? Enums.OfficeType.WAREHOUSE
                            : terminal.IsPVZ
                                ? Enums.OfficeType.PVZ
                                : terminal.Default
                                    ? Enums.OfficeType.POSTAMAT
                                    : null,
                        Uuid = terminal.Id, //???
                        WorkTime = terminal.Worktables.Worktable.FirstOrDefault()?.Timetable ?? "",
                        Phones = terminal.Phones
                            .Select(p => new Phone()                        
                            {
                                PhoneNumber = p.Number,
                                Additional = p.Comment,
                                OfficeId = Convert.ToInt32(terminal.Id)
                            }).ToList()
                    });
                }
            }

            return offices;
        }
    }
}
