using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using API.Services;
using DTO;

namespace API.Controllers
{
    [ApiController]
    [Route("/[controller]/[action]")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardDataService)
        {
            _boardService = boardDataService;
        }

        [HttpGet]
        public Task<List<BoardDto>> GetAllBoards()
        {
            return _boardService.GetAllBoards();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBoardById(int boardId)
        {

            await _boardService.DeleteBoardById(boardId);

            return Ok($"Board with id: {boardId} deleted");
        }

        [HttpGet]
        public string TestBoardsController()
        {
            return "Boards Controller is Working";
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewBoard(BoardDto board)
        {
            await _boardService.AddBoard(board);
            return Ok();
        }
    }
}
