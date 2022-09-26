using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace API_Consultas_Agendadas.Interfaces
{
    public interface IMedicoRepository
    {
        // CRUD

        Medico Insert(Medico medico);

        ICollection<Medico> GetAll();
        Medico GetById(int id);

        void Update(Medico medico);

        void Delete(Medico medico);

        void UpdateParcial(JsonPatchDocument patch, Medico medico);
    }
}
