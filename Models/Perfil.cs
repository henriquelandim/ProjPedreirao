using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPedreirao.Models
{
    public class Perfil
    {
         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string DescDPerfil { get; set; } //descriição de perfil
        
        [StringLength(255)]
        public string FotoPerfil { get; set; }
        [StringLength(255)]
        public string FotoPort1 { get; set; }
        [StringLength(255)]
        public string FotoPort2 { get; set; }
        [StringLength(255)]
        public string FotoPort3 { get; set; }
        [StringLength(255)]
        public string FotoPort4 { get; set; }
        [StringLength(255)]
        public string FotoPort5 { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}