using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiPedreirao.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ApiPedreirao
{
    public class Program
    {
        public static void Main(string[] args){
        var ambiente = BuildWebHost(args);
            using(var escopo = ambiente.Services.CreateScope())
            {
                var servico = escopo.ServiceProvider;
                try{
                    var contex = servico.GetRequiredService<APContexto>();
                    InicializarBanco.Iniciar(contex);
                }
                catch(Exception e){
                    var logger = servico.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e,"Ocorreu um erro enquanto os dados foram enviados");
                }
            }
            ambiente.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
       
    }
}
