using System.ComponentModel.DataAnnotations.Schema;

namespace Alma.Domain.Entities
{
    [Table("historias_destaque")]
    public class Historias
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("titulo")]
        public string Titulo { get; set; }

        [Column("conteudo")]
        public string Conteudo { get; set; }

        [Column("criado_em")]
        public DateTime CriadoEm { get; set; }
    }
}
