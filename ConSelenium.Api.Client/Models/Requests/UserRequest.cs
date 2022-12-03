namespace ConSelenium.Api.Client.Models.Requests
{
    public class UserRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Address? Address { get; set; }
    }
}
