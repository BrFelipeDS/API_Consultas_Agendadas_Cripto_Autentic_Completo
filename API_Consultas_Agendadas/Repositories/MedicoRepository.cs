using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Consultas_Agendadas.Repositories
{
    public class MedicoRepository : IMedicoRepository
    {
        Consultas_AgendadasContext ctx;

        public MedicoRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Medico medico)
        {
            ctx.Medicos.Remove(medico);
            var usuario = ctx.Usuarios.Find(medico.IdUsuario);
            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        public ICollection<Medico> GetAll()
        {
               var medicos = ctx.Medicos
                .Include(e => e.IdEspecialidadeNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .Include(c => c.Consulta)
                .ToList();
            return medicos;
        }

        public Medico GetById(int id)
        {
            var medico = ctx.Medicos
                .Include(e => e.IdEspecialidadeNavigation)
                .Include(u => u.IdUsuarioNavigation)
                .Include(c => c.Consulta)
                .FirstOrDefault(m => m.Id == id);

            return medico;
        }

        public Medico Insert(Medico medico)
        {
            ctx.Medicos.Add(medico);
            ctx.SaveChanges();
            return medico;
        }

        public void Update(Medico medico)
        {
            ctx.Entry(medico).State = EntityState.Modified;
            var usuario = medico.IdUsuarioNavigation;
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Medico medico)
        {
            patch.ApplyTo(medico);
            ctx.Entry(medico).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
