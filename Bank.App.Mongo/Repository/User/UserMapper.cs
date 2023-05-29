using System;
using AutoMapper;
using static System.Net.Mime.MediaTypeNames;

namespace Bank.App.Mongo.Repository.User
{

    internal static class UserMapper
    {

        internal static Model.User Map(this UserEntity entity)
        {
            if (entity == null) return null;

            var autoMapper = ConfigureAutoMapper();

            var _result = autoMapper.Map<UserEntity, Model.User>(entity);
            return _result;

        }

        internal static UserEntity Map(this Model.User entity)
        {
            if (entity == null) return null;

            var autoMapper = ConfigureAutoMapper();

            var _result = autoMapper.Map<Model.User, UserEntity>(entity);
            return _result;

        }


        private static IMapper ConfigureAutoMapper()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model.User, UserEntity>()
                .ForMember(c => c.UserId, a => a.MapFrom(u => u.UserId))
                     .ForMember(c => c.Name, a => a.MapFrom(u => u.Name))
                       
                .ReverseMap();


            });

            return config.CreateMapper();
        }
    }
}
