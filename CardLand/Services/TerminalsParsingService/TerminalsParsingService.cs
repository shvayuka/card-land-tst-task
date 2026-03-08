using CardLand.Services.TerminalsParsingService.Dtos;
using System.Text.Json;

namespace CardLand.Services.TerminalsParsingService
{
    public static class TerminalsParsingService
    {
        public static async Task<TerminalDictionaryDto> Parse()
        {
            var json = await File.ReadAllTextAsync(Consts.TERMINALS_FILE);

            var dictionary = JsonSerializer.Deserialize<TerminalDictionaryDto>(
                json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });

            if (dictionary == null)
                throw new Exception("Error during json deserialization");

            return dictionary;
        }
    }
}
