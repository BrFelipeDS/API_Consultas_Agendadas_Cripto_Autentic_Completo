using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace API_Consultas_Agendadas.Models
{
    public partial class Medico
    {
        public Medico()
        {
            Consulta = new HashSet<Consulta>();
        }

        
        public int Id { get; set; }

        [Required]
        public string Crm { get; set; }

        public int? IdUsuario { get; set; }

        public int? IdEspecialidade { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual Especialidade IdEspecialidadeNavigation { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<Consulta> Consulta { get; set; }
    }
}
