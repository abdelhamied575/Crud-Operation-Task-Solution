namespace Crud_Operation_Task_API.Error
{
    public class ApiValidationErrorResponse:ApiErrorResponse
    {

        public IEnumerable<string> Errors { get; set; } = new List<string>();

        public ApiValidationErrorResponse() : base(400)
        {

        }

    }
}
