using API_Consultas_Agendadas.Data;
using API_Consultas_Agendadas.Interfaces;
using API_Consultas_Agendadas.Models;
using Microsoft.Data.SqlClient.Server;
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

        public string LogarPaciente(string email, string senha)
        {
            //return ctx.Usuarios.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            var usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == email);

            if(usuario.IdTipoUsuario == 2)
            {
                return null;
            }

            if(usuario is not null)
            {
                bool confere = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha); // Geração de Hash para criptografia de senha

                if (confere)
                {
                    //Criar as credenciais do JWT

                    // Definimos as Claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Paciente"), //Define a Role Paciente, que não possui tantos acessos

                        new Claim("Acesso", "Paciente")
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

        public string LogarMedico(string email, string senha)
        {
            //return ctx.Usuarios.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            var usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == email);

            if (usuario.IdTipoUsuario == 1)
            {
                return null;
            }

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
                        new Claim(ClaimTypes.Role, "Medico"), //Define a Role de Medico, que concede acessos extras

                        new Claim("Acesso", "Medico")
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
