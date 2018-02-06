using Microsoft.AspNetCore.Identity;

namespace BooksWebApi.Entities
{
  public class User : IdentityUser
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
}
