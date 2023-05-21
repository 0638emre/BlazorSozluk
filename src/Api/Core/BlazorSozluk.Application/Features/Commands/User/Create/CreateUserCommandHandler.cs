using AutoMapper;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Application.Features.Commands.User.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existsUser = await _userRepository.GetSingleAsync(u => u.EmailAddress == request.EmailAddress);

            if (existsUser is not null)
                throw new DatabaseValidationException("User already exists!");

            var dbUser = _mapper.Map<Domain.Models.User>(request);

            var rows = await _userRepository.AddAsync(dbUser);

            //BURADA RABBİT MQ İLE KULLANICI UPDATE OLDUĞU İÇİN EMAİL CHANGES/CREATED BİLGİSİ GEÇMEMİZ GEREK.
            if (rows > 0)
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = request.EmailAddress
                };

                QueueFactory.SendMessage(exchangeName: SozlukConstans.UserExchangeName,
                    exchangeType: SozlukConstans.DefaultExchangeType,
                    queueName: SozlukConstans.UserEmailChangedQueueName, obj: @event);
            }

            return dbUser.Id;
        }
    }
}