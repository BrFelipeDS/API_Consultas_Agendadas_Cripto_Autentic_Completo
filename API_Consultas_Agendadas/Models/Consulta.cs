using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace API_Consultas_Agendadas.Models
{
    public partial class Consulta
    {
        
        public int Id { get; set; }

        [Required]
        public DateTime? DataHora { get; set; }
        public int? IdPaciente { get; set; }
        public int? IdMedico { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] //Uso do JsonIgnoreCondition para evitar mostrar conteúdo desnecessário no Swagger
        public virtual Medico IdMedicoNavigation { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual Paciente IdPacienteNavigation { get; set; }
    }
}
