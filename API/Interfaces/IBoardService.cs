using DataAccess.Models;
using DTO;

namespace API.Interfaces
{
    public interface IBoardService
    {
        Task DeleteBoardById(int boardId);
        Task<List<BoardDto>> GetAllBoards();
        Task AddBoard(BoardDto board);

    }
}
