using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace API_Consultas_Agendadas.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly Consultas_AgendadasContext ctx;
        public LoginRepository(Consultas_AgendadasContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            //return ctx.Usuarios.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            var usuario = ctx.Usuarios
                                .Include(u => u.IdTipoUsuarioNavigation)
                                .FirstOrDefault(x => x.Email == email);
            
            if (usuario is not null)
            {
                bool confere = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

                if (confere)
                {
                    //Criar as credenciais do JWT
                    
                    // Definimos as Claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),

                        new Claim(ClaimTypes.Role, usuario.IdTipoUsuarioNavigation.Tipo), //Define a Role de Medico, que concede acessos extras

                        new Claim("Acesso", usuario.IdTipoUsuarioNavigation.Tipo)
                    };

                    // Criamos as chaves
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("consultasAgendadas-chave-autenticacao"));

                    // Criamos as credenciais
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Geramos o token
                    var meuToken = new JwtSecurityToken(
                        issuer: "consultasAgendadas.webAPI",
                        audience: "consultasAgendadas.webAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );

                    return new JwtSecurityTokenHandler().WriteToken(meuToken);
                }
            }

            return null;
        }
    }
}
