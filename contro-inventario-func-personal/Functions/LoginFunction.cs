using System.IO;
using System.Net;
using System.Threading.Tasks;
using control_inventario_function.Soporte;
using control_inventario_function.SoporteDto;
using control_inventario_function.SoporteUtil;
using control_inventario_service_personal.service;
using control_inventario_service_personal.ServiceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace contro_inventario_func_personal.Functions
{
    public class LoginFunction
    {
        private readonly ILoginService _loginService;
        private readonly IExecutorFunctions _executorFunctions;

        public LoginFunction(ILoginService loginService, IExecutorFunctions executorFunctions)
        {
            this._loginService = loginService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("LoginNormal")]
        [OpenApiOperation(operationId: "login-01", tags: new[] { "Login" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<LoginDto>), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        [OpenApiRequestBody("application/json", typeof(RequestLogin))]
        public async Task<ActionResult> LoginNormal(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login/normal")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var filtro = await req.GetBodyAsync<RequestLogin>();
                var login = await _loginService.Login(filtro);
                var response = new Response<LoginDto>();
                var mensaje = "Credencial correcto";
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Accepted(login, mensaje);
            }, log);

        }
    }
}

