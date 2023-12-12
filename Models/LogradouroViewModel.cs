using System.ComponentModel;

namespace Front.Models
{
    public class LogradouroViewModel
    {
        public int Id { get; set; }
        [DisplayName("Endereço: ")]
        public string Endereco { get; set; }
        [DisplayName("Cidade: ")]
        public string Cidade { get; set; }
        [DisplayName("Estado: ")]
        public string Estado { get; set; }
        [DisplayName("CEP: ")]
        public string Cep { get; set; }
    }
}
