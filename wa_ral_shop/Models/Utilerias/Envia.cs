using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestSharp;

namespace wa_ral_shop.Models.Utilerias
{
    public class Envia
    {
        string Key = "fd8b689dfd4ea1f6178442eadc8f1f8f4eec90f4abcbb5acf263611f1acfe064";

        /// <summary>
        /// Recibe CP y retorna todas las colonias que corresponde a ese CP
        /// </summary>
        /// <param name="CP"></param>
        /// <returns>string con error o con la información</returns>
        public string ValidaCP(int CP)
        {
            try
            {
                string baseURL = "https://geocodes.envia.com/zipcode/mx/";
                var client = new RestClient(baseURL + CP.ToString());
                client.Options.MaxTimeout = -1;
                var request = new RestRequest();
                request.Method = Method.Get;
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception e)
            {
                return e.Message;
            }            
        }

        /// <summary>
        /// Retorna toda la informacion de los estados de mexico entre ellos el identificador del estado
        /// </summary>
        /// <returns>string con error o con la información</returns>
        public string GetCodeEdos()
        {
            try
            {
                var client = new RestClient("http://queries.envia.com/state?country_code=mx");
                client.Options.MaxTimeout = -1;
                var request = new RestRequest();
                request.Method = Method.Get;
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// Consulta de paqueterias disponible se realiza utilizando la llave
        /// </summary>
        /// <returns>string con error o con la informacion solicitada</returns>
        public string PaqueteriaDisponible()
        {
            try
            {
                var client = new RestClient("https://queries.envia.com/available-carrier/MX/0");
                client.Options.MaxTimeout = -1;

                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("Authorization", Key);
                RestResponse response = client.Execute(request);
                return response.Content;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


    }
}