using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ApiPedreirao.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Você precisa inserir o nome de usuario")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "Senha Obrigatória!")]
        [StringLength(255)]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Você precisa inserir o nome Completo")]
        [StringLength(100)]
        public string NomeCompleto { get; set; }
        [StringLength(100)]
        public string NomeSocial { get; set; }

        [Required(ErrorMessage = "Você precisa inserir o Email")]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "É preciso inserir o tipo de usuario, 'C' para cliente 'P' para prestador")]
        [StringLength(1)]
        public string TipoUsuario { get; set; }
        [Required(ErrorMessage = "É preciso inserir o sexo,'M' para masculino, 'F' para feminino, 'O' para prefiro não informar")]
        [StringLength(1)]
        public char Sexo { get; set; }
        [StringLength(15)]
        public string Telefone { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [DefaultValue("getDate()")]
        public DateTime DataCadastro {get; set;} //= DateTime.UtcNow;
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "CPF é requerido.")]
        public string CPF { get; set; }
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CPF é requerido.")]
        public string CNPJ { get; set; }
         [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; } 
        [Required(ErrorMessage = "Você precisa inserir o endereço")]
        [StringLength(120)]
        public string Endereco { get; set; }
        [StringLength(100, ErrorMessage = "Você precisa inserir a razão social da empresa")]
        public string RazaoSocial { get; set; }
        [StringLength(100, ErrorMessage = "Você precisa inserir a especialidade profissional")]
        public string Especialidade { get; set; }
        [StringLength(100, ErrorMessage = "Você precisa inserir a especialidade profissional")]
        public string Especialidade2 { get; set; }
        [StringLength(100, ErrorMessage = "Você precisa inserir a respecialidade profissional")]
        public string Especialidade3 { get; set; }
        public ICollection<FechaNegocio> FechaNegocio { get; set; }
        public ICollection<Servico> Servico { get; set; }


        public Perfil Perfil { get;  set; }

        
    }
}