//Register your Repository

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Repositories;
using Catalog.Settings;
using Catelog.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Catelog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //used to read some config info from multiple sources, anytime u 
        //need to read config info from multiple souces ie env vars or files
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //Used to register services used across ur app
        public void ConfigureServices(IServiceCollection services)
        {

            //Tell MongoDB Driver how to Serialize your data u store (Item here)
            //tells it anytime it sees a Guid in any entity it wil serialize it as a string in DB
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            //Register MongoDB
            //AddSingleton - > provides 1 copy of MongoDB client. 
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                //Create Settings Object
                //grab settings from AppSettings.json using the MongoDbSettings Class
                //GetSection(one_of_settings_in_appsettins.json)
                var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();

                // construct Mongo Client instance
                return new MongoClient(settings.ConnectionString);
            });

            //Register Mongo DB Items Reop
            services.AddSingleton<IItemsRepository, MongoDbItemsRepository>();

            //register ur Repository - OLD REPO BEFORE MONGO DB!
            //Add Singleton will create one copy of the instance type. So only one will be created
            //and reused as needed
            //format: AddSingleton<Interface, concreteInstance>

            //services.AddSingleton<IItemsRepository, InMemItemsRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catelog", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Configures the request pipeline. defines some middlewares, which runs before ur controller runs.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catelog v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
