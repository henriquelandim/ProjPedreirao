using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPedreirao.Models
{
    public class FechaNegocio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Você precisa inserir o nome de usuario")]
        public string Categoria { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "Você precisa inserir o nome de usuario")]
        public string DescServ { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Preco { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DefaultValue("getDate()")]
        public DateTime DataFechamento { get; set; }
        public int IdFormaDPag { get; set; }
        public int IdUsuarioC { get; set; }
        public int IdUsuarioP { get; set; }
        public Usuario Usuario { get; internal set; }
        public FormaPagamento FormaPagamento { get; internal set; }
    }
}