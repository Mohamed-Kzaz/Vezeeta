using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API.Dtos.Account
{
    public class RegisterDoctorDto
    {
        public IFormFile? Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public int SpecializationId { get; set; }
        public string DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
    }
}
