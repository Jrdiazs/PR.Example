using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PR.Data;
using PR.Services.Mapping;

namespace PR.Services.InjectConfigure
{
    /// <summary>
    /// Contenedor de repositorios y servicios
    /// </summary>
    public static class InitContainer
    {
        /// <summary>
        /// Contenedor de repositorios y servicios
        /// </summary>
        /// <param name="services"></param>
        public static void AddServices(IServiceCollection services)
        {
            //AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            ///Repositorios
            services.AddScoped<IDocumentTypeData, DocumentTypeData>();
            services.AddScoped<IUserAppData, UserAppData>();
            services.AddScoped<IMenusData, MenusData>();

            //Servicios
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IMenuServices, MenuServices>();
        }
    }
}