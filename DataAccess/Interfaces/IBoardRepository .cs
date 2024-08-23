using DataAccess.Interfaces;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IBoardRepository 
    {
        Task DeleteBoardByIdAsync(int boardId);
    }
}
