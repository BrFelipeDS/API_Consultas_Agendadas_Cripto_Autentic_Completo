using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Consultas_Agendadas.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        Consultas_AgendadasContext ctx;

        public PacienteRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Paciente paciente)
        {
            ctx.Pacientes.Remove(paciente);
            var usuario = ctx.Usuarios.Find(paciente.IdUsuario);
            ctx.Usuarios.Remove(usuario);
            ctx.SaveChanges();
        }

        public ICollection<Paciente> GetAll()
        {
            var pacientes = ctx.Pacientes
                .Include(u => u.IdUsuarioNavigation)
                .Include(c => c.Consulta)
                .ToList();
            return pacientes;
        }

        public Paciente GetById(int id)
        {
            var paciente = ctx.Pacientes
                .Include(u => u.IdUsuarioNavigation)
                .Include(c => c.Consulta)
                .FirstOrDefault(p => p.Id == id);

            return paciente;
        }

        public Paciente Insert(Paciente paciente)
        {
            ctx.Pacientes.Add(paciente);
            ctx.SaveChanges();
            return paciente;
        }

        public void Update(Paciente paciente)
        {
            ctx.Entry(paciente).State = EntityState.Modified;
            var usuario = paciente.IdUsuarioNavigation;
            ctx.Entry(usuario).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Paciente paciente)
        {
            patch.ApplyTo(paciente);
            ctx.Entry(paciente).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
