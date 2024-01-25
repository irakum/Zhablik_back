using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zhablik.Models;

public class FrogInfo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid FrogInfoID { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public int Price { get; set; }

    [Required]
    public int UpgradePrice { get; set; }

    public FrogInfo()
    {
        
    }

    public FrogInfo(string title, string description)
    {
        FrogInfoID = Guid.NewGuid();
        Title = title;
        Description = description;
        Price = 100;
        UpgradePrice = 70;
    }
}