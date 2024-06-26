using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace wa_ral_shop.Models.Utilerias
{
    public class Info
    {
        public string GetIp()
        {
            string IpImg;
            try
            {
                IpImg = ConfigurationManager.AppSettings["IpImg"].ToString();
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
            return IpImg;
        }
        
        //public int GetTUsuario() 
        //{
        //    int TUi;
        //    try
        //    {
        //        TUi = Convert.ToInt32(ConfigurationManager.AppSettings["TUsuario"].ToString());
        //    }
        //    catch (Exception e)
        //    {
        //        throw new ArgumentException(e.Message);
        //    }
        //    return TUi;
        //}

    }
}