using System.ComponentModel.DataAnnotations;

namespace Shared;

public class Owner
{
    [Key]
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string Job { get; set; }
    public int Age { get; set; }

    public List<House> Houses { get; set; }
    
}   

