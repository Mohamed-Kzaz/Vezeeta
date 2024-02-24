using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Dashboard
{
    public class DoctorDto
    {
        public string Id { get; set; }
        public string? ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Specialization { get; set; }
        public Gender Gender { get; set; }
        public string DateOfBirth { get; set; }
    }
}
