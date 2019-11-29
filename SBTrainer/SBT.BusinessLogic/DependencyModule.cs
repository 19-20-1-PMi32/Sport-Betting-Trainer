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

            builder.RegisterModule<Database.DependencyModule>();
        }
    }
}
