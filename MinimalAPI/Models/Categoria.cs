using System.Text.Json.Serialization;

namespace MinimalAPI.Models
{
    public class Categoria
    {

        public int CategoriaId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }

        //duvida, o que e?
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
