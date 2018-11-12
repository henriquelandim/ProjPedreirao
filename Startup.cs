using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPedreirao.Data;
using ApiPedreirao.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ApiPedreirao
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        IConfiguration Configuration{get;}
        
        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<APContexto>(opt => 
                opt.UseMySql(Configuration.GetConnectionString("bancodados"))
                );
                /*
                O método ConfigureServices da classe Startup também passará por ajustes:

                Uma referência de TokenConfigurations será criada a partir do objeto vinculado à 
                propriedade Configuration e do conteúdo definido na seção de mesmo nome no arquivo appsettings.json;
                Instâncias dos tipos SigningConfigurations e TokenConfigurations serão 
                configuradas via método AddSingleton, de forma que uma única referência
                 das mesmas seja empregada durante todo o tempo em que a aplicação permanecer em 
                 execução. Quanto a UsersDAO, o método AddTransient determina que referências
                  desta classe sejam geradas toda vez que uma dependência for encontrada;
                Em seguida serão invocados os métodos AddAuthentication e AddJwtBearer. A chamada
                 a AddAuthentication especificará os schemas utilizados para a autenticação do tipo
                  Bearer. Já em AddJwtBearer serão definidas configurações como a chave e o algoritmo
                   de criptografia utilizados, a necessidade de analisar se um token ainda é válido e 
                   o tempo de tolerância para expiração de um token (zero, no caso desta aplicação de testes);
                A chamada ao método AddAuthorization ativará o uso de tokens com o intuito de autorizar
                 ou não o acesso a recursos da aplicação de testes.
                 */
                var signingConfigurations = new SigningConfigurations();
                services.AddSingleton(signingConfigurations);

                var tokenConfigurations = new TokenConfigurations();
                new ConfigureFromConfigurationOptions<TokenConfigurations>(
                    Configuration.GetSection("TokenConfigurations"))
                        .Configure(tokenConfigurations);
                services.AddSingleton(tokenConfigurations);


                services.AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = tokenConfigurations.Audience;
                    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

                // Ativa o uso do token como forma de autorizar o acesso
                // a recursos deste projeto
                services.AddAuthorization(auth =>
                {
                    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser().Build());
                });


             
            services.AddCors();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // //Ao Usar app.userCors, nós dizemos que o será permitido acessar a url da api, neste caso foi configurado que sera permetido
            //  o acesso a qualquer metodo e com passagem de qualquer cabeçalho. isso ira nos permitir enviar dados a api a partir 
            //  de um protocolo diferente como file por exemplo
            app.UseCors(cors => {cors.WithOrigins("http://localhost:5000").AllowAnyMethod().AllowAnyHeader();});

          app.UseMvc();
        }
    }
}
