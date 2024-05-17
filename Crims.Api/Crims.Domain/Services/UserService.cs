using AutoMapper;
using Crims.Core.Helpers;
using Crims.Data.Entities;
using Crims.Data.Repository;
using Crims.Data.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Crims.Core.Failures;

namespace Crims.Domain.Services
{
    public interface IUserService
    {
        Task<RegisterDto> Register(RegisterDto registerDto);
        Task<UserDto> Login(LoginDto loginDto);
    }
    internal class UserService(UserRepository userRepository, IMapper mapper, IPasswordHelper passwordHelper, UserRoleRepository roleRepository) : IUserService
    {
        private readonly UserRepository userRepository = userRepository;
        private readonly IMapper mapper = mapper;
        private readonly IPasswordHelper passwordHelper = passwordHelper;
        private readonly UserRoleRepository roleRepository = roleRepository;

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var account = await userRepository.GetItem(where => where.Email == loginDto.Email) ?? throw new NotFoundFailure("Email or Password incorrect");
            if (passwordHelper.VerifyPassword(loginDto.Password, account.Password) == false)
            {
                throw new NotFoundFailure("Email or Password incorrect");
            }
            var map = mapper.Map<UserDto>(account);
            map.UserRole = await roleRepository.GetItem(where => where.Id == account.UserRoleId);
            return map;
        }

        public async Task<RegisterDto> Register(RegisterDto registerDto)
        {
            var email = await userRepository.GetItem(where => where.Email == registerDto.Email);
            var nickname = await userRepository.GetItem(where => where.Nickname == registerDto.Nickname);
            var errors = new List<ErrorValidationDto>();
            if (email != null)
            {
                errors.Add(new ErrorValidationDto("email", "Email já cadastrado"));
            }
            if (nickname != null)
            {
                errors.Add(new ErrorValidationDto("nickname", "Nickname já cadastrado"));
            }
            if (errors.Count > 0)
            {
            }
            var user = mapper.Map<UserEntity>(registerDto);
            var role = await roleRepository.GetItem(where => where.Name.Equals("User"));
            user.PasswordSalt = passwordHelper.GenerateSalt();
            user.Password = passwordHelper.HashPassword(registerDto.Password!, user.PasswordSalt);
            user.UserRoleId = role.Id;
            user = await userRepository.Add(user);
            var userDto = mapper.Map<RegisterDto>(user);
            userDto.Password = null;
            return userDto;
        }
    }
}
