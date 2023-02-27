using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlogCoreModels
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre para el Slider")]
        [Display(Name = "Nombre Slider")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imágen")]
        public string UrlImagen { get; set; }
    }
}
