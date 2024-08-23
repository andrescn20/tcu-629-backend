using API.Interfaces;
using DataAccess.Repositories;

namespace API.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }


        public async Task DeleteBoardById(int boardId)
        {
            await _boardRepository.DeleteBoardByIdAsync(boardId);
        }
    }

}
