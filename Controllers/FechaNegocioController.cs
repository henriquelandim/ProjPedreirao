using System.Collections.Generic;
using System.Linq;
using ApiPedreirao.Data;
using ApiPedreirao.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPedreirao.Controllers
{
    [Route("api/[controller]")]
    public class FechaNegocioController : Controller
    {
        FechaNegocio fn = new FechaNegocio();
        readonly APContexto contexto;
        public FechaNegocioController(APContexto contexto)
        {
            this.contexto = contexto;
        }
        [Authorize("Bearer", Roles = "Cliente")]
        [HttpGet]
        public IEnumerable<FechaNegocio> Listar()
        {
            return contexto.FechaNegocio.ToList();
        }
        [HttpGet("{id}")]
        public FechaNegocio Listar(int id)
        {
            return contexto.FechaNegocio.Where(y => y.Id == id).FirstOrDefault();
        }
        [HttpPost]
        public IActionResult Registrar([FromBody] FechaNegocio fechaNegocio)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Invalido, Não foi possivel cadastrar");
            contexto.FechaNegocio.Add(fechaNegocio);
            int rs = contexto.SaveChanges();
            if (rs < 1)
                return BadRequest("Houve uma falha interna e não foi possivel registrar.");
            else
                return Ok(fechaNegocio);
        }
        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] FechaNegocio fechaNegocio, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest("NÃO foi possivel enviar os dados");
            var fne = contexto.FechaNegocio.Where(y => y.Id == id).FirstOrDefault();

            fne.Categoria = fechaNegocio.Categoria;
            fne.DescServ = fechaNegocio.DescServ;
            fne.Preco = fechaNegocio.Preco;
            fne.FormaPagamento = fechaNegocio.FormaPagamento;
            int rs = contexto.SaveChanges();
            if(rs <1)
                return BadRequest("Houve uma falha interna e não foi possivel cadastrar");
            else
                return Ok(fechaNegocio);
        }
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {

            var fne = contexto.FechaNegocio.Where(x => x.Id == id).FirstOrDefault();
            if (fne == null)
                return BadRequest("Cliente não localizado");

            contexto.FechaNegocio.Remove(fne);
            int rs = contexto.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();

        }
    }
}