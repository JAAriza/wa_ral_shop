using Openpay;
using Openpay.Entities;
using wa_ral_shop.Models.Anonymous.Catalogos;
using Openpay.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Openpay.Entities.Request;

namespace wa_ral_shop.Models.Utilerias
{
    public class Pagos
    {
        OpenpayAPI openpayAPI = new OpenpayAPI("sk_cb8fb0bed1d147ef9c408686f7221356", "mcpqgoe22dmeeukfcajw", "MX", false);

        public string CrearCliente(ClienteAnonymous clienteAnonymous)
        {
            string Error = string.Empty;
            Customer customer = new Customer();
            customer.Name = clienteAnonymous.Nombre;
            customer.LastName = clienteAnonymous.APaterno;
            customer.Email = clienteAnonymous.EMail;
            customer.ExternalId = clienteAnonymous.Id.ToString();
            customer.PhoneNumber = clienteAnonymous.Telefono;
            
            //customer.Address = new Address();
            //customer.Address.Line1 = "line 1";
            //customer.Address.PostalCode = "12355";
            //customer.Address.City = "Queretaro";
            //customer.Address.CountryCode = "MX";
            //customer.Address.State = "Queretaro";

            try
            {
                Customer customerCreated = openpayAPI.CustomerService.Create(customer);
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }

            return Error;
        }

        public Customer ConsultarCliente(string CustomerId)
        {
            //string customer_id = "a6fqqdzialiolrzijqid";
            Customer customer = new Customer();
            customer = openpayAPI.CustomerService.Get(CustomerId);
            return customer;
        }

        public string BorrarCliente()
        {
            string customer_id = "adyytoegxm6boiusecxm";
            string Error = string.Empty;
            try
            {
                openpayAPI.CustomerService.Delete(customer_id);
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }

        public string ActualizarCliente()
        {
            string Error = string.Empty;
            string customer_id = "adyytoegxm6boiusecxm";
            try
            {
                Customer customer = openpayAPI.CustomerService.Get(customer_id);
                customer.Name = "My new name";

                customer = openpayAPI.CustomerService.Update(customer);
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }

        public string ConsultarClientes()
        {
            string Error = string.Empty;
            try
            {
                SearchParams search = new SearchParams();
                search.Limit = 50;

                List<Customer> customers = openpayAPI.CustomerService.List(search);
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }

        public string CrearCargo()
        {
            string Error = string.Empty;

            try
            {
                string customer_id = "adyytoegxm6boiusecxm";

                ChargeRequest request = new ChargeRequest();
                request.Method = "card";
                request.SourceId = "kwkoqpg6fcvfse8k8mg2";
                request.Description = "Testing from .Net";
                request.Amount = new Decimal(9.99);

                Charge charge = openpayAPI.ChargeService.Create(customer_id, request);
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }

        public string CrearCarga()
        {
            string Error = string.Empty;
            try
            {
                string customer_id = "adyytoegxm6boiusecxm";

                ChargeRequest request = new ChargeRequest();
                request.Method = "card";
                request.SourceId = "kwkoqpg6fcvfse8k8mg2";
                request.Description = "Testing from .Net";
                request.Amount = new Decimal(9.99);
                request.Capture = false; //false indicate that only we want capture the amount

                Charge charge = openpayAPI.ChargeService.Create(customer_id, request);
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }

        public string ReembolsarCargo()
        {
            string Error = string.Empty;
            try
            {
                string customer_id = "adyytoegxm6boiusecxm";
                string charge_id = "ttcg5roe2py2bur38cx2";

                Charge chargeRefunded = openpayAPI.ChargeService.Refund(customer_id, charge_id, "refund desc");
                Error = "Be Happy";
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }
    }
}