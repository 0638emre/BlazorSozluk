using AutoMapper;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using MediatR;

namespace BlazorSozluk.Application.Features.Commands.User.ChangePassword;

public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, bool>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public ChangeUserPasswordCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserId.HasValue)
            throw new DatabaseValidationException("User id is null");

        var user = await _userRepository.GetSingleAsync(u => u.Id == request.UserId);

        if (user is null)
            throw new DatabaseValidationException("User not found");

        var oldPassword = PasswordEncryptor.Encrypt(request.OldPassword);
        if (user.Password != oldPassword)
            throw new DatabaseValidationException("User old password wrong!");

        user.Password = request.NewPassword;
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync(); // ??TODO: Buna bi bak gerek var mı ?

        return true;
    }
}