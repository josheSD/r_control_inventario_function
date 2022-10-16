using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using control_inventario_function.Soporte;
using control_inventario_function.SoporteDto;
using control_inventario_service_personal.service;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using control_inventario_function.SoporteUtil;
using control_inventario_service_personal.ServiceDto;

namespace contro_inventario_func_personal.Functions
{
    public class RolFunction
    {
        private readonly IRolService _rolService;
        private readonly IExecutorFunctions _executorFunctions;

        public RolFunction(IRolService rolService, IExecutorFunctions executorFunctions)
        {
            this._rolService = rolService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("RolListar")]
        [OpenApiOperation(operationId: "rol-01", tags: new[] { "Rol" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> RolListar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "rol/listar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var lista = await _rolService.Listar();
                var response = new Response<List<RolDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(lista, Mensajes.correcto);
            }, log);
        }

    }
}
