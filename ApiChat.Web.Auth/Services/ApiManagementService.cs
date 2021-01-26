﻿using Microsoft.Azure.Management.ApiManagement;
using Microsoft.Azure.Management.ApiManagement.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiChat.Web.Auth.Services
{
    public class ApiManagementService : IApiManagementService
    {
        private readonly string _subscriptionId;
        private readonly string _resourceGroupName;
        private readonly string _serviceName;

        public ApiManagementService(IConfiguration configuration)
        {
            _subscriptionId = configuration["ApiManagement:SubscriptionId"];
            _resourceGroupName = configuration["ApiManagement:ResourceGroupName"];
            _serviceName = configuration["ApiManagement:ServiceName"];
        }

        public async Task UserCreateOrUpdateAsync(string token, string userId, UserCreateParameters parameters)
        {
            var apiManagement = new ApiManagementClient(new TokenCredentials(token))
            {
                SubscriptionId = _subscriptionId
            };

            await apiManagement.User.CreateOrUpdateAsync(_resourceGroupName, _serviceName, userId, parameters);
        }

        public async Task<UserTokenResult> GetSharedAccessTokenAsync(string token, string userId, UserTokenParameters parameters)
        {
            var apiManagement = new ApiManagementClient(new TokenCredentials(token))
            {
                SubscriptionId = _subscriptionId
            };

            return await apiManagement.User.GetSharedAccessTokenAsync(_resourceGroupName, _serviceName, userId, parameters);
        }

        public async Task SubscribtionCreateOrUpdateAsync(string token, string sid, SubscriptionCreateParameters parameters)
        {
            var apiManagement = new ApiManagementClient(new TokenCredentials(token))
            {
                SubscriptionId = _subscriptionId
            };

            await apiManagement.Subscription.CreateOrUpdateAsync(_resourceGroupName, _serviceName, sid, parameters);
        }
    }

    public interface IApiManagementService
    {
        Task UserCreateOrUpdateAsync(string token, string userId, UserCreateParameters parameters);
        Task<UserTokenResult> GetSharedAccessTokenAsync(string token, string userId, UserTokenParameters parameters);
        Task SubscribtionCreateOrUpdateAsync(string token, string sid, SubscriptionCreateParameters parameters);
    }
}