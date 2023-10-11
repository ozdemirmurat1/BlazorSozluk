﻿using Blazored.LocalStorage;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Extensions;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using BlazorSozluk.WebApplication.Infrastructure.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly ISyncLocalStorageService _syncLocalStorageService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public IdentityService(HttpClient httpClient, ISyncLocalStorageService syncLocalStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClient = httpClient;
            _syncLocalStorageService = syncLocalStorageService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());

        public string GetUserToken()
        {
            return _syncLocalStorageService.GetToken();
        }

        public string GetUserName()
        {
            return _syncLocalStorageService.GetToken();
        }

        public Guid GetUserId()
        {
            return _syncLocalStorageService.GetUserId();
        }

        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await _httpClient.PostAsJsonAsync("/api/User/Login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }

                return false;
            }

            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token))
            {
                _syncLocalStorageService.SetToken(response.Token);
                _syncLocalStorageService.SetUserName(response.UserName);
                _syncLocalStorageService.SetUserId(response.Id);

                ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogin(response.UserName, response.Id);

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", response.UserName);

                return true;
            }

            return false;
        }

        public void Logout()
        {
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);

            ((AuthStateProvider)_authenticationStateProvider).NotifyUserLogout();

            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
