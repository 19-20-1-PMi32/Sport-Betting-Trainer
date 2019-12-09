using Autofac;
using SBT.BusinessLogic.Contracts;
using SBT.BusinessLogic.Services;

namespace SBT.BusinessLogic
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<SportService>().As<ISportService>();
            builder.RegisterType<SportDataService>().As<ISportDataService>();
            builder.RegisterType<GameService>().As<IGameService>();
            builder.RegisterType<SiteService>().As<ISiteService>();
            builder.RegisterType<BetService>().As<IBetService>();

            builder.RegisterModule<Database.DependencyModule>();
        }
    }
}
