using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Infrastructure;
using ECommerce.Common.Models.Queries;
using ECommerce.Common.Models.RequestModels;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api.Application.Features.Commands.User;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
{


    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;

    public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
    {
        this.configuration = configuration;
        this.mapper = mapper;
        this.userRepository = userRepository;

    }

    public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.Email);

        if (dbUser == null)
            throw new DatabaseValidationException("User not found!");

        var pass = PasswordEncryptor.Encrypt(request.Password);

        if (dbUser.Password != pass)
            throw new DatabaseValidationException("Password is wrong!");

        if (!dbUser.EmailConfirmed)
            throw new DatabaseValidationException("Email address is not confirm yet!");


        var result = mapper.Map<LoginUserViewModel>(dbUser);

        var claims = new Claim[]
            {
                new Claim(ClaimTypes. NameIdentifier, dbUser.ID.ToString()),
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
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(10);
        var token = new JwtSecurityToken(claims: claims,
                                        expires: expiry,
                                        signingCredentials: creds,
                                        notBefore: DateTime.Now);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

