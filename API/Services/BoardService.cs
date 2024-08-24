using API.Interfaces;
using DataAccess.Models;
using DataAccess.Repositories;
using DTO;

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
        public async Task<List<BoardDto>> GetAllBoards()
        {
            return await _boardRepository.GetAllBoards();

        }

        public async Task AddBoard(BoardDto board)
        {
            var boardModel = new Board
            {
                BoardSerial = board.BoardSerial,
                Description = board.Description,
                IsInstalled = false,
                Microcontroller = board.Microcontroller,

            };
            await _boardRepository.AddBoardAsync(boardModel);
        }

    }

}
