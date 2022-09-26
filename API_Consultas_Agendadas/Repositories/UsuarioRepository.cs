using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Consultas_Agendadas.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        // Injeção de Dependência

        Consultas_AgendadasContext ctx;

        public UsuarioRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Usuario usuario)
        {
            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        public ICollection<Usuario> GetAll()
        {
            return ctx.Usuarios.ToList();
        }

        public Usuario GetById(int id)
        {
            return ctx.Usuarios.Find(id);
        }

        public Usuario Insert(Usuario usuario)
        {
            ctx.Usuarios.Add(usuario);
            ctx.SaveChanges();
            return usuario;
        }

        public void Update(Usuario usuario)
        {
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Usuario usuario)
        {
            patch.ApplyTo(usuario);
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
