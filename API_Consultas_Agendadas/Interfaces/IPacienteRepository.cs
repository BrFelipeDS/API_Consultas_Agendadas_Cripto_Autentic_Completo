using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace API_Consultas_Agendadas.Interfaces
{
    public interface IPacienteRepository
    {
        // CRUD

        Paciente Insert(Paciente paciente);

        ICollection<Paciente> GetAll();
        Paciente GetById(int id);

        void Update(Paciente paciente);

        void Delete(Paciente paciente);

        void UpdateParcial(JsonPatchDocument patch, Paciente paciente);
    }
}
