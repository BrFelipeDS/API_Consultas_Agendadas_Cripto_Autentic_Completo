
namespace API_Consultas_Agendadas.Interfaces
{
    public interface ILoginRepository
    {
        string LogarPaciente(string email, string senha);

        string LogarMedico(string email, string senha);
    }
}
