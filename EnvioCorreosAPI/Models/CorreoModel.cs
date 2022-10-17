using EnvioCorreosAPI.BaseDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace EnvioCorreosAPI.Models
{
    public class CorreoModel
    {

        public RespuestaObj RegistrarCorreo (CorreoObj correo)
        {
            /*Ir a la BD*/
            using (var baseDatos = new EnvioCorreosEntities2())
            {
                var respuesta = baseDatos.Registrar_Correos(correo.Destinatario, correo.CopiaDestinatario, correo.Asunto, correo.CuerpoMensaje,  correo.FechaHoraEnvio);

                if (respuesta > 0)
                {
                    return new RespuestaObj
                    {
                        codigo = 1,
                        mensaje = "Correo registrado en base de datos"
                    };
                }
                return null;
            }
        }

        public MailMessage cargarMail (CorreoObj correo)
        {
            string subject = correo.Asunto;
            string body = correo.CuerpoMensaje;
            string FromMail = "jsandi50148@ufide.ac.cr";
            string emailTo = correo.Destinatario;
            string emailToCopy = correo.CopiaDestinatario;
            if (emailToCopy != null && emailToCopy != "")
            {
                emailToCopy = "";
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FromMail);
                mail.To.Add(emailTo);
                if(emailToCopy != "")
                {
                    mail.To.Add(emailToCopy);
                }
                mail.Subject = subject;
                mail.Body = body;
                return mail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}