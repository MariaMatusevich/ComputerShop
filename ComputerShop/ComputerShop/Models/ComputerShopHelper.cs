using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ComputerShop.Models
{
    public static class ComputerShopHelper
    {
        #region [ Functions ]

        public static void SendMail(string email, string text, bool useSignature)
        {
            string Signature = "\n\n\n--\nС уважением,\nComputerShop.\nТелефон: +375 (29) 789-22-00\nАдрес: г.Минск, ул.Карла Маркса-25";

            using (var mm = new MailMessage("computer-shop@vwf.by", email))
            {
                mm.Subject = "ComputerShop";
                mm.Body = GetWelcomeString() + "\n\n" + text;
                if(useSignature)
                {
                    mm.Body += Signature;
                }
                mm.IsBodyHtml = false;
                using (var sc = new SmtpClient("smtp.yandex.ru", 587))
                {
                    sc.EnableSsl = true;
                    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new NetworkCredential("computer-shop@vwf.by", "Aa1234");
                    sc.Send(mm);
                }
            }
        }

        public static string GetWelcomeString()
        {
            var hour = DateTime.Now.Hour;
            if (hour > 3 && hour < 12) return "Доброе утро!";
            if (hour > 11 && hour < 18) return "Добрый день!";
            if (hour > 17 && hour < 24) return "Добрый вечер!";
            if (hour > 23 || hour < 4) return "Доброй ночи!";

            return "";
        }

        public static string GetPriceFormat(int price)
        {
            return ((price).ToString("### ### ### ###.##"));
        }

        #endregion
    }

    #region [ Enums ]

    public enum EquipmentType
    {
        Computer,
        Notebook,
        Mouse,
        Monitor,
        Flash,
        HardDrive,
        UNKNOWN
    }

    public enum Status
    {
        InStock,
        Sold,
        PurchaseRequisition
    }

    public enum JsonResponseType
    {
        Success = 200,
        Error = 400
    }

    public enum OperationType
    {
        ToStock,
        Sold,
        PurchaseRequisition
    }

    #endregion
}