using System.ComponentModel.DataAnnotations;

namespace P03_webApi.Models
{
    public class marcas
    {
        [Key]
        public int id_marca { get; set; }
        public string nombre_marca { get; set; }
        public string estados { get; set; }
    }
}
