using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Front.Models
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        [DisplayName("Nome: ")]
        public string Nome { get; set; }
        [DisplayName("E-mail: ")]
        public string Email { get; set; }
        [DisplayName("Logotipo: ")]
        public string Logotipo { get; set; }

        //[JsonIgnore]
        //public List<Logradouro> Logradouros { get; set; }
    }
}
