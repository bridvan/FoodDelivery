using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace DataAccessLayer
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {

            // initialize Hosting Environment 
            _env = env;


            //use App Settings File  appsettingd ,appsettings.Development,appsettings.Production
            var builder = new ConfigurationBuilder().SetBasePath(_env.ContentRootPath)
                .AddJsonFile($"appsettings.{_env.EnvironmentName}.json");

            //initailoze ConfigurationRoot Object 
            _config = builder.Build();

        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //use One Object Of _configuration 
            services.AddSingleton(_config);

            //Use DbContext In programe
            services.AddDbContext<LearningDbContext>();

            //use Identity in your PRoject 
            services.AddIdentity<AppUser, AppRole>()  //use Entity Frame Work Store
                    .AddEntityFrameworkStores<LearningDbContext>();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

          
        }
    }
}
