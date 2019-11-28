using Autofac;
using SBT.Database.Repository;
using SBT.Database.Entities;

namespace SBT.Database
{
    public class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>().As<IGenericRepository<Account>>();
            builder.RegisterType<BetRepository>().As<IGenericRepository<Bet>>();
            builder.RegisterType<GameRepository>().As<IGenericRepository<Game>>();
            builder.RegisterType<SiteRepository>().As<IGenericRepository<Site>>();
            builder.RegisterType<SportDataRepository>().As<IGenericRepository<SportData>>();
            builder.RegisterType<SportRepository>().As<IGenericRepository<Sport>>();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
        }
    }
}
