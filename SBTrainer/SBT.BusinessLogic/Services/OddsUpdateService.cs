using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBT.Database;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SBT.Core.Parser;
using SBT.Core.Requesters;

namespace SBT.BusinessLogic.Services
{
    public class OddsUpdateService : BackgroundService
    {
        private readonly ILogger<SportsUpdateService> _logger;
        private readonly int updateTimeDellay;
        private readonly IUnitOfWork _unitOfWork;

        public OddsUpdateService(ILogger<SportsUpdateService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            updateTimeDellay = 300000; //every 5 minutes
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Odds update service is starting.");

            stoppingToken.Register(() =>
                _logger.LogDebug("Odds update service is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Updating...");

                //var sportData = await _unitOfWork.SportDataRepository.GetAll();

                //foreach (var data in sportData)
                //{
                //    await UpdateOdd(data);
                //}

                await UpdateOdd(await _unitOfWork.SportDataRepository.Get("soccer_uefa_champs_league"));

                await Task.Delay(updateTimeDellay, stoppingToken);
            }

            _logger.LogDebug("Odds update service is stopping.");
        }

        private async Task UpdateOdd(Database.Entities.SportData sportData)
        {
            _logger.LogDebug("OddsApi requester: GetOdds(sportData.Id).");
            // var oddsJson = OddsApi.GetOdds(sportData.Id);
            var oddsJson = File.ReadAllText("TestData/odds.json");

            _logger.LogDebug("GameRepository: GetAll().");
            var gameInDatabase = (await _unitOfWork.GameRepository.GetAll())
                .Where(x => x.SportDataId == sportData.Id);

            _logger.LogDebug("Parser: ParseOdds(oddsJson).");
            var oddsData = Parser.ParseOdds(oddsJson);

            foreach (var oddData in oddsData)
            {
                var game = new Database.Entities.Game()
                {
                    Team1 = oddData.Team1,
                    Team2 = oddData.Team2,
                    SportDataId = sportData.Id
                };

                _logger.LogDebug("GameRepository: Insert(game).");
                _unitOfWork.GameRepository.Insert(game);

                foreach (var siteInfo in oddData.SitesInfo)
                {
                    var site = new Database.Entities.Site()
                    {
                        Name = siteInfo.Name,
                        LastUpdate = siteInfo.LastUpdate,
                        FirstWin = siteInfo.FirstWinCoef,
                        SecondWin = siteInfo.SecondWinCoef,
                        Draw = (siteInfo.FirstWinCoef + siteInfo.SecondWinCoef) / 2.0f,
                        GameId = game.Id
                    };

                    _logger.LogDebug("SiteRepository: Insert(site).");
                    _unitOfWork.SiteRepository.Insert(site);
                }
            }

            _logger.LogDebug("UintOfWork: Commit().");
            await _unitOfWork.Commit();
        }
    }
}
