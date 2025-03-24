namespace Crud_Operation_Task_API.Error
{
    public class ApiExceptionResponse:ApiErrorResponse
    {

        public string? Details { get; set; }


        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null) 
               : base(statusCode, message)
        {
            Details = details;
        }

    }
}
