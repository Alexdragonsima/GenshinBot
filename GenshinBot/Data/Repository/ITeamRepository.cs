using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinBot.Models.DataBase;

namespace GenshinBot.Data.Repository
{
    public interface ITeamRepository
    {
        Task<Team> GetTeamByIdAsync(int id);
        Task<List<Team>> GetTeamsByCharacterAsync(int characterId);
        Task<List<Team>> GetTeamsByPlayStyleAsync(string playStyle);
        Task<List<TeamRotation>> GetTeamRotationsAsync(int teamId);
    }
}
