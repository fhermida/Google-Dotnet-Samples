﻿using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Blogger.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace Daimto_Blogger_Sample
{
    class Authentication
    {
        /// <summary>
        /// Authenticate to Google Using Oauth2
        /// Documentation https://developers.google.com/accounts/docs/OAuth2
        /// </summary>
        /// <param name="clientId">From Google Developer console https://console.developers.google.com</param>
        /// <param name="clientSecret">From Google Developer console https://console.developers.google.com</param>
        /// <param name="userName">A string used to identify a user.</param>
        /// <returns></returns>
        public static BloggerService AuthenticateOauth(string clientId, string clientSecret, string userName)
        {
            
            string[] scopes = new string[] { BloggerService.Scope.Blogger,  // view and manage your analytics data                                            
                                             BloggerService.Scope.BloggerReadonly};     // View your Blogs

            try
            {
                // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
                UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret }
                                                                                             , scopes
                                                                                             , userName
                                                                                             , CancellationToken.None
                                                                                             , new FileDataStore("Daimto.Blogger.Auth.Store")).Result;

                BloggerService service = new BloggerService(new BloggerService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Blogger API Sample",
                });
                return service;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.InnerException);
                return null;

            }

        }

        



    }
    }

