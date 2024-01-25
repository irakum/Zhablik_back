using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhablik.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserID { get; private set; }

    [Required]
    [MaxLength(255)]
    public string Username { get; set; }

    [Required]
    [MaxLength(255)]
    public string Password { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; }
    public bool Authenticated { get; set; }
    public int Coins { get; set; }
    public List<Assignment> Tasks { get; set; }
    public List<UserFrog> Frogs { get; set; }

    public User()
    {
        
    }
    public User(string username, string email, string password)
    {
        UserID = Guid.NewGuid();
        Username = username;
        Email = email;
        Password = password;
        Authenticated = false;
        Coins = 0;
        Tasks = new List<Assignment>();
        Frogs = new List<UserFrog>();
    }

    public bool Authenticate(string username, string password)
    {
        if (this.Username == username && this.Password == password)
        {
            Authenticated = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}