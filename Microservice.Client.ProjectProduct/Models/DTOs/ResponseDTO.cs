namespace Microservice.Client.ProjectProduct.Models.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
