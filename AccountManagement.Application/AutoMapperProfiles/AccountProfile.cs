using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;
using AutoMapper;

namespace AccountManagement.Application.AutoMapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account,AccountViewModel>();
            CreateMap<Account, EditAccount>().ForMember(x=>x.ProfilePicture,opt=> opt.Ignore());
        }
    }
}
