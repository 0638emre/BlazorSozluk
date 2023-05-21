using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BlazorSozluk.Application.Features.Commands.User.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;


        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

            if (dbUser == null)
                throw new DatabaseValidationException("User not found!");

            var pass = PasswordEncryptor.Encrypt(request.Password);
            if (dbUser.Password != pass)
                throw new DatabaseValidationException("Password is wrong!");

            if (!dbUser.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            var result = mapper.Map<LoginUserViewModel>(dbUser);

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim(ClaimTypes.Email, dbUser.EmailAddress),
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.GivenName, dbUser.FirstName),
            new Claim(ClaimTypes.Surname, dbUser.LastName)
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            byte[] anahtar = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(anahtar);
            }

            var key = new SymmetricSecurityKey(anahtar);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(10);

            var token = new JwtSecurityToken(claims: claims,
                expires: expiry,
                signingCredentials: creds,
                notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
            //kendi çözümümü bu şekidle buldum. muhtemelen app.settingsteki secretkey çok ufak kalıyor.



            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expiry = DateTime.Now.AddDays(10);

            //var token = new JwtSecurityToken(claims: claims,
            //    expires: expiry,
            //    signingCredentials: creds,
            //    notBefore: DateTime.Now);

            //return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
