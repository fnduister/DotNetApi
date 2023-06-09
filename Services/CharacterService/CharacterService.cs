using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        public readonly IMapper _mapper;
        public readonly DataContext _dbContext;
        public DataContext DbContext { get; }

        public CharacterService(IMapper mapper, DataContext DbContext)
        {
            _dbContext = DbContext;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var character = _mapper.Map<Character>(newCharacter);

            await _dbContext.Characters.AddAsync(character);

            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = _dbContext.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacter()
        {
            var characters = await _dbContext.Characters.ToArrayAsync();
            return new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList()
            };
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int Id)
        {
            var characters = await _dbContext.Characters.ToArrayAsync();
            var FoundCharacter = characters.FirstOrDefault(c => c.Id == Id);

            if (FoundCharacter == null)
            {
                throw new Exception("This is does not exist");
            }

            return new ServiceResponse<GetCharacterDTO>
            {
                Data = _mapper.Map<GetCharacterDTO>(FoundCharacter)
            }; ;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> RemoveCharacterById(int Id)
        {

            var CharacterToDelete = _dbContext.Characters.FirstOrDefault(c => c.Id == Id);

            if (CharacterToDelete == null)
            {
                return new ServiceResponse<List<GetCharacterDTO>>
                {
                    Data = _dbContext.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList(),
                    Success = false,
                    Message = "Character was not found"
                };
            }

            _dbContext.Characters.Remove(CharacterToDelete);

            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<List<GetCharacterDTO>>
            {
                Data = _dbContext.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList(),
                Success = true
            };
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var CharacterToUpdate = _dbContext.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

            if (CharacterToUpdate == null)
            {
                return new ServiceResponse<GetCharacterDTO>
                {
                    Data = null,
                    Success = false,
                    Message = $"Character with id: '{updatedCharacter.Id}' was not found"
                };
            }

            CharacterToUpdate.Name = updatedCharacter.Name;

            await _dbContext.SaveChangesAsync();

            return new ServiceResponse<GetCharacterDTO>
            {
                Data = _mapper.Map<GetCharacterDTO>(CharacterToUpdate),
                Success = true
            };
        }
    }
}