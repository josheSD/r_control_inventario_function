﻿using control_inventario_function.Soporte;
using control_inventario_function.SoporteUtil;
using control_inventario_repository_inventario.Context;
using control_inventario_service_inventario.Service;
using control_inventario_service_inventario.Service.Imp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(contro_inventario_func_inventario.Startup))]
namespace contro_inventario_func_inventario
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var sqlConnection = SettingEnvironment.GetSqlConnectionString();
            builder.Services.AddDbContext<ControlInventarioContext>(options => options.UseSqlServer(sqlConnection));
            builder.Services.AddTransient<IExecutorFunctions, ExecutorFunctions>();
            builder.Services.AddTransient<IAlmacenService, AlmacenService>();
            builder.Services.AddTransient<IArticuloService, ArticuloService>();
            builder.Services.AddTransient<ICategoriaService, CategoriaService>();
            builder.Services.AddTransient<IProyectoService, ProyectoService>();
        }
    }
}
