using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class BoardRepository : IBoardRepository
    {
        private readonly MonitoringDbContext _context;

        public BoardRepository(MonitoringDbContext context)
        {
            _context = context;
        }

        public async Task DeleteBoardByIdAsync(int boardId)
        {
            var board = new Board { BoardId = boardId};
            _context.Boards.Attach(board);
            _context.Boards.Remove(board);
            await _context.SaveChangesAsync();
        }

    }
}
