using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Consultas_Agendadas.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        Consultas_AgendadasContext ctx;

        public TipoUsuarioRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuarios.Remove(tipoUsuario);
            ctx.SaveChanges();
        }

        public ICollection<TipoUsuario> GetAll()
        {
            return ctx.TipoUsuarios.ToList();
        }

        public TipoUsuario GetById(int id)
        {
            return ctx.TipoUsuarios.Find(id);
        }

        public TipoUsuario Insert(TipoUsuario tipoUsuario)
        {
            ctx.TipoUsuarios.Add(tipoUsuario);
            ctx.SaveChanges();
            return tipoUsuario;
        }

        public void Update(TipoUsuario tipoUsuario)
        {
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, TipoUsuario tipoUsuario)
        {
            patch.ApplyTo(tipoUsuario);
            ctx.Entry(tipoUsuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
