using System;
using System.Linq;
using System.Data;
using ApiPedreirao.Models;

namespace ApiPedreirao.Data
{
    public class InicializarBanco
    {
        public static void Iniciar(APContexto contexto)
        {
            contexto.Database.EnsureCreated();
            if (contexto.Usuario.Any())
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
                DataNascimento = DateTime.Parse("2018-06-08") ,
                Sexo = "F",
                Endereco = "Rua Carapanã",
                TipoUsuario = "C",
                Especialidade = "Pedreira",
                Especialidade2 = "Azulegista",
                Especialidade3 = "Gesseira",
                CNPJ = "01234567891234",
                RazaoSocial = "AnaClara LTDA"

            };
            contexto.Usuario.Add(usuario);

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
            contexto.Perfil.Add(perfil);
            var formaPagamento = new FormaPagamento()
            {
            };
            contexto.FormaPagamento.Add(formaPagamento);
            var fechaNegocio = new FechaNegocio()
            {
                Categoria = "uma",
                DescServ ="umamamamamamamama", 
                Preco = 580.54,
                IdFormaDPag = formaPagamento.Id,
                IdUsuarioC = usuario.Id,
                IdUsuarioP = usuario.Id        
                

            };
            contexto.FechaNegocio.Add(fechaNegocio);
            contexto.SaveChanges();
            

        }
    }
}