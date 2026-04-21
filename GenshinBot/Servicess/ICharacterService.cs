using GenshinBot.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBot.Servicess
{
    public interface ICharacterService
    {
        Task<Character> GetCharacterAsync(string name);
        Task<List<Character>> GetAllCharactersAsync();
        Task<List<Team>> GetCharacterTeamsAsync(string characterName);
        Task<string> GetCharacterInfoAsync(string characterName);
        Task<string> GetCharacterTeamsInfoAsync(string characterName);
    }
}
