using ArmorFeedApi.Shared.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ArmorFeedApi.Shared.Services
{
    public class SequenceService
    {
        private readonly AppDbContext _context;
        public SequenceService(AppDbContext context)
        {
            _context = context;
        }
        public int IncrementId()
        {

            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.CommandText = @"
                UPDATE armorfeed.sequence
                SET valor = valor + 1
                WHERE nombre = 'mi_secuencia';
            
                SELECT valor
                FROM armorfeed.sequence
                WHERE nombre = 'mi_secuencia';
            ";

            _context.Database.OpenConnection();

            using var reader = command.ExecuteReader();

            int nuevoValor = 0;

            if (reader.Read())
            {
                nuevoValor = reader.GetInt32(0);
            }

            _context.Database.CloseConnection();

            return nuevoValor;

        }
    }
}
