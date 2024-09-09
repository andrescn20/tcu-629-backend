
namespace DTO
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string ResetUrl { get; set; }
    }

    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
