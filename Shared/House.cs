using System.ComponentModel.DataAnnotations;

namespace Shared;

public class House
{
    [Key]
    public int HouseId { get; set; }
    public string Adress { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public bool Sold { get; set; }
    public decimal Price { get; set; }
    
    public int OwnerId { get; set; }
    



}