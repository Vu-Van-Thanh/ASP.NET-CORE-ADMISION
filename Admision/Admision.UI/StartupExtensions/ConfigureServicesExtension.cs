using Admission.Core.Domain.Entities;
using Admission.Core.Domain.IdentityEntities;
using Admission.Core.Domain.RepositoryContracts;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Admission.Filters.ActionFilters;
using Admission.Infrastructure.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;

namespace Admission
{
    public static class ConfigureServicesExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddTransient<ResponseHeaderActionFilter>();

            //it adds controllers and views as services
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<ResponseHeaderActionFilter>(5);

                /*var logger = services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();

                options.Filters.Add(new ResponseHeaderActionFilter(logger)
                {
                    Key = "My-Key-From-Global",
                    Value = "My-Value-From-Global",
                    Order = 2
                });*/

                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                
            });


            //add services into IoC container
            services.AddScoped<ICountriesRepository, CountriesRepository>();

            services.AddScoped<IGroupsRepository,GroupsRepository>();
            services.AddScoped<IPostsRepository,PostsRepository>();
            services.AddScoped<ICommentsRepository,CommentsRepository>();
            services.AddScoped<IStudentsRepository, StudentsRepository>();
            services.AddScoped<ICountriesGetterService, CountriesGetterService>();
            services.AddScoped<ICountriesAdderService, CountriesAdderService>();
            services.AddScoped<ICountriesUploaderService, CountriesUploaderService>();
            services.AddScoped<IHighSchoolsRepository, HighSchoolsRepository>();
            services.AddSingleton<IVnPayService, VnPayService>();
/*            services.AddScoped<IPersonsGetterService, PersonsGetterServiceWithFewExcelFields>();
            services.AddScoped<PersonsGetterService, PersonsGetterService>();


            services.AddScoped<IPersonsAdderService, PersonsAdderService>();
            services.AddScoped<IPersonsDeleterService, PersonsDeleterService>();
            services.AddScoped<IPersonsUpdaterService, PersonsUpdaterService>();
            services.AddScoped<IPersonsSorterService, PersonsSorterService>();*/
            services.AddScoped<IGroupsService, GroupService>();
            services.AddScoped<IPostsService,PostService>();
            services.AddScoped<IStudentMediasService,StudentMediasService>();
            services.AddScoped<IFilesService, FilesService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IStudentsService, StudentService>();
            services.AddScoped<IHighSchoolsService, HighSchoolService>();
            services.AddScoped<IArticlesService, ArticleService>();
            services.AddScoped<IArticlesRepository, ArticlesRepository>();
            services.AddScoped<IMediasRepository, MediasRepository>();

/*            services.AddTransient<PersonsListActionFilter>();*/
            services.AddScoped<IMajorsService, MajorService>();
            services.AddScoped<IMajorsRepository, MajorsRepository>();

            services.AddScoped<IStudentMediasRepository, StudentMediasRepository>();


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            //Enable Identity in this project
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 3; //Eg: AB12AB (unique characters are A,B,1,2)
            })
             .AddEntityFrameworkStores<ApplicationDbContext>()

             .AddDefaultTokenProviders()

             .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()

             .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //enforces authoriation policy (user must be authenticated) for all the action methods

                options.AddPolicy("NotAuthorized", policy =>
       {
                 policy.RequireAssertion(context =>
        {
                  return !context.User.Identity.IsAuthenticated;
              });
             });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
            });

            services.AddHttpLogging(options =>
            {
                options.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
            });

            return services;
        }
    }
}
