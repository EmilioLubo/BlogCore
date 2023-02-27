using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreModels
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para el Artículo")]
        [Display(Name = "Nombre Artículo")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para el Artículo")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha de creación")]
        public string FechaCreacion { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imágen")]
        public string UrlImagen { get; set; }
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }
    }
}
