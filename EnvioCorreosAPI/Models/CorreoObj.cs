using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace EnvioCorreosAPI.Models
{
    public class CorreoObj
    {

        public int CorreoID { get; set; }
        public MailAddress Remitente { get; set; }
        public string Destinatario { get; set; }
        public string CopiaDestinatario { get; set; }
        public string CuerpoMensaje { get; set; }
        public string Asunto { get; set; }
        public Nullable<DateTime> FechaHoraEnvio { get; set; }

    }
}