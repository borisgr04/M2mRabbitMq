// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static async Task Main()
        {
            // discover endpoints from metadata
            var client = new HttpClient();
                                                                //https://localhost:44343/
            var disco = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest {
                Address = "http://45.40.133.92:8027/identity/",
                Policy =
                {
                    RequireHttps = false
                }
            });
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "archivoInventarioOpenApiDev",
                ClientSecret = "archivoInventarioOpenApiSecret",
                Scope = "ArchivoInventario"
            });
            
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            Console.WriteLine(tokenResponse.AccessToken);
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            apiClient.DefaultRequestHeaders.Add("ByAUserName", "deisy");//?
                                                   //"llamar api para que consulte sircc"     
            var response = await apiClient.GetAsync("https://localhost:5001/WeatherForecast");
            
            //encabezado user  //sistema
            //request          //

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}