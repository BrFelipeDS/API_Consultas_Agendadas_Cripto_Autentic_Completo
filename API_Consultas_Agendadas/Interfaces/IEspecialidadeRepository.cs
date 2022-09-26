using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace API_Consultas_Agendadas.Interfaces
{
    public interface IEspecialidadeRepository
    {
        // CRUD

        Especialidade Insert(Especialidade especialidade);

        ICollection<Especialidade> GetAll();
        Especialidade GetById(int id);

        void Update(Especialidade especialidade);

        void Delete(Especialidade especialidade);

        void UpdateParcial(JsonPatchDocument patch, Especialidade especialidade);
    }
}
