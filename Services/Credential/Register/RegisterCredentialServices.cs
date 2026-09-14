using AutoMapper;
using MyPasswords.Models;
using MyPasswords.Repositories.Interfaces;
using MyPasswords.Security.Interfaces;
using MyPasswords.Services.ObtainUserLogged;

namespace MyPasswords.Services.Credential.Register
{
    public class RegisterCredentialServices : IRegisterCredentialServices
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;
        private readonly ICredentialRepository _credentialRepository;

        public RegisterCredentialServices(ILoggedUser loggedUser, IMapper mapper, IEncryptionService encryptionService, ICredentialRepository credentialRepository)
        {
            _loggedUser = loggedUser;
            _mapper = mapper;
            _encryptionService = encryptionService;
            _credentialRepository = credentialRepository;
        }

        public async Task<bool> Register(RegisterCredentialViewModel model)
        {
            var userId = _loggedUser.Id;

            var credential = _mapper.Map<Models.Entities.Credential>(model);
            credential.UserId = userId;
            credential.UserName = _encryptionService.Encrypt(model.UserName);
            credential.Password = _encryptionService.Encrypt(model.Password);

            await _credentialRepository.Add(credential);
            await _credentialRepository.Save();
            return true;
        }
    }
}
