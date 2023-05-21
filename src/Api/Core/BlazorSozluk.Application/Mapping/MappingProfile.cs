using AutoMapper;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.Domain.Models;

namespace BlazorSozluk.Application.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            CreateMap<CreateUserCommand, User>();
            CreateMap<UpdateUserCommand, User>();
            //CreateMap<UserDetailViewModel, User>().ReverseMap();
            //CreateMap<CreateEntryCommand, Entry>().ReverseMap();
            //CreateMap<Entry, GetEntriesViewModel>().ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));
            //CreateMap<CreateEntryCommentCommand, EntryComment>().ReverseMap();
        }
    }
}
