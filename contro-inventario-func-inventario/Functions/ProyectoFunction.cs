using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using control_inventario_function.Soporte;
using control_inventario_function.SoporteDto;
using control_inventario_function.SoporteUtil;
using control_inventario_service_inventario.Service;
using control_inventario_service_inventario.Service.Imp;
using control_inventario_service_inventario.ServiceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace contro_inventario_func_inventario.Functions
{
    public class ProyectoFunction
    {
        private readonly IProyectoService _proyectoService;
        private readonly IExecutorFunctions _executorFunctions;

        public ProyectoFunction(IProyectoService proyectoService, IExecutorFunctions executorFunctions)
        {
            this._proyectoService = proyectoService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("ProyectoListar")]
        [OpenApiOperation(operationId: "proyectoListar", tags: new[] { "Proyecto" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> proyectoListar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "proyecto/listar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var lista = await _proyectoService.Lista();
                var response = new Response<List<ProyectoDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(lista, Mensajes.correcto);
            }, log);
        }

        [FunctionName("ProyectoGuardar")]
        [OpenApiOperation(operationId: "proyectoGuardar", tags: new[] { "Proyecto" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> proyectoGuardar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "proyecto/guardar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<ProyectoDto>();
                await _proyectoService.Guardar(body);
                var response = new Response<List<ProyectoDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ProyectoDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("ProyectoEditar")]
        [OpenApiOperation(operationId: "proyectoEditar", tags: new[] { "Proyecto" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> proyectoEditar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "proyecto/editar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<ProyectoDto>();
                await _proyectoService.Actualizar(body);
                var response = new Response<List<ProyectoDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ProyectoDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("ProyectoVigente")]
        [OpenApiOperation(operationId: "proyectoVigente", tags: new[] { "Proyecto" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> proyectoVigente(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "proyecto/vigente")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var body = await req.GetBodyAsync<ProyectoVigenteDto>();
                await _proyectoService.Vigente(body);
                var response = new Response<List<ProyectoDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ProyectoDto>(), Mensajes.correcto);
            }, log);
        }

        [FunctionName("ProyectoEliminar")]
        [OpenApiOperation(operationId: "proyectoEliminar", tags: new[] { "Proyecto" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> proyectoEliminar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "proyecto/eliminar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                int IdProyecto = Convert.ToInt32(req.Query["IdProyecto"]);
                await _proyectoService.Eliminar(IdProyecto);
                var response = new Response<List<ProyectoDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(new List<ProyectoDto>(), Mensajes.correcto);
            }, log);
        }


    }
}

