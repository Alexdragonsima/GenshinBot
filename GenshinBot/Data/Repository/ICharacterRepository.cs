using GenshinBot.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBot.Data.Repository
{
    public interface ICharacterRepository
    {
        Task<Character> GetCharacterByIdAsync(int id);
        Task<Character> GetCharacterByNameAsync(string name);
        Task<List<Character>> GetCharactersByElementAsync(string elementName);
        Task<List<Character>> GetCharactersByWeaponAsync(string weaponType);
        Task<List<Character>> GetCharactersByRegionAsync(string regionName);
        Task<List<Character>> GetAllCharactersAsync();
        Task<List<Team>> GetCharacterBestTeamsAsync(int characterId);
        Task<List<CharacterArtifact>> GetCharacterArtifactsAsync(int characterId);
        Task<List<Character>> SearchCharactersAsync(string searchTeam);
    }
}
