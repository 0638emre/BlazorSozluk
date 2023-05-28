using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorSozluk.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Application.Features.Queries.Entry.GetEntries;

public class GetEntriesQueryHandler : IRequestHandler<GetEntriesQuery, List<GetEntriesViewModel>>
{
    private readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }

    public async Task<List<GetEntriesViewModel>> Handle(GetEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if (request.TodaysEntries)//gelen data bugüne ait olanları getir der isek.
        {
            query = query.Where(i => i.CreatedDate >= DateTime.Now.Date)
                         .Where(i => i.CreatedDate <= DateTime.Now.Date.AddDays(1).Date);
        }

        //gelen data rasgele getirilir.
        query = query.Include(i => i.EntryComments)
            .OrderBy(i => Guid.NewGuid())
            .Take(request.Count);

        //automapper in nimetlerinden istifade edelim. queryable dan gelen sonucu mapliyoruz.(tabi mapping classımıza bunu bildirmemiz gerekmekte.) cancellationtoken da mevcut.
        return await query.ProjectTo<GetEntriesViewModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

    }
}