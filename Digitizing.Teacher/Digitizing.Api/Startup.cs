using Library.Common.Caching;
using Library.Common.Helper;
using Library.BusinessLogicLayer;
using Library.DataAccessLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO.Compression;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Digitizing.Api.Cms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            FileHelper.configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddControllers();
            services.TryAddSingleton<ICacheProvider, LZ4RedisCache>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IDatabaseHelper, DatabaseHelper>();

			services.AddTransient<IWebsiteTagBusiness, WebsiteTagBusiness>();
			services.AddTransient<IWebsiteTagRepository,WebsiteTagRepository>(); 
            services.AddTransient<IInternshipClassBusiness, InternshipClassBusiness>();
            services.AddTransient<IInternshipClassRepository, InternshipClassRepository>();
            services.AddTransient<IInternshipStudentBusiness, InternshipStudentBusiness>();
            services.AddTransient<IInternshipStudentRepository, InternshipStudentRepository>();
            services.AddTransient<IStudentClassBusiness, StudentClassBusiness>();
            services.AddTransient<IStudentClassRepository, StudentClassRepository>();
            services.AddTransient<IEvaluateRecruitmentBusiness, EvaluateRecruitmentBusiness>();
            services.AddTransient<IEvaluateRecruitmentRepository, EvaluateRecruitmentRepository>();
            services.AddTransient<IReportRecruitmentRepository, ReportRecruitmentRepository>();
            services.AddTransient<IReportRecruitmentBusiness, ReportRecruitmentBusiness>();
            services.AddTransient<IScientificResearchRepository, ScientificResearchRepository>();
            services.AddTransient<IScientificResearchBusiness, ScientificResearchBusiness>();
            services.AddTransient<IStudentProjectRegisterRepository, StudentProjectRegisterRepository>();
            services.AddTransient<IStudentProjectRegisterBusiness, StudentProjectRegisterBusiness>();
            services.AddTransient<IStudentInformationRepository, StudentInformationRepository>();
            services.AddTransient<IStudentInformationBusiness, StudentInformationBusiness>();
            services.AddTransient<ISubjectScoreRepository, SubjectScoreRepository>();
            services.AddTransient<ISubjectScoreBusiness, SubjectScoreBusiness>();
            services.AddTransient<IPointTrainingRepository, PointTrainingRepository>();
            services.AddTransient<IPointTrainingBusiness, PointTrainingBusiness>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            var MimeTypes = new[]
                                {
                                    // General
                                    "text/plain",
                                    // Static files
                                    "text/css",
                                    "application/javascript",
                                    // MVC
                                    "text/html",
                                    "application/xml",
                                    "text/xml",
                                    "application/json",
                                    "text/json",
                                    "image/svg+xml",
                                    "application/atom+xml"
                                };
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(MimeTypes); ;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = Int32.MaxValue;
                x.MultipartBodyLengthLimit = Int32.MaxValue;
                x.MultipartHeadersLengthLimit = Int32.MaxValue;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            app.UseRouting();
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseCors("AllowAll");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHttpsRedirection();
            app.UseDeveloperExceptionPage();
        }
    }
}
