using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBT.Database;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SBT.Core.Parser;

namespace SBT.BusinessLogic.Services
{
    public class SportsUpdateService : BackgroundService
    {
        private readonly ILogger<SportsUpdateService> _logger;
        private readonly int updateTimeDellay;
        private readonly IUnitOfWork _unitOfWork;

        public SportsUpdateService(ILogger<SportsUpdateService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            updateTimeDellay = 21600000; //every 6 hours
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Sports update service is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Sports update service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Updating...");

                var sports = await _unitOfWork.SportRepository.GetAll();

                foreach (var sport in sports)
                {
                    await UpdateSport(sport);
                }

                await Task.Delay(updateTimeDellay, stoppingToken);
            }

            _logger.LogDebug("Sports update service is stopping.");
        }

        private async Task UpdateSport(Database.Entities.Sport sport)
        {
            _logger.LogDebug("OddsApi requester: GetSports().");
            //var sportsJson = OddsApi.GetSports();
            var sportsJson = File.ReadAllText("TestData/sports.json");

            _logger.LogDebug("SportDataRepository: GetAll().");
            var sportDataInDatabase = (await _unitOfWork.SportDataRepository.GetAll())
                .Where(x => x.Id.StartsWith(sport.Id));

            _logger.LogDebug("Parser: ParseSports(spotsJson).");
            var sportData = Parser.ParseSports(sportsJson)
                .Where(x => x.SportId.StartsWith(sport.Id) 
                            && !sportDataInDatabase.Any(y => y.Id == x.SportId));

            foreach (var spData in sportData)
            {
                var data = new Database.Entities.SportData()
                {
                    Id = spData.SportId,
                    IsActive = spData.IsActive,
                    Group = spData.Group,
                    Details = spData.Details,
                    Title = spData.Title,
                    SportId = sport.Id
                };

                _logger.LogDebug("SportDataRepository: Insert(data).");
                _unitOfWork.SportDataRepository.Insert(data);
            }

            _logger.LogDebug("UintOfWork: Commit().");
            await _unitOfWork.Commit();
        }
    }
}
