using AutoMapper;
using MyPasswords.Models;
using MyPasswords.Models.Entities;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security;
using MyPasswords.Security.Interfaces;

namespace MyPasswords.Services.User.Register
{
    public class RegisterUserServices : IRegisterUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IMapper _mapper;

        public RegisterUserServices(IUserRepository userRepository, IPasswordHashService passwordHashService, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _mapper = mapper;
        }

        public async Task<bool> Register(RegisterViewModel model)
        {
            if (await _userRepository.ExistUserWithEmail(model.Email))
                return false;

            var user = _mapper.Map<Models.Entities.User>(model);
            user.Password = _passwordHashService.HashPassword(model.Password);


            await _userRepository.Add(user);
            await _userRepository.Save();
            return true;
        }
    }
}
