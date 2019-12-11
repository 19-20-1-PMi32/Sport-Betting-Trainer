using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SBT.BusinessLogic.Contracts;
using SBT.WebApp.Models;
using SBT.WebApp.ViewModels;

namespace SBT.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISportService _sportService;
        private readonly ISportDataService _sportDataService;
        private readonly IGameService _gameService;
        private readonly ISiteService _siteService;
        private readonly IBetService _betService;
        private readonly IAccountService _accountService;

        public HomeController(ISportService sportService, ISportDataService sportDataService,
            IGameService gameService, ISiteService siteService, IBetService betService, IAccountService accountService)
        {
            _sportService = sportService;
            _sportDataService = sportDataService;
            _gameService = gameService;
            _siteService = siteService;
            _betService = betService;
            _accountService = accountService;
        }
        /// <summary>
        /// Obtain information from database and put it in SportSectionModel
        /// </summary>
        /// <returns>Index page</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var sportSectionModel = new SportSectionModel()
            {
                SportModels = new List<SportModel>()
            };

            var sports = await _sportService.GetSports();
            var sportData = await _sportDataService.GetAllSportData();
            var games = await _gameService.GetAllGames();

            foreach (var sport in sports)
            {
                var filteredSportData = sportData.Where(x => x.SportId == sport.Id);
                var sportDataModels = new List<SportDataModel>();

                foreach (var spData in filteredSportData)
                {
                    var filteredGames = games.Where(x => x.SportDataId == spData.Id);
                    var gameModels = new List<GameModel>();

                    foreach (var game in filteredGames)
                    {
                        gameModels.Add(new GameModel()
                        {
                            Id = game.Id,
                            Team1 = game.Team1,
                            Team2 = game.Team2
                        });
                    }

                    if (gameModels.Count != 0)
                    {
                        sportDataModels.Add(new SportDataModel()
                        {
                            Group = spData.Group,
                            Title = spData.Title,
                            GameModels = gameModels
                        });
                    }
                }

                sportSectionModel.SportModels.Add(new SportModel()
                {
                    Name = sport.Name,
                    SportDataModels = sportDataModels
                });
            }

            return View(sportSectionModel);
        }

        /// <summary>
        /// Get information when make bet button was pushed and put it into MakeBetPartialModel
        /// </summary>
        /// <param name="id">Game id</param>
        /// <returns>Partial view to display make bet form</returns>
        [HttpPost]
        public async Task<IActionResult> Bet(int id)
        {
            var game = await _gameService.GetGame(id);
            var spData = await _sportDataService.GetData(game.SportDataId);
            var sport = await _sportService.GetSport(spData.SportId);
            var odd = (await _siteService.GetSitesByGameId(game.Id)).FirstOrDefault();

            var model = new MakeBetPartialModel()
            {
                SportName = sport.Name,
                Team1 = game.Team1,
                Team2 = game.Team2,
                Team1Coef = odd.FirstWin,
                DrawCoef = odd.Draw ?? 1.0f,
                Team2Coef = odd.SecondWin,
                GameId = game.Id
            };

            return PartialView(model);
        }

        /// <summary>
        /// Handle information from make bet form and update bet entity
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirection to index page</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> MakeBet(MakeBetPartialModel model)
        {
            if (ModelState.IsValid)
            {
                float coef = 0.0f;
                switch (model.Type)
                {
                    case "first":
                        coef = model.Team1Coef;
                        break;
                    case "draw":
                        coef = model.DrawCoef;
                        break;
                    case "second":
                        coef = model.Team2Coef;
                        break;
                }

                var bet = new Database.Entities.Bet()
                {
                    AccountEmail = User.Identity.Name,
                    Coefficient = coef,
                    Type = model.Type,
                    Money = model.Money,
                    GameId = model.GameId
                };

                await _betService.CreateBet(bet);
            }

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Get information about account
        /// </summary>
        /// <returns>Page with account statistic</returns>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            var account = await _accountService.GetAccount(User.Identity.Name);

            var bets = await _betService.GetBetsForUser(User.Identity.Name);

            float earned = 0.0f, spent = 0.0f;

            foreach (var bet in bets)
            {
                if (bet.Type == bet.Result)
                {
                    earned += bet.Coefficient * bet.Money;
                }
                else
                {
                    spent += bet.Money;
                }
            }

            var model = new StatisticModel
            {
                MoneyModel = new MoneyModel()
                {
                    Current = account.Ballance,
                    Earned = earned,
                    Spent = spent
                }
            };

            return View(model);
        }

        /// <summary>
        /// Handle replenish form and update account ballance
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Redirection to About page</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Replenish(StatisticModel model)
        {
            var account = await _accountService.GetAccount(User.Identity.Name);
      
            account.Ballance += model.Money;

            await _accountService.UpdateAccount(account);

            return RedirectToAction("About", "Home");
        }

        /// <summary>
        /// Contact page route
        /// </summary>
        /// <returns>Contact page</returns>
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Privacy page route
        /// </summary>
        /// <returns>Privacy page</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Privacy page route
        /// </summary>
        /// <returns>Error page</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
