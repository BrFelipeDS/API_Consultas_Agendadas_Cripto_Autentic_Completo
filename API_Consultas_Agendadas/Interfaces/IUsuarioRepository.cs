using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections;
using System.Collections.Generic;

namespace API_Consultas_Agendadas.Interfaces
{
    public interface IUsuarioRepository
    {
        // CRUD

        Usuario Insert(Usuario usuario);

        ICollection<Usuario> GetAll();
        Usuario GetById(int id);

        void Update(Usuario usuario);

        void Delete(Usuario usuario);

        void UpdateParcial(JsonPatchDocument patch, Usuario usuario);

    }
}
