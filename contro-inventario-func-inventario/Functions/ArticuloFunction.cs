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
using control_inventario_repository_inventario.Entity;

namespace contro_inventario_func_inventario.Functions
{
    public class ArticuloFunction
    {
        private readonly IArticuloService _articuloService;
        private readonly IExecutorFunctions _executorFunctions;

        public ArticuloFunction(IArticuloService articuloService, IExecutorFunctions executorFunctions)
        {
            this._articuloService = articuloService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("ArticuloListar")]
        [OpenApiOperation(operationId: "articuloListar", tags: new[] { "Articulo" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> articuloListar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "articulo/listar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var lista = await _articuloService.Lista();
                var response = new Response<List<ArticuloDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(lista, Mensajes.correcto);
            }, log);
        }

        [FunctionName("ArticuloGuardar")]
        [OpenApiOperation(operationId: "articuloGuardar", tags: new[] { "Articulo" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> articuloGuardar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "articulo/guardar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<ArticuloDto>();
                await _articuloService.Guardar(body);
                var response = new Response<List<ArticuloDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ArticuloDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("ArticuloEditar")]
        [OpenApiOperation(operationId: "articuloEditar", tags: new[] { "Articulo" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> articuloEditar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "articulo/editar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<ArticuloDto>();
                await _articuloService.Actualizar(body);
                var response = new Response<List<ArticuloDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ArticuloDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("ArticuloEliminar")]
        [OpenApiOperation(operationId: "articuloEliminar", tags: new[] { "Articulo" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> articuloEliminar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "articulo/eliminar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                int IdArticulo = Convert.ToInt32(req.Query["IdArticulo"]);
                await _articuloService.Eliminar(IdArticulo);
                var response = new Response<List<ArticuloDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ArticuloDto>(), Mensajes.correcto);
            }, log);
        }

    }
}
