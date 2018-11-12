using System;
using System.Linq;
using System.Data;
using ApiPedreirao.Models;

namespace ApiPedreirao.Data
{
    public class InicializarBanco
    {
        public static void Iniciar(APContexto contex)
        {
            contex.Database.EnsureCreated();
            if (contex.Usuario.Any())
            {
                return;
            }
            var usuario = new Usuario()
            {
                NomeUsuario = "anaclara",
                Senha = "ana123",
                NomeCompleto = "Ana Clara Cardoso Pinheiro",
                NomeSocial = "Aninha",
                Email = "anaclaracardosopinheiro@outlook.com.br",
                Telefone = "11988326738",
                CPF = "51241895485",
                //DataNascimento = DateTime.Parse("2018-06-08") ,
                Sexo = 'F',
                Endereco = "Rua Carapanã, 33 - Jardim Santa Terezinha (Zona Leste) São Paulo - SP",
                TipoUsuario = "Cliente",
                Especialidade = "Pedreira",
                Especialidade2 = "Azulegista",
                Especialidade3 = "Gesseira",
                CNPJ = "01234567891234",
                RazaoSocial = "AnaClara LTDA"

            };
            contex.Usuario.Add(usuario);

            var perfil = new Perfil()
            {
                IdUsuario = usuario.Id,
                FotoPerfil = "caminho",
                FotoPort1 = "1",
                FotoPort2 = "1",
                FotoPort3 = "1",
                FotoPort4 = "1",
                FotoPort5 = "1"
            };
            contex.Perfil.Add(perfil);
            var formaPagamento = new FormaPagamento()
            {
                Teste = "teste",
            };
            contex.FormaPagamento.Add(formaPagamento);
            var fechaNegocio = new FechaNegocio()
            {
                Categoria = "",
                DescServ ="", 
                Preco = 580.54,
                IdFormaDPag = formaPagamento.Id,
                IdUsuarioC = usuario.Id,
                IdUsuarioP = usuario.Id        
                

            };
            contex.FechaNegocio.Add(fechaNegocio);
            contex.SaveChanges();
            

        }
    }
}