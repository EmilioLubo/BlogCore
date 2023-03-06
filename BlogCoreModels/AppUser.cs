using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCoreModels
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "Ingrese un Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese una Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Ingrese una Ciudad")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Ingrese un País")]
        public string Pais { get; set; }

    }
}
