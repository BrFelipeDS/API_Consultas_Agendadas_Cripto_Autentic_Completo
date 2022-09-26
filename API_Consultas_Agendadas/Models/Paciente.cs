using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace API_Consultas_Agendadas.Models
{
    public partial class Paciente
    {
        public Paciente()
        {
            Consulta = new HashSet<Consulta>();
        }

        public int Id { get; set; }

        [Required]
        public string Carteirinha { get; set; }

        [Required]
        public DateTime? DataNascimento { get; set; }

        [Required]
        public bool? Ativo { get; set; }

        
        public int? IdUsuario { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
