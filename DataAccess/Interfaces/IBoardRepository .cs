using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IBoardRepository
    {
        Task DeleteBoardByIdAsync(int boardId);
        Task<int> AddBoardAsync(Board board);
        Task<int> GetBoardIdBySerialAsync(string boardSerial);
        Task<List<BoardDto>> GetAllBoards();
        Task<Board> GetBoardByIdAsync(int boardId);
        Task UpdateBoardAsync(Board board);
    }
}
