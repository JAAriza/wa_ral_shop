using Openpay;
using Openpay.Entities;
using Openpay.Entities.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Utilerias
{
    public class OpenPay
    {
        //Cliente
        //OpenPay openP = new OpenPay();
        //openP.
        //OpenpayAPI.ChargeService.Create(string customer_id, ChargeRequest request);

        //Comercio
        //openpayAPI.ChargeService.Create(ChargeRequest request);
        //Ejemplo de petición con cliente

        //curl https://sandbox-api.openpay.mx/v1/mzdtln0bmtms6o3kck8f/charges\-u sk_e568c42a6c384b7ab02cd47d2e407cab:
//        OpenpayAPI openpayAPI = new OpenpayAPI("sk_3433941e467c4875b178ce26348b0fac", "moiep6umtcnanql3jrxp", "MX", false);

//        Customer customer = new Customer();
//        //Customer.
//        customer.Name = "Net Client";
//customer.LastName = "C#";
//customer.Email = "net@c.com";
//customer.Address = new Address();
//        customer.Address.Line1 = "line 1";
//customer.Address.PostalCode = "12355";
//customer.Address.City = "Queretaro";
//customer.Address.CountryCode = "MX";
//customer.Address.State = "Queretaro";
// CustomerService customerService = openpayAPI.CustomerService;
//        Customer customerCreated = customerService.Create(customer);

        //Sandbox
        //OpenpayAPI openpayAPI = new OpenpayAPI("sk_3433941e467c4875b178ce26348b0fac", "moiep6umtcnanql3jrxp");
        //openpayAPI.Production = false; // Default value = false

        //Produccion
        //OpenpayAPI openpayAPI = new OpenpayAPI("sk_3433941e467c4875b178ce26348b0fac", "moiep6umtcnanql3jrxp");
        //openpayAPI.Production = true;

        /*OpenpayAPI api = new OpenpayAPI("sk_b05586ec98454522ac7d4ccdcaec9128", "maonhzpqm8xp2ydssovf");
                ChargeRequest request = new ChargeRequest();
                request.Method = "bank_account";
        request.Amount = new Decimal(100.00);
                request.Description = "Cargo con banco";
        request.OrderId = "oid-00053";

        Charge charge = api.ChargeService.Create("ag4nktpdzebjiye1tlze", request);
        */
    }
}