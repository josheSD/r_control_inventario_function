using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace control_inventario_function.SoporteDto.Resource
{
    public class ResourceStorage
    {
        public byte[] FileBytes { get; set; }
        public string ContentType { get; set; }
        public string base64 { get; set; }
        public string Name { get; set; }
    }
}
