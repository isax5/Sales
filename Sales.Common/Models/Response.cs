using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Common.Models
{
    public class Response
    {
        /// <summary>
        /// Peticion valida?
        /// </summary>
        public bool IsSeccess { get; set; }

        /// <summary>
        /// Mensaje de error si <see cref="IsSeccess"/> es False
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Resultado si <see cref="IsSeccess"/> es True
        /// </summary>
        public object Result { get; set; }

    }
}
