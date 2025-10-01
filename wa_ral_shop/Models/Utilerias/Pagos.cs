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
    /// <summary>
    /// Clase Pagos que contiene los metodos que brinda la api de pagos OpenPay
    /// </summary>
    public class Pagos
    {
        // De acuerdo a documentación dice que esto es lo que se puede hacer
        //openpayAPI.CustomerService
        //openpayAPI.CardService
        //openpayAPI.BankAccountService
        //openpayAPI.ChargeService
        //openpayAPI.TransferService
        //openpayAPI.PayoutService
        //openpayAPI.FeeService
        //openpayAPI.PlanService
        //openpayAPI.SubscriptionService

        OpenpayAPI openpayAPI = new OpenpayAPI("sk_cb8fb0bed1d147ef9c408686f7221356", "mcpqgoe22dmeeukfcajw", "MX", false);

        /// <summary>
        /// Crear Cliente en la aplicación, se requiere el objeto cliente ya que se tiene la información y no pedirla de nuevo
        /// </summary>
        /// <param name="clienteAnonymous"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Consultar cliente en especifico, esta información se almacena en BD ya que se actualiza cuando se da de alta
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public Customer ConsultarCliente(string CustomerId)
        {
            //string customer_id = "a6fqqdzialiolrzijqid";
            Customer customer = new Customer();
            customer = openpayAPI.CustomerService.Get(CustomerId);
            return customer;
        }

        /// <summary>
        /// Borrar cliente, se necesita ID que se actualizó al guardar cliente en OpenPay
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Actualizar cliente, se necesita ID que se actualizó al guardar cliente de OpenPay
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retorna LIST<Customer> puede manipular el numero de registros que retornara
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Crear Cargo, puede ser a cliente en especifico o puede ser como en general la creación del cargo, tiene 2 sobrecargas
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Prepara cargo pero digamos que esta inactivo
        /// </summary>
        /// <returns></returns>
        public string ReembolsarCargo()
        {
            string Error = string.Empty;
            try
            {
                string customer_id = "adyytoegxm6boiusecxm";
                string charge_id = "ttcg5roe2py2bur38cx2";

                Charge chargeRefunded = openpayAPI.ChargeService.Refund(customer_id, charge_id, "refund desc");
                Error = "Be Happy";

                //ooooo
                //string customer_id = "adyytoegxm6boiusecxm";
                //string charge_id = "ttcg5roe2py2bur38cx2";
                //RefundRequest refundRequest = new RefundRequest();
                //refundRequest.Description = "refund desc";

                //Charge chargeRefunded = openpayAPI.ChargeService.RefundWithRequest(customer_id, charge.Id, refundRequest);

            }
            catch (Exception ex)
            {
                Error = ex.Message;
                throw;
            }
            return Error;
        }

        /// <summary>
        /// Crear cargo para transferencia bancaria
        /// </summary>
        /// <returns></returns>
        public string CrearCargoaPagarPorTransferencia()
        {
            string customer_id = "adyytoegxm6boiusecxm";
            string Error = string.Empty;

            try
            {
                ChargeRequest request = new ChargeRequest();
                request.Method = "bank_account";
                request.Description = "Testing from .Net [BankAccount]";
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



    }
}