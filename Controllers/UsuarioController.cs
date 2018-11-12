using ApiPedreirao.Models;
using ApiPedreirao.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using ApiPedreirao.Security;

namespace ApiPedreirao.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        Usuario usu = new Usuario();
        readonly APContexto apc;
        public UsuarioController(APContexto apc)
        {
            this.apc = apc;
        }
        [Authorize("Bearer", Roles = "Usuario")]
        [HttpGet]
        public IEnumerable<Usuario> Listar()
        {
            return apc.Usuario.ToList();
        }
        [HttpGet("{id}")]
        public Usuario Listar(int id)
        {
            return apc.Usuario.Where(x => x.Id == id).FirstOrDefault();
        }
        [HttpPost]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Não foi possivel cadastrar, dados fora de padrão!");

            Seguranca sg = new Seguranca();
            usuario.Senha = sg.encriptografar(usuario.Senha);
            
            apc.Usuario.Add(usuario);
            int res = apc.SaveChanges();
            if (res < 1)
                return BadRequest("Houve uma falha interna e não foi possivel cadastrar");
            else
                return Ok(usuario);
        }
        [HttpPut("{id}")]
        public IActionResult Atubaina([FromBody] Usuario usuario, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("não foi possivel enviar os dados para atualizar");

            var us = apc.Usuario.Where(y => y.Id == id).FirstOrDefault();

            /*#####################################################################################
               ATENÇÃO!! 
            NESSA CONDIÇÃO DE ATUALIZAR OS DADOS DA COLUNA NomeUsuario,CPF e Email DEVERÁ SER APURADA COM CUIDADO
            POIS COMO SE TRATA DE DADOS UNIQUE, DEPENDENDO DO DADO QUE COLOCAR NÃO ESTARÁ
            DISPONÍVEL, PORTANTO TODA VEZ QUE O CAMPO 'NomeUsuario, CPF e Email' FOR PREENCHIDO PRECISARÁ
            DE UM SELECT, P APURAR SE O DADO ESTÁ DISPONIVEL OU NÃO... A DUVIDA QUE TENHO È
            COMO POSSO RODAR ESSE SELECT DE FORMA INDEPENDENTE VIA API? */
            us.NomeUsuario = usuario.NomeUsuario;
            us.NomeCompleto = usuario.NomeCompleto;
            us.NomeSocial = usuario.NomeSocial;
            us.Email = usuario.Email;
            us.Telefone = usuario.Telefone;
            us.Sexo = usuario.Sexo;
            us.DataNascimento = usuario.DataNascimento;
            us.TipoUsuario = usuario.TipoUsuario;
            us.Endereco = usuario.Endereco;
            us.Senha = usuario.Senha;
            us.CNPJ = usuario.CNPJ;
            us.RazaoSocial = usuario.RazaoSocial;
            us.Especialidade = usuario.Especialidade;
            us.Especialidade2 = usuario.Especialidade2;
            us.Especialidade3 = usuario.Especialidade3;

            apc.Usuario.Update(us);
            int rs = apc.SaveChanges();
            if (rs < 1)
                return BadRequest("Hoube uma falha interna e não foi possivel cadastrar");
            else
                return Ok(usuario);


        }
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            var us = apc.Usuario.Where(y => y.Id == id).FirstOrDefault();
            if (us == null)
                return BadRequest("Usuario não encontrado.");

            apc.Usuario.Remove(us);
            int rs = apc.SaveChanges();
            if (rs > 0)
                return Ok();
            else
                return BadRequest();
        }

    }
}