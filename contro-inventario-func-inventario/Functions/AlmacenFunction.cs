using control_inventario_function.Soporte;
using control_inventario_function.SoporteDto;
using control_inventario_service_inventario.Service;
using control_inventario_service_inventario.ServiceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using control_inventario_function.SoporteUtil;

namespace contro_inventario_func_inventario.Functions
{
    public class AlmacenFunction
    {
        private readonly IAlmacenService _almacenService;
        private readonly IExecutorFunctions _executorFunctions;

        public AlmacenFunction(IAlmacenService almacenService, IExecutorFunctions executorFunctions)
        {
            this._almacenService = almacenService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("AlmacenListar")]
        [OpenApiOperation(operationId: "almacenListar", tags: new[] { "Almacen" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> almacenListar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "almacen/listar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var lista = await _almacenService.Lista();
                var response = new Response<List<AlmacenDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(lista, Mensajes.correcto);
            }, log);
        }

        [FunctionName("AlmacenGuardar")]
        [OpenApiOperation(operationId: "almacenGuardar", tags: new[] { "Almacen" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> almacenGuardar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "almacen/guardar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<AlmacenDto>();
                await _almacenService.Guardar(body);
                var response = new Response<List<AlmacenDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<AlmacenDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("AlmacenEditar")]
        [OpenApiOperation(operationId: "almacenEditar", tags: new[] { "Almacen" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> almacenEditar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "almacen/editar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<AlmacenDto>();
                await _almacenService.Actualizar(body);
                var response = new Response<List<AlmacenDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<AlmacenDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("AlmacenEliminar")]
        [OpenApiOperation(operationId: "almacenEliminar", tags: new[] { "Almacen" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> almacenEliminar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "almacen/eliminar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                int IdAlmacen = Convert.ToInt32(req.Query["IdAlmacen"]);
                await _almacenService.Eliminar(IdAlmacen);
                var response = new Response<List<AlmacenDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<AlmacenDto>(), Mensajes.correcto);
            }, log);
        }

    }
}
