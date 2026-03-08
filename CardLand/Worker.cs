using CardLand.Data;
using CardLand.Services.TerminalsParsingService;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CardLand
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger; 
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var offices = await TerminalsParsingService.Parse();
                    _logger.LogInformation($"{DateTime.Now} INFO: Загружено {offices.Count()} терминалов из JSON");

                    using var scope = _serviceProvider.CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<DellinDictionaryDbContext>();
                    var officeIds = offices.Select(o => o.Id);

                    var toDeleteCount = await dbContext.Offices.CountAsync(o => !officeIds.Contains(o.Id));
                    var toUpdateCount = await dbContext.Offices.CountAsync(o => officeIds.Contains(o.Id));

                    await dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE \"{nameof(dbContext.Offices)}\" RESTART IDENTITY CASCADE");

                    await dbContext.BulkInsertAsync(offices);
                    await dbContext.BulkInsertAsync(offices.SelectMany(o => o.Phones));


                    _logger.LogInformation($"{DateTime.Now} INFO: Удалено {toDeleteCount} терминалов из JSON");
                    _logger.LogInformation($"{DateTime.Now} INFO: Сохранено {officeIds.Count() - toUpdateCount} новых терминалов из JSON");


                }
                catch(Exception ex)
                {
                    _logger.LogError($"{DateTime.Now} Error: Ошибка импорта: {ex.Message}\n{ex.StackTrace}");
                }
                finally
                {

                }

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
