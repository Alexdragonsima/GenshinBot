using GenshinBot.Data.Repository;
using GenshinBot.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace GenshinBot.Servicess
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ITeamRepository _teamRepository;

        public CharacterService(ICharacterRepository characterRepository, ITeamRepository teamRepository)
        {
            _characterRepository = characterRepository;
            _teamRepository = teamRepository;
        }

        public async Task<Character> GetCharacterAsync(string name)
        {
            return await _characterRepository.GetCharacterByNameAsync(name);
        }

        public async Task<List<Character>> GetAllCharactersAsync()
        {
            return await _characterRepository.GetAllCharactersAsync();
        }

        public async Task<List<Team>> GetCharacterTeamsAsync(string characterName)
        {
            var character = await GetCharacterTeamsAsync(characterName);
            if(character == null)
                return new List<Team>();

            return await _teamRepository.GetTeamsByCharacterAsync(character.CharacterID);
        }

        public async Task<string> GetCharacterInfoAsync(string characterName)
        {
            var character = await GetCharacterAsync(characterName);
            if (character == null)
                return $"Персонаж '{characterName}' не найден.";

            var sb = new StringBuilder();
            sb.AppendLine($" *{characterName}*");
            sb.AppendLine($" *{character.CharacterTitle}*");
            sb.AppendLine($" *{characterName}*");
            sb.AppendLine($" *{characterName}*");
            sb.AppendLine($" *{characterName}*");
            sb.AppendLine($" *{characterName}*");
        }
    }
}
