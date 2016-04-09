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
}