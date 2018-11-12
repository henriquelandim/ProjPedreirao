using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPedreirao.Models
{
    public class Servico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float AvaliC { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float AValiP { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public float Pontos { get; set; }
        public int IdUsuarioC { get; set; }
        public int IdUsuarioP { get; set; }
        public int IdFechaNegocio { get; set; }
        public ICollection<FechaNegocio> FechaNegocio { get; set; }
        public Usuario Usuario { get; internal set; }
    }
}