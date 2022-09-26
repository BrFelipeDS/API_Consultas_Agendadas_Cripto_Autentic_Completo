using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;

namespace API_Consultas_Agendadas.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        // CRUD

        TipoUsuario Insert(TipoUsuario tipoUsuario);

        ICollection<TipoUsuario> GetAll();
        TipoUsuario GetById(int id);

        void Update(TipoUsuario tipoUsuario);

        void Delete(TipoUsuario tipoUsuario);

        void UpdateParcial(JsonPatchDocument patch, TipoUsuario tipoUsuario);
    }
}
