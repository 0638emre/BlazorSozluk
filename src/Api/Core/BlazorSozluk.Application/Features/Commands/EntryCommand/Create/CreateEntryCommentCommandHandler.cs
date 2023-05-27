using AutoMapper;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;

namespace BlazorSozluk.Application.Features.Commands.EntryCommand.Create;

public class CreateEntryCommentCommandHandler: IRequestHandler<CreateEntryCommand ,Guid>
{
    private readonly IEntryCommentRepository _entryCommentRepository;
    private readonly IMapper _mapper;

    public CreateEntryCommentCommandHandler(IMapper mapper, IEntryCommentRepository entryCommentRepository)
    {
        _mapper = mapper;
        _entryCommentRepository = entryCommentRepository;
    }

    public async Task<Guid> Handle(CreateEntryCommand request, CancellationToken cancellationToken)
    {
        var entryComment = _mapper.Map<Domain.Models.EntryComment>(request);
        await _entryCommentRepository.AddAsync(entryComment);
        
        return entryComment.Id;
    }
}