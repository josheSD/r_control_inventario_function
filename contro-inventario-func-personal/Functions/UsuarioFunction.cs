using control_inventario_function.Soporte;
using control_inventario_function.SoporteDto;
using control_inventario_service_personal.service;
using control_inventario_service_personal.ServiceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using control_inventario_function.SoporteUtil;

namespace contro_inventario_func_personal.Functions
{
    public class UsuarioFunction
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IExecutorFunctions _executorFunctions;

        public UsuarioFunction(IUsuarioService usuarioService, IExecutorFunctions executorFunctions)
        {
            this._usuarioService = usuarioService;
            this._executorFunctions = executorFunctions;
        }

        [FunctionName("UsuarioListar")]
        [OpenApiOperation(operationId: "usuario-01", tags: new[] { "Usuario" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<List<UsuarioDto>>), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        public async Task<ActionResult> UsuarioListar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "usuario/listar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var lista = await _usuarioService.Listar();
                var response = new Response<List<UsuarioDto>>();
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Ok(lista, Mensajes.correcto);
            }, log);
        }

        [FunctionName("UsuarioGuardar")]
        [OpenApiOperation(operationId: "usuario-02", tags: new[] { "Usuario" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<UsuarioDto>), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        [OpenApiRequestBody("application/json", typeof(UsuarioDto))]
        public async Task<ActionResult> UsuarioGuardar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "usuario/guardar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var filtro = await req.GetBodyAsync<UsuarioDto>();
                await _usuarioService.Guardar(filtro);
                var response = new Response<UsuarioDto>();
                var mensaje = "Usuario Creado";
                log.LogInformation("C# HTTP trigger function processed a request.");

                return response.Accepted(new UsuarioDto(), mensaje);
            }, log);
        }

        [FunctionName("UsuarioEditar")]
        [OpenApiOperation(operationId: "usuario-03", tags: new[] { "Usuario" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<UsuarioDto>), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        [OpenApiRequestBody("application/json", typeof(UsuarioDto))]
        public async Task<ActionResult> UsuarioEditar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "usuario/editar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                var filtro = await req.GetBodyAsync<UsuarioDto>();
                await _usuarioService.Actualizar(filtro);
                var response = new Response<UsuarioDto>();
                var mensaje = "Usuario Actualizado";
                log.LogInformation("C# HTTP trigger function processed a request.");
                return response.Accepted(new UsuarioDto(), mensaje);
            }, log);
        }

        [FunctionName("UsuarioEliminar")]
        [OpenApiOperation(operationId: "usuario-04", tags: new[] { "Usuario" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Response<UsuarioDto>), Description = "")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.BadRequest, contentType: "application/json", bodyType: typeof(Response))]
        [OpenApiParameter(name: "IdUsuario", In = ParameterLocation.Query, Required = true, Type = typeof(int))]
        public async Task<ActionResult> UsuarioEliminar(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "usuario/eliminar")] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return await _executorFunctions.ExecuteFunctions(async () =>
            {
                int IdUsuario = Convert.ToInt32(req.Query["IdUsuario"]);
                 await _usuarioService.Eliminar(IdUsuario);
                var response = new Response<UsuarioDto>();
                var mensaje = "Usuario Eliminado";
                log.LogInformation("C# HTTP trigger function processed a request.");

                return response.Accepted(new UsuarioDto(), mensaje);
            }, log);
        }


    }
}
