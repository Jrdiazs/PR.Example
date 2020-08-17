using AutoMapper;
using PR.Models;
using PR.Services.ModelResponse;

namespace PR.Services.Mapping
{
    /// <summary>
    /// Mapeo de datos para la entidad UserApp vs UserResponse
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Mapeo de datos para la entidad UserApp vs UserResponse
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap<UserApp, UserViewModel>().
                ForMember(x => x.DocumentAbbreviation, y => y.MapFrom(src => src.DocumentType != null ? src.DocumentType.DocumentAbbreviation : string.Empty)).
                ForMember(x => x.DocumentType, y => y.MapFrom(src => src.DocumentType != null ? src.DocumentType.DocumentDescription : string.Empty)).
                ForMember(x => x.DocumentTypeId, y => y.MapFrom(src => src.DocumentTypeId)).
                ForMember(x => x.UserDateCreation, y => y.MapFrom(src => src.UserDateCreation)).
                ForMember(x => x.UserDocumentId, y => y.MapFrom(src => src.UserDocumentId)).
                ForMember(x => x.UserEmail, y => y.MapFrom(src => src.UserEmail)).
                ForMember(x => x.UserFullName, y => y.MapFrom(src => src.UserFullName)).
                ForMember(x => x.UserId, y => y.MapFrom(src => src.UserId)).
                ForMember(x => x.UserLastName, y => y.MapFrom(src => src.UserLastName)).
                ForMember(x => x.UserName, y => y.MapFrom(src => src.UserName)).
                ForMember(x => x.UserNickName, y => y.MapFrom(src => src.UserNickName));

            CreateMap<UserViewModel, UserApp>().
                ForMember(x => x.DocumentTypeId, y => y.MapFrom(src => src.DocumentTypeId)).
                ForMember(x => x.UserDateCreation, y => y.MapFrom(src => src.UserDateCreation)).
                ForMember(x => x.UserDocumentId, y => y.MapFrom(src => src.UserDocumentId)).
                ForMember(x => x.UserEmail, y => y.MapFrom(src => src.UserEmail)).
                ForMember(x => x.UserFullName, y => y.MapFrom(src => src.UserFullName)).
                ForMember(x => x.UserId, y => y.MapFrom(src => src.UserId)).
                ForMember(x => x.UserLastName, y => y.MapFrom(src => src.UserLastName)).
                ForMember(x => x.UserName, y => y.MapFrom(src => src.UserName)).
                ForMember(x => x.UserNickName, y => y.MapFrom(src => src.UserNickName));
        }
    }
}