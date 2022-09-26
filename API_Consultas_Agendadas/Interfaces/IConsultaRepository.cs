using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace API_Consultas_Agendadas.Interfaces
{
    public interface IConsultaRepository
    {
        // CRUD

        Consulta Insert(Consulta consulta);

        ICollection<Consulta> GetAll();
        Consulta GetById(int id);

        void Update(Consulta consulta);

        void Delete(Consulta consulta);

        void UpdateParcial(JsonPatchDocument patch, Consulta consulta);
    }
}
