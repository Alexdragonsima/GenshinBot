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
            var character = await GetCharacterAsync(characterName);
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
            sb.AppendLine($"🤺 *{character.CharacterName}*");
            sb.AppendLine($"🏷️ {character.CharacterTitle}");
            sb.AppendLine($"⚡ Элемент: {character.Element?.ElementName}");
            sb.AppendLine($"⚔️ Оружие: {character.WeaponType?.WeaponTypeName}");
            sb.AppendLine($"🗺️ Регион: {character.Region?.RegionName}");
            sb.AppendLine($"⭐ Редкость: {character.Rarity?.Stars}⭐");

            if (!string.IsNullOrEmpty(character.CharacterBirthDate))
                sb.AppendLine($"🎂 День рождения: {character.CharacterBirthDate}");

            if (!string.IsNullOrEmpty(character.CharacterDescription))
                sb.AppendLine($"\n📖 {character.CharacterDescription}");

            return sb.ToString();
        }

        public async Task<string> GetCharacterTeamsInfoAsync(string characterName)
        {
            var teams = await GetCharacterTeamsAsync(characterName);
            if (!teams.Any())
                return $"Для персонажа '{characterName}' не найдено команд.";

            var sb = new StringBuilder();
            sb.AppendLine($"🏆 *Лучшие команды для {characterName}:*");

            foreach ( var team in teams)
            {
                sb.AppendLine($"\n🔹 *{team.TeamName}*");
                sb.AppendLine($"📝 {team.TeamDescription}");
                sb.AppendLine($"🕹️ Стиль игры: {team.TeamPlayStyle}");
                sb.AppendLine($"⚡ Сложность: {new string('⭐', team.TeamDifficulty ?? 1)}");

                // получаем персонажей в команде
                var characters = team.CharacterTeams
                    .OrderBy(ct => ct.CharacterTeamPriority)
                    .Select(ct => $"{ct.Character?.CharacterName} ({ct.RoleInTeam})");

                sb.AppendLine($"👥 Состав: {string.Join(",", characters)}");

                // Получаем ротацию
                var rotations = await _teamRepository.GetTeamRotationsAsync(team.TeamID);
                if (rotations.Any())
                {
                    sb.AppendLine("🔄️ Ротация:");
                    foreach ( var rotation in rotations.OrderBy(r=>r.RotationOrder))
                    {
                        sb.AppendLine($" {rotation.RotationOrder}. {rotation.Character?.CharacterName}: {rotation.ActionDescription}");
                    }
                }
            }

            return sb.ToString();
        }
    }
}
