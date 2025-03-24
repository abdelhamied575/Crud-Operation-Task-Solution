namespace Crud_Operation_Task_API.Error
{
    public class ApiErrorResponse
    {
        
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageForStatusCode(statusCode);
        }


        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
            var message = statusCode switch
            {
                400 => "a bad request, you have made",
                401 => "You are Not Authorized",
                404 => "Resource Was Not Found",
                500 => "Server Error",
                _ => null
            };

            return message;
        }

    }
}
