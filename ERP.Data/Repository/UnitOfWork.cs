using ERP.Core.Interfaces.Repositories;
using ERP.Data.Data;
using Microsoft.Extensions.Logging;

namespace ERP.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context ;
        private readonly ILogger _logger;

       // public IOrderRepository Orders { get; private set; }


        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
        }


        public async Task<int> CompleteAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during SaveChangesAsync in UnitOfWork");
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Dispose in UnitOfWork");
                throw;
            }
        }
    }
}