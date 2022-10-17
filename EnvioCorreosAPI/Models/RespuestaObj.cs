using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnvioCorreosAPI.Models
{
    public class RespuestaObj
    {
        //codigos:
        //0: Falta informacion
        //1: Envio Existoso
        //2: Formato Correo Invalido
        //99: Error con el servidor de correo
        public int codigo { get; set; }
        public string mensaje { get; set; }

    }
}