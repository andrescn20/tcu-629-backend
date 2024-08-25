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

        public async Task<int> DeleteBoardByIdAsync(int boardId)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var board = await _context.Boards
                    .Include(b => b.Sensors)
                    .FirstOrDefaultAsync(s => s.BoardId == boardId);

                if (board == null)
                {
                    throw new Exception("Board not found in DataBase");
                }

                var sensors = board.Sensors;

                if (sensors != null)
                {
                    foreach (var sensor in sensors)
                    {
                        sensor.IsAvailable = true;
                    }
                }

                _context.Boards.Remove(board);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return boardId;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<int> AddBoardAsync(Board board)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
            return board.BoardId;
        }

        public async Task<List<BoardDto>> GetAllBoards()
        {
            var boards = await _context.Boards
            .Select(b => new BoardDto
            {
                BoardId = b.BoardId,
                Description = b.Description,
                IsInstalled = b.IsInstalled,
                Microcontroller = b.BoardSerial,
                BoardSerial = b.BoardSerial,
            }).ToListAsync();


            return boards;
        }

        public async Task<int> GetBoardIdBySerialAsync(string boardSerial)
        {
            var board = await _context.Boards
            .Where(b => b.BoardSerial == boardSerial)
            .Select(b => b.BoardId)
            .FirstOrDefaultAsync();

            return board;
        }

        public async Task<Board> GetBoardByIdAsync(int boardId)
        {
            var board =  await _context.Boards.FindAsync(boardId);
            if (board == null)
            {
                throw new Exception("Board not found");
            }
            return board;

        }

        public async Task UpdateBoardAsync(Board board)
        {
            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

    }
}
