using Autofac;
using Bank.App.IFace;
using Microsoft.Extensions.Configuration;
using static System.Net.WebRequestMethods;

namespace Bank.App.DI;
public class BankDI : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var environment = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(string.Format("appsettings.{0}.json", environment), optional: true);

        var configuration = config?.Build();
        builder.RegisterInstance(configuration);



        var _appSettings = configuration.GetSection("config");

        builder.RegisterInstance(_appSettings).As<IConfigurationSection>();


        builder.RegisterType<Mongo.Repository.User.UserRepository>()
            .As<IUserRepository>().SingleInstance();
        builder.RegisterType<Mongo.Repository.Account.AccountRepository>()
        .As<IAccountRepository>().SingleInstance();

        builder.RegisterType<Business.AccountBusiness>()
            .As<IAccountBusiness>().SingleInstance();
        builder.RegisterType<Business.UserBusiness>()
        .As<IUserBusiness>().SingleInstance();

    }
}

