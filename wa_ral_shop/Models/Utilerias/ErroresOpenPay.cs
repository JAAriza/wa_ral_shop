using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Utilerias
{
    public class ErroresOpenPay
    {
        /// <summary>
        /// Recibe numero entero del error y retorna mensaje que corresponde al código de error
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public string MensajeGeneral(int Codigo)
        {
            string Respuesta= string.Empty;
            switch (Codigo)
            {
                case 1000:
                    Respuesta = "Ocurrió un error interno en el servidor de Openpay";
                    break;

                case 1001:
                    Respuesta = "El formato de la petición no es JSON, los campos no tienen el formato correcto, o la petición no tiene campos que son requeridos.";
                    break;

                case 1002:
                    Respuesta = "La llamada no esta autenticada o la autenticación es incorrecta.";
                    break;

                case 1003:
                    Respuesta = "La operación no se pudo completar por que el valor de uno o más de los parametros no es correcto.";
                    break;

                case 1004:
                    Respuesta = "Un servicio necesario para el procesamiento de la transacción no se encuentra disponible.";
                    break;

                case 1005:
                    Respuesta = "Uno de los recursos requeridos no existe.";
                    break;

                case 1006:
                    Respuesta = "Ya existe una transacción con el mismo ID de orden.";
                    break;

                case 1007:
                    Respuesta = "La transferencia de fondos entre una cuenta de banco o tarjeta y la cuenta de Openpay no fue aceptada.";
                    break;

                case 1008:
                    Respuesta = "Una de las cuentas requeridas en la petición se encuentra desactivada.";
                    break;

                case 1009:
                    Respuesta = "El cuerpo de la petición es demasiado grande.";
                    break;

                case 1010:
                    Respuesta = "Se esta utilizando la llave pública para hacer una llamada que requiere la llave privada, o bien, se esta usando la llave privada desde JavaScript.";
                    break;

                default:
                    Respuesta = "Ocurrió un problema interno";
                    break;
            }
            return Respuesta;
        }

        /// <summary>
        /// Recibe numero entero del error y retorna mensaje que corresponde al código de error
        /// </summary>
        /// <param name="Codigo"></param>
        /// <returns></returns>
        public string MensajeAlmacenamiento(int Codigo)
        {
            string Respuesta = string.Empty;
            switch (Codigo)
            {
                case 2001:
                    Respuesta = "La cuenta de banco con esta CLABE ya se encuentra registrada en el cliente.";
                    break;

                case 2002:
                    Respuesta = "La tarjeta con este número ya se encuentra registrada en el cliente.";
                    break;

                case 2003:
                    Respuesta = "El cliente con este identificador externo (External ID) ya existe.";
                    break;

                case 2004:
                    Respuesta = "El dígito verificador del número de tarjeta es inválido de acuerdo al algoritmo Luhn.";
                    break;

                case 2005:
                    Respuesta = "La fecha de expiración de la tarjeta es anterior a la fecha actual.";
                    break;
                case 2006:
                    Respuesta = "El código de seguridad de la tarjeta (CVV2) no fue proporcionado.";
                    break;
                case 2007:
                    Respuesta = "El número de tarjeta es de prueba, solamente puede usarse en Sandbox.";
                    break;
                case 2008:
                    Respuesta = "La tarjeta consultada no es valida para puntos.";
                    break;
                case 2009:
                    Respuesta = "El código de seguridad de la tarjeta (CVV2) no no es valido.";
                    break;

                default:
                    Respuesta = "Ocurrio un problema al guardar";
                    break;
            }
            return Respuesta;
        }

        /// <summary>
        /// Recibe numero entero del error y retorna mensaje que corresponde al código de error
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string MensajeTarjeta(int codigo)
        {
            string Respuesta = string.Empty;

            switch (codigo)
            {
                case 3001:
                    Respuesta = "La tarjeta fue declinada.";
                    break;

                case 3002:
                    Respuesta = "La tarjeta ha expirado.";
                    break;
                case 3003:
                    Respuesta = "La tarjeta no tiene fondos suficientes.";
                    break;
                case 3004:
                    Respuesta = "La tarjeta ha sido identificada como una tarjeta robada.";
                    break;
                case 3005:
                    Respuesta = "La tarjeta ha sido identificada como una tarjeta fraudulenta.";
                    break;
                case 3006:
                    Respuesta = "La operación no esta permitida para este cliente o esta transacción.";
                    break;
                case 3008:
                    Respuesta = "La tarjeta no es soportada en transacciones en linea.";
                    break;
                case 3009:
                    Respuesta = "La tarjeta fue reportada como perdida.";
                    break;
                case 3010:
                    Respuesta = "El banco ha restringido la tarjeta.";
                    break;
                case 3011:
                    Respuesta = "El banco ha solicitado que la tarjeta sea retenida. Contacte al banco.";
                    break;
                case 3012:
                    Respuesta = "Se requiere solicitar al banco autorización para realizar este pago.";
                    break;

                default:
                    Respuesta = "Problemas al realizar la operación.";
                    break;
            }

            return Respuesta;
        }

        

    }
}