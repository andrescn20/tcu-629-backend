using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using API.Interfaces;
using Microsoft.AspNetCore.Cors;
using API.Services;

namespace API.Controllers
{
    [EnableCors("TCU_Cors")]
    [ApiController]
    [Route("/[controller]/[action]")]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        public BoardController(IBoardService boardDataService)
        {
            _boardService = boardDataService;
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
    }
}
