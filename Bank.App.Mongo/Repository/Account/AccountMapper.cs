using System;
using AutoMapper;
using Bank.App.Mongo.Repository.User;

namespace Bank.App.Mongo.Repository.Account
{
    internal static class AccountMapper
    {

        internal static Model.Account Map(this AccountEntity entity)
        {
            if (entity == null) return null;

            var autoMapper = ConfigureAutoMapper();

            var _result = autoMapper.Map<AccountEntity, Model.Account>(entity);
            return _result;

        }

        internal static AccountEntity Map(this Model.Account entity)
        {
            if (entity == null) return null;

            var autoMapper = ConfigureAutoMapper();

            var _result = autoMapper.Map<Model.Account, AccountEntity>(entity);
            return _result;

        }


        private static IMapper ConfigureAutoMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.Account, AccountEntity>()
                .ForMember(c => c.AccountId, a => a.MapFrom(u => u.AccountId))
                     .ForMember(c => c.Balance, a => a.MapFrom(u => u.Balance))
          
                .ReverseMap();


            });

            return config.CreateMapper();
        }
    }
}
