using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
    public class PalestranteEvento
    {
        public int? PalestranteId { get; set; }
        [ForeignKey("PalestranteId")]

        public virtual Palestrante Palestrante { get; set; }
        public int? EventoId { get; set; }
        [ForeignKey("EventoId")]

        public virtual Evento Evento { get; set; }
    }
}
