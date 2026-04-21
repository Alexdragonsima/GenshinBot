using GenshinBot.Models.DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBot.Data.Repository
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;
        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Team> GetTeamByIdAsync(int id)
        {
            return await _context.Teams
                .Include(t => t.CharacterTeams)
                    .ThenInclude(ct => ct.Character)
                    .ThenInclude(c => c.Element)
                .Include(t => t.Rotations)
                    .ThenInclude(r => r.Character)
                .FirstOrDefaultAsync(t => t.TeamID == id);
        }

        public async Task<List<Team>> GetTeamsByCharacterAsync(int characterId)
        {
            return await _context.Teams
                .Include(t => t.CharacterTeams)
                    .ThenInclude(ct => ct.Character)
                .Include(t => t.Rotations)
                .Where(t => t.CharacterTeams.Any(ct => ct.CharacterID == characterId))
                .ToListAsync();
        }

        public async Task<List<Team>> GetTeamsByPlayStyleAsync(string playStyle)
        {
            return await _context.Teams
                .Include(t => t.CharacterTeams)
                    .ThenInclude(ct => ct.Character)
                .Where(t => t.TeamPlayStyle.ToLower() == playStyle.ToLower())
                .ToListAsync();
        }

        public async Task<List<TeamRotation>> GetTeamRotationsAsync(int teamId)
        {
            return await _context.TeamRotations
                .Include(r => r.Character)
                .Where(r => r.TeamID == teamId)
                .OrderBy(r => r.RotationOrder)
                .ToListAsync();
        }
    }
}
