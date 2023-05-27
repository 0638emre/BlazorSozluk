using AutoMapper;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Application.Features.Commands.User.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _userRepository.GetByIdAsync(request.Id);

            if (dbUser == null)
                throw new DatabaseValidationException("User nor found");

            var dbEmailAddress = dbUser.EmailAddress;
            var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;
            //compareOrdinal: karşılaştırma için kullanılan bir metot.

            _mapper.Map(request, dbUser);  //bunun ile aşağıdaki aynı işi yapıyor.
            //dbUser = _mapper.Map<Domain.Models.User>(request); 

            var rows = await _userRepository.UpdateAsync(dbUser);

            //BURADA RABBİT MQ İLE KULLANICI UPDATE OLDUĞU İÇİN EMAİL CHANGES/CREATED BİLGİSİ GEÇMEMİZ GEREK.
            
            if (emailChanged && rows > 0) //email addres değişti mi kontrolü
            {
                var @event = new UserEmailChangedEvent()
                {
                    OldEmailAddress = null,
                    NewEmailAddress = request.EmailAddress
                };

                QueueFactory.SendMessageToExchange(exchangeName: SozlukConstans.UserExchangeName, exchangeType: SozlukConstans.DefaultExchangeType,
                    queueName: SozlukConstans.UserEmailChangedQueueName, obj: @event);

                dbUser.EmailConfirmed = false; // email değiştiği için yeni email confirm edilmemiş demek. use onaylaması gerek

                await _userRepository.UpdateAsync(dbUser);

            }

            return dbUser.Id;
        }
    }
}
