using EnvioCorreosAPI.Models;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace EnvioCorreosAPI.Controllers
{
    public class EnvioCorreoController : ApiController
    {

        CorreoModel model = new CorreoModel();

        [HttpPost]
        [Route("api/EnviarCorreo")]

        public RespuestaObj EnviarCorreo (CorreoObj correo)
        {
            if (correo.Asunto == null || correo.CuerpoMensaje == null || correo.Destinatario == null)
            {
                return new RespuestaObj
                {
                    codigo = 0,
                    mensaje = "Faltan campos requeridos"
                };
            }

            string pattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$";
            Regex re = new Regex(pattern);

            if (correo.CopiaDestinatario != "" && correo.CopiaDestinatario != null)
            {
                if (!re.IsMatch(correo.CopiaDestinatario))
                {
                    return new RespuestaObj
                    {
                        codigo = 2,
                        mensaje = "La copia destinatario no tiene un formato de correo valido"
                    };
                }
            }

            if (!re.IsMatch(correo.Destinatario))
            {
                return new RespuestaObj
                {
                    codigo = 2,
                    mensaje = "El destinatario no tiene un formato de correo valido"
                };
            }

            var mail = model.cargarMail(correo);

            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");
                SmtpServer.Port = 587;
                //En correo y contraseña van credenciales de un correo office
                SmtpServer.Credentials = new System.Net.NetworkCredential("correo", "contraseña");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                return new RespuestaObj
                {
                    codigo = 99,
                    mensaje = "Error con el servidor de correo"+ex.Message
                };
            }

            try
            {
                //Se procede a registrar los datos del correo enviado en base de datos
                return model.RegistrarCorreo(correo);
            }

            catch (Exception ex)
            {
                return new RespuestaObj
                {
                    codigo = 3,
                    mensaje = "Error con al almacenar el correo en la base de datos" + ex.Message
                };
            }
        }


    }
}