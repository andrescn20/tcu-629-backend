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
