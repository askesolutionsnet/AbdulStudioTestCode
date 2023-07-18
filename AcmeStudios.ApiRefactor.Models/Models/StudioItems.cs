using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Models;

public class StudioItems
{
   // public int StudioItemId { get; set; }
    public DateTime Acquired { get; set; }
    public DateTime? Sold { get; set; } 
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; } 
    [Required]
    public string SerialNumber { get; set; } 
    public decimal Price { get; set; }
    public decimal? SoldFor { get; set; } 
    public bool Eurorack { get; set; } 
    public int StudioItemTypeId { get; set; }
}
