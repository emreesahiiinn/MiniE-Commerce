using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Business.Mapping.AutoMapper;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

var services = builder.Services;
var configuration = builder.Configuration;

services.AddAutoMapper(typeof(Profiles));

services.AddControllers(options => { options.ModelBinderProviders.Insert(0, new ArrayModelBinderProvider()); });

services.AddHttpClient();
services.AddCors();

var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

services.AddDependencyResolvers([new CoreModule()]);

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddMetrics();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(builder => builder.WithOrigins("http://0.0.0.0:5187").AllowAnyHeader());


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();