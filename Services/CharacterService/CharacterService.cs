using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg_vs.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Sam" }
        };

        // field for dependency injection
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        // constructor for dependency injection
        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            try
            {
                var character = _mapper.Map<Character>(newCharacter);

                _context.Characters.Add(character);
                await _context.SaveChangesAsync();

                // Select() returns IEnumerable, hence need for conversion
                return new ServiceResponse<List<GetCharacterDto>>()
                {
                    Success = true,
                    Message = "character added successfully!"
                };
            }
            catch (System.Exception ex)
            {
                return new ServiceResponse<List<GetCharacterDto>>()
                {
                    Success = false,
                    Message = $"something went wrong while trying to add a new character. message: {ex.Message}"
                };
            }
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try 
            {
                var dbCharacter = await _context.Characters.FindAsync(updatedCharacter.Id);
                if (dbCharacter is null)
                {
                    return new ServiceResponse<GetCharacterDto>()
                    {
                        Success = false,
                        Message = "Character Id not found."
                    };
                }

                dbCharacter.Name = updatedCharacter.Name;
                dbCharacter.HitPoints = updatedCharacter.HitPoints;
                dbCharacter.Strength = updatedCharacter.Strength;
                dbCharacter.Defence = updatedCharacter.Defence;
                dbCharacter.Intelligence = updatedCharacter.Intelligence;
                dbCharacter.Class = updatedCharacter.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

                _context.Characters.Update(dbCharacter);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ServiceResponse<GetCharacterDto>()
                {
                        Success = false,
                        Message = $"Character Id not found.\nMessage: {ex.Message}"
                };
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            try 
            {
                // find character with id
                var dbCharacter = await _context.Characters.FindAsync(id);
                if (dbCharacter is null)
                {
                    return new ServiceResponse<List<GetCharacterDto>>()
                    {
                        Success = false,
                        Message = "Character Id not found."
                    };
                }
                // remove character if found
                _context.Characters.Remove(dbCharacter);
                await _context.SaveChangesAsync();
                // return response to show that the character was removed
                return new ServiceResponse<List<GetCharacterDto>>()
                {
                    Success = true,
                    Message = "Character removed successfully!"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<GetCharacterDto>>()
                {
                        Success = false,
                        Message = $"Character not found.\nMessage: {ex.Message}"
                };
            }
        }
    }
}