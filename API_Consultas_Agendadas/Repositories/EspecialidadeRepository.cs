using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Consultas_Agendadas.Repositories
{
    public class EspecialidadeRepository : IEspecialidadeRepository
    {
        Consultas_AgendadasContext ctx;

        public EspecialidadeRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }


        public void Delete(Especialidade especialidade)
        {
            ctx.Especialidades.Remove(especialidade);
            ctx.SaveChanges();
        }

        public ICollection<Especialidade> GetAll()
        {
            var especialidade = ctx.Especialidades
                .Include(m => m.Medicos)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .ToList();
            return especialidade;
        }

        public Especialidade GetById(int id)
        {
            var especialidade = ctx.Especialidades
                .Include(m => m.Medicos)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .FirstOrDefault(m => m.Id == id);

            return especialidade;
        }

        public Especialidade Insert(Especialidade especialidade)
        {
            ctx.Especialidades.Add(especialidade);
            ctx.SaveChanges();
            return especialidade;
        }

        public void Update(Especialidade especialidade)
        {
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Especialidade especialidade)
        {
            patch.ApplyTo(especialidade);
            ctx.Entry(especialidade).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
