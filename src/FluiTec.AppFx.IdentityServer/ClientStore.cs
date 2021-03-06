﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace FluiTec.AppFx.IdentityServer
{
    /// <summary>	A client store. </summary>
    public class ClientStore : IClientStore
    {
        #region Fields

        /// <summary>	The identity server data service. </summary>
        private readonly IIdentityServerDataService _dataService;

        #endregion

        #region Constructors

        /// <summary>	Constructor. </summary>
        /// <param name="dataService">					The data service. </param>
        public ClientStore(IIdentityServerDataService dataService)
        {
            _dataService = dataService;
        }

        #endregion

        #region Methods

        /// <summary>	Searches for the first client by identifier asynchronous. </summary>
        /// <param name="clientId">	Identifier for the client. </param>
        /// <returns>	The found client by identifier asynchronous. </returns>
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            using (var uow = _dataService.StartUnitOfWork())
            {
                var entity = uow.ClientRepository.GetCompoundByClientId(clientId);
                if (entity == null) return Task.FromResult((Client) null);

                var client = new Client
                {
                    ClientId = entity.Client.ClientId,
                    ClientName = entity.Client.Name,
                    ClientSecrets = new List<Secret>(new[] {new Secret(entity.Client.Secret.Sha256())}),
                    AllowedGrantTypes = entity.Client.GrantTypes?.Split(','),
                    AllowedScopes = entity.Scopes.Select(s => s.Name).ToList(),
                    RedirectUris = new List<string>(GetUrisSplit(entity.Client.RedirectUri)),
                    PostLogoutRedirectUris = new List<string>(GetUrisSplit(entity.Client.PostLogoutUri)),
                    AllowOfflineAccess = entity.Client.AllowOfflineAccess,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AlwaysSendClientClaims = false,
                    Claims = entity.ClientClaims
                        .Select(claimEntity => new Claim(claimEntity.ClaimType, claimEntity.ClaimValue))
                        .ToList()
                };

                return Task.FromResult(client);
            }
        }

        /// <summary>   Gets uris split. </summary>
        /// <param name="value">    The value. </param>
        /// <returns>   The uris split. </returns>
        private List<string> GetUrisSplit(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<string>();
            if (value.Contains(','))
            {
                return value.Split(',').ToList();
            }
            else
            {
                return new List<string>(new [] {value});
            }
        }

        #endregion
    }
}