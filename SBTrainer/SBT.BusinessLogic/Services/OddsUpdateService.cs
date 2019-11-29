using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SBT.Database;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SBT.Core.Parser;
using SBT.Core.Requesters;
using System;

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
            await Task.Delay(15000); // prevents concurrent access

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
            //int currentTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            int currentTime = 1571000000;

            _logger.LogDebug("OddsApi requester: GetOdds(sportData.Id).");
            // var oddsJson = OddsApi.GetOdds(sportData.Id);
            var oddsJson = File.ReadAllText("TestData/odds.json");

            _logger.LogDebug("GameRepository: GetAll().");
            var gameInDatabase = (await _unitOfWork.GameRepository.GetAll())
                .Where(x => x.SportDataId == sportData.Id && x.CommenceTime > currentTime).ToArray();

            _logger.LogDebug("SiteRepository: GetAll().");
            var siteInDatabase = (await _unitOfWork.SiteRepository.GetAll()).ToArray();

            _logger.LogDebug("Parser: ParseOdds(oddsJson).");
            var oddsData = Parser.ParseOdds(oddsJson)
                .Where(x => x.CommenceTime > currentTime).ToArray();

            foreach (var oddData in oddsData)
            {
                var game = gameInDatabase
                    .Where(x => x.Team1 == oddData.Team1
                        && x.Team2 == oddData.Team2
                        && x.CommenceTime == oddData.CommenceTime)
                    .SingleOrDefault();
                var gameId = 0;

                if (game == null)
                {
                    game = new Database.Entities.Game()
                    {
                        Team1 = oddData.Team1,
                        Team2 = oddData.Team2,
                        CommenceTime = oddData.CommenceTime,
                        SportDataId = sportData.Id
                    };

                    _logger.LogDebug("GameRepository: Insert(game).");
                    _unitOfWork.GameRepository.Insert(game);

                    _logger.LogDebug("UintOfWork: Commit().");
                    await _unitOfWork.Commit();

                    gameId = game.Id;
                }
                else
                {
                    gameId = game.Id;
                }

                var sites = siteInDatabase.Where(x => x.GameId == gameId);
                
                foreach (var siteInfo in oddData.SitesInfo)
                {
                    var site = sites.Where(x => x.Name == siteInfo.Name).SingleOrDefault();

                    if (site != null)
                    {
                        site.FirstWin = siteInfo.FirstWinCoef;
                        site.SecondWin = siteInfo.SecondWinCoef;
                        site.Draw = (siteInfo.FirstWinCoef + siteInfo.SecondWinCoef) / 2.0f;
                        site.LastUpdate = siteInfo.LastUpdate;

                        _logger.LogDebug("SiteRepository: Update(site).");
                        _unitOfWork.SiteRepository.Update(site);
                    }
                    else
                    {
                        site = new Database.Entities.Site()
                        {
                            Name = siteInfo.Name,
                            LastUpdate = siteInfo.LastUpdate,
                            FirstWin = siteInfo.FirstWinCoef,
                            SecondWin = siteInfo.SecondWinCoef,
                            Draw = (siteInfo.FirstWinCoef + siteInfo.SecondWinCoef) / 2.0f,
                            GameId = gameId
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
}
