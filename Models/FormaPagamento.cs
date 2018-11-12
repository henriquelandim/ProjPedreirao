using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPedreirao.Models
{
    public class FormaPagamento
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(10)]
        public string Teste { get; set; }
        public ICollection<FechaNegocio> FechaNegocio { get; set; }

    }
}