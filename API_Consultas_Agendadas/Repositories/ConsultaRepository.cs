using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_Consultas_Agendadas.Repositories
{
    public class ConsultaRepository : IConsultaRepository
    {
        //Criação do contexto para acesso ao banco de dados com o Entity Framework
        Consultas_AgendadasContext ctx;

        public ConsultaRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }

        public void Delete(Consulta consulta)
        {
            ctx.Consulta.Remove(consulta);
            ctx.SaveChanges();
        }

        public ICollection<Consulta> GetAll()
        {
            //Utilizando o Include, é possível exibir no Get as informações referentes aos atributos das entidades que são Foreign Keys na entidade atual
            //Utilizando o ThenInclude, é possível exibir no get as informações referentes aos atributos das entidades que são Foreign Keys em uma Foreign Key da entidade atual
            var consultas = ctx.Consulta
                .Include(p => p.IdPacienteNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(m => m.IdMedicoNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .ToList();
            return consultas;
        }

        public Consulta GetById(int id)
        {
            var consulta = ctx.Consulta
                .Include(p => p.IdPacienteNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .Include(m => m.IdMedicoNavigation)
                .ThenInclude(u => u.IdUsuarioNavigation)
                .FirstOrDefault(m => m.Id == id); //FirtsOrDefault exibe a primeiro ocorrência ou a padrão que obedece à condição da expressão Lambda

            return consulta;
        }

        public Consulta Insert(Consulta consulta)
        {
            ctx.Consulta.Add(consulta);
            ctx.SaveChanges();
            return consulta;
        }

        public void Update(Consulta consulta)
        {
            ctx.Entry(consulta).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void UpdateParcial(JsonPatchDocument patch, Consulta consulta)
        {
            patch.ApplyTo(consulta);
            ctx.Entry(consulta).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }
}
