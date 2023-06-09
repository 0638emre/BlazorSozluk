﻿using System.Net.Http.Json;
using BlazorSozluk.Application.Features.Commands.EntryCommand.Create;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
     private readonly HttpClient client;

        public EntryService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<GetEntriesViewModel>> GetEntires()
        {
            var result = await client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEntries=false&count=30");

            return result;
        }

        public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
        {
            var result = await client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");

            return result;
        }

        public async Task<PageViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PageViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<PageViewModel<GetEntryDetailViewModel>> GetProfilePageEntries(int page, int pageSize, string userName = null)
        {
            var result = await client.GetFromJsonAsync<PageViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize={pageSize}");

            return result;
        }

        public async Task<PageViewModel<GetEntryCommentViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
        {
            var result = await client.GetFromJsonAsync<PageViewModel<GetEntryCommentViewModel>>($"/api/entry/comments/{entryId}?page={page}&pageSize={pageSize}");

            return result;
        }


        public async Task<Guid> CreateEntry(CreateEntryCommand command)
        {
            var res = await client.PostAsJsonAsync("/api/Entry/CreateEntry", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
        {
            var res = await client.PostAsJsonAsync("/api/Entry/CreateEntryComment", command);

            if (!res.IsSuccessStatusCode)
                return Guid.Empty;

            var guidStr = await res.Content.ReadAsStringAsync();

            return new Guid(guidStr.Trim('"'));
        }

        public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        {
            var result = await client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/entry/Search?searchText={searchText}");

            return result;
        }
}