using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhablik.Models;

public class UserFrog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid UserFrogId { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    [ForeignKey("FrogInfoID")]
    public Guid FrogInfoId { get; set; }
    public User User { get; set; }
    public FrogInfo FrogInfo { get; set; }
    
    [Required]
    [Range(1, 5)]
    public int Level { get; set; }

    public UserFrog()
    {
        
    }

    public UserFrog(User user, FrogInfo frogInfo)
    {
        User = user;
        FrogInfo = frogInfo;
        UserFrogId = Guid.NewGuid();
        UserId = user.UserID;
        Level = 1;
    }
}