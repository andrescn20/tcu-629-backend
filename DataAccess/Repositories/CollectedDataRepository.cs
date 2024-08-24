using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class CollectedDataRepository : ICollectedDataRepository
    {
        private readonly MonitoringDbContext _context;

        public async Task CreateBoardAsync(Board board)
        {
            await _context.Boards.AddAsync(board);
        }
        public CollectedDataRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task<TemperatureData> SaveTemperatureSensorDataAsync(TemperatureData temperatureData)
        {
            _context.TemperatureData.Add(temperatureData);
            await _context.SaveChangesAsync();
            return temperatureData;
        }

    }
}
