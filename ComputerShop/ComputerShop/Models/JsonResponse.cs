using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerShop.Models
{
    public class JsonResponse
    {
        public JsonResponseType Type { get; set; }
        public string Message { get; set; }

        public JsonResponse(JsonResponseType type, string message)
        {
            Type = type;
            Message = message;
        }
    }

    public enum JsonResponseType
    {
        Success = 200,
        Error = 400
    }
}