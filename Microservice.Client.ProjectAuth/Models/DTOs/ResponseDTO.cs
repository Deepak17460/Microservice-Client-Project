namespace Microservice.Client.ProjectAuth.Models.DTOs
{
    public class ResponseDTO
    {
        public bool IsSuccess { get; set; }

        public string? Message { get; set; }

        public string? Token { get; set; }
        public object? User { get; set; }
    }
}
