namespace Vezeeta.API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        // ApiValidationError is a badRequest,
        // constructor chain with base => 400.
        public ApiValidationErrorResponse() : base(400)
        {
            Errors = new List<string>();    
        }
    }
}
