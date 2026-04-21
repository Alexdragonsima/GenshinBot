using GenshinBot.Data.Repository;
using GenshinBot.Models.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinBot.Services
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
    }
}
