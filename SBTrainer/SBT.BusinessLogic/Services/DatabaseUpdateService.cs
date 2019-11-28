using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBT.Database;
using SBT.Core.Requesters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SBT.Core.Parser;

namespace SBT.BusinessLogic.Services
{
    public class DatabaseUpdateService : BackgroundService
    {
        private readonly ILogger<DatabaseUpdateService> _logger;
        private readonly int updateTimeDellay;
        private readonly IUnitOfWork _unitOfWork;

        public DatabaseUpdateService(ILogger<DatabaseUpdateService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            updateTimeDellay = 500000;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Database update service is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Database update service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Updating...");

                var sports = await _unitOfWork.SportRepository.GetAll();

                foreach (var sport in sports)
                {
                    await UpdateSport(sport);
                }

                //var spotsData = OddsApi.GetSports();

                await Task.Delay(updateTimeDellay, stoppingToken);
            }

            _logger.LogDebug("Database update service is stopping.");
        }

        private async Task UpdateSport(Database.Entities.Sport sport)
        {
            _logger.LogDebug("OddsApi requester: GetSports().");
            var spotsJson = OddsApi.GetSports();

            _logger.LogDebug("Parser: ParseSports(spotsJson).");
            var sportData = Parser.ParseSports(spotsJson).Where(x => x.SportId.StartsWith(sport.Id));

            foreach (var spData in sportData)
            {
                var data = new Database.Entities.SportData()
                {
                    IsActive = spData.IsActive,
                    Group = spData.Group,
                    Details = spData.Details,
                    Title = spData.Title,
                    SportId = spData.SportId
                };

                _logger.LogDebug("SportDataRepository: Insert(data).");
                _unitOfWork.SportDataRepository.Insert(data);
            }

            _logger.LogDebug("UintOfWork: Commit().");
            await _unitOfWork.Commit();
        }
    }
}
