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

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44343");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "PresupuestoClient",
                ClientSecret = "PresupuestoClientSecret",
                Scope = "PresupuestoTodo"
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

            //Prueba Consulta Rps Rubros
            var response = await apiClient.GetAsync("https://localhost:44301/api/ExternoRp/RpsRubros/Vigencia/2020/TipoDocumento/CONTRATO/NumeroDocumento/2020020001/Identificacion/1111111");


            //var response = await apiClient.GetAsync("https://localhost:44340/api/Prueba/Cadena");
            //var response = await apiClient.GetAsync("https://localhost:44340/api/ApisExternas/ConsultarContratosConRps/Contratista/50950218");

            //encabezado user  //sistema
            //request          //

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }

    //     public class FactoryHttp : IFactoryHttp
    //     {
    //         public HttpClient Crear(string username)
    //         {
    //             //obnet la direccion de identitu
    //             //y configurr ç

    //             // discover endpoints from metadata
    //             var client = new HttpClient();

    //             var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44343");
    //             if (disco.IsError)
    //             {
    //                 Console.WriteLine(disco.Error);
    //                 return;
    //             }

    //             // request token
    //             var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
    //             {
    //                 Address = disco.TokenEndpoint,
    //                 ClientId = "Sigelc",
    //                 ClientSecret = "SigelcSecret",
    //                 Scope = "UnidadContratacion"
    //             });

    //             if (tokenResponse.IsError)
    //             {
    //                 Console.WriteLine(tokenResponse.Error);
    //                 return;
    //             }

    //             Console.WriteLine(tokenResponse.Json);
    //             Console.WriteLine("\n\n");

    //             // call api
    //             var apiClient = new HttpClient();
    //             Console.WriteLine(tokenResponse.AccessToken);
    //             apiClient.SetBearerToken(tokenResponse.AccessToken);
    //             apiClient.DefaultRequestHeaders.Add("ByAUserName", "deisy");//?


    //         }
    //     }
    //     public class AgentService
    //     {
    //         private IFactoryHttp _factoryHttp;
    //         public AgentService(IFactoryHttp factoryHttp)
    //         {
    //             _factoryHttp = factoryHttp;
    //         }
    //         public void AgenteServicio()
    //         {
    //             HttpClient apiClient = _factoryHttp.Crear("user");

    //             var response = await apiClient.GetAsync("https://localhost:44340/api/Prueba/Cadena");
    //             //var response = await apiClient.GetAsync("https://localhost:44340/api/ApisExternas/ConsultarContratosConRps/Contratista/50950218");

    //             //encabezado user  //sistema
    //             //request          //

    //             if (!response.IsSuccessStatusCode)
    //             {
    //                 Console.WriteLine(response.StatusCode);
    //             }
    //             else
    //             {
    //                 var content = await response.Content.ReadAsStringAsync();
    //                 Console.WriteLine(content);
    //             }
    // ;
    //         }

    //     }

    }
}