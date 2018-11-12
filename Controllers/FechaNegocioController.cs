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
        readonly APContexto apc;
        public FechaNegocioController(APContexto apc)
        {
            this.apc = apc;
        }
        [Authorize("Bearer", Roles = "")]
        [HttpGet]
        public IEnumerable<FechaNegocio> Listar()
        {
            return apc.FechaNegocio.ToList();
        }
        [HttpGet("{id}")]
        public FechaNegocio Listar(int id)
        {
            return apc.FechaNegocio.Where(y => y.Id == id).FirstOrDefault();
        }
        [HttpPost]
        public IActionResult Registrar([FromBody] FechaNegocio fechaNegocio)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados Invalido, Não foi possivel cadastrar");
            apc.FechaNegocio.Add(fechaNegocio);
            int rs = apc.SaveChanges();
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
            var fne = apc.FechaNegocio.Where(y => y.Id == id).FirstOrDefault();

            fne.Categoria = fechaNegocio.Categoria;
            fne.DescServ = fechaNegocio.DescServ;
            fne.Preco = fechaNegocio.Preco;
            fne.FormaPagamento = fechaNegocio.FormaPagamento;
            int rs = apc.SaveChanges();
            if(rs <1)
                return BadRequest("Houve uma falha interna e não foi possivel cadastrar");
            else
                return Ok(fechaNegocio);
        }
        [HttpDelete("{id}")]
        public IActionResult Apagar(int id)
        {

            var fne = apc.FechaNegocio.Where(x => x.Id == id).FirstOrDefault();
            if (fne == null)
                return BadRequest("Cliente não localizado");

            apc.FechaNegocio.Remove(fne);
            int rs = apc.SaveChanges();

            if (rs > 0)
                return Ok();
            else
                return BadRequest();

        }
    }
}