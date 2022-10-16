using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using control_inventario_function.Soporte;

namespace control_inventario_function.SoporteUtil
{
    public static class HttpRequestExtensions
    {
        public static async Task<T> GetBodyAsync<T>(this HttpRequest req)
        {
            var bodyString = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyModel = JsonConvert.DeserializeObject<T>(bodyString);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(bodyModel, new ValidationContext(bodyModel, null, null), results, true);
            if (!isValid)
            {
                var errorMessage = string.Join(" - ", results.Select(s => s.ErrorMessage).ToArray());
                throw new CustomException(errorMessage);
            }
            return bodyModel;
        }
        public static async Task<List<T>> GetBodyListAsync<T>(this HttpRequest req)
        {
            var bodyString = await new StreamReader(req.Body).ReadToEndAsync();
            var bodyModel = JsonConvert.DeserializeObject<List<T>>(bodyString);
            var results = new List<ValidationResult>();
            var i = 1;
            foreach (var dto in bodyModel)
            {
                var isValid = Validator.TryValidateObject(dto, new ValidationContext(dto, null, null), results, true);
                if (!isValid)
                {
                    var errorFile = "Record N° " + i + " Error: ";
                    var errorMessage = errorFile + string.Join(" - ", results.Select(s => s.ErrorMessage).ToArray());
                    throw new CustomException(errorMessage);
                }
                i++;
            }
            return bodyModel;
        }

    }
}
