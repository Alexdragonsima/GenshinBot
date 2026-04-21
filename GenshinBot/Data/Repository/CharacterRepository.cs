using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenshinBot.Models.DataBase;
using Microsoft.EntityFrameworkCore;

namespace GenshinBot.Data.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly AppDbContext _context;

        public CharacterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Character> GetCharacterByIdAsync(int id)
        {
            return await _context.Characters
                .Include(c => c.Element)
                .Include(c => c.WeaponType)
                .Include(c => c.Region)
                .Include(c => c.Rarity)
                .FirstOrDefaultAsync(c => c.CharacterID == id);
        }

        public async Task<Character> GetCharacterByNameAsync(string name)
        {
            return await _context.Characters
                .Include (c => c.Element)
                .Include(c => c.WeaponType)
                .Include (c => c.Region)
                .Include(c=>c.Rarity)
                .FirstOrDefaultAsync(c=>c.CharacterName.ToLower().Contains(name.ToLower())||name.ToLower().Contains(c.CharacterName.ToLower()));
        }

        public async Task<List<Character>> GetCharactersByElementAsync(string elementName)
        {
            return await _context.Characters
                .Include(c=>c.Element)
                .Include(c=>c.WeaponType)
                .Include(c=>c.Region)
                .Where(c=>c.Element.ElementName.ToLower()== elementName.ToLower())
                .ToListAsync();
        }

        public async Task<List<Character>> GetCharactersByWeaponAsync(string weaponType)
        {
            return await _context.Characters
                .Include(c=>c.Element)
                .Include(c=>c.WeaponType)
                .Include(c=>c.Region)
                .Where(c=>c.WeaponType.WeaponTypeName.ToLower()==weaponType.ToLower())
                .ToListAsync();
        }

        public async Task<List<Character>> GetCharactersByRegionAsync(string regionName)
        {
            return await _context.Characters
                .Include(c=>c.Element)
                .Include(c=>c.WeaponType)
                .Include(c=>c.Region)
                .Where(c=>c.Region.RegionName.ToLower()==regionName.ToLower())
                .ToListAsync();
        }

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            return await _context.Characters
                .Include(c=>c.Element)
                .Include(c=>c.WeaponType)
                .Include(c=>c.Region)
                .Include(c=>c.Rarity)
                .OrderBy(c=>c.CharacterName)
                .ToListAsync();
        }

        public async Task<List<Team>> GetCharacterBestTeamsAsync(int characterId)
        {
            return await _context.Teams
                .Include(t=>t.CharacterTeams)
                    .ThenInclude(ct=>ct.Character)
                .Include(t=>t.Rotations)
                    .ThenInclude(r=>r.Character)
                .Where(t=>t.CharacterTeams.Any(ct=>ct.CharacterID==characterId))
                .ToListAsync();
        }

        public async Task<List<CharacterArtifact>> GetCharacterArtifactsAsync(int characterId)
        {
            return await _context.CharacterArtifacts
                .Include(ca=>ca.Artifact)
                .Where(ca=>ca.CharacterID== characterId)
                .OrderBy(ca=>ca.CharacterArtifactPriority)
                .ToListAsync();
        }

        public async Task<List<Character>>SearchCharactersAsync(string searchTeam)
        {
            return await _context.Characters
                .Include(c=>c.Element)
                .Include(c=>c.WeaponType)
                .Include(c=>c.Region)
                .Where(c=>c.CharacterName.ToLower().Contains(searchTeam.ToLower())||
                c.CharacterTitle.ToLower().Contains(searchTeam.ToLower())||
                c.CharacterDescription.ToLower().Contains(searchTeam.ToLower()))
                .ToListAsync();
        }
    }
}