﻿using BlazorSozluk.Application.Features.Commands.EntryCommand.Create;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;

namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

public interface IEntryService
{
    Task<Guid> CreateEntry(CreateEntryCommand command);
    Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
    Task<List<GetEntriesViewModel>> GetEntires();
    Task<PageViewModel<GetEntryCommentViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);
    Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
    Task<PageViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
    Task<PageViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null);
    Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);
}