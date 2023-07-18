using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Models;

public class StudioItemHeaders
{
    public int StudioItemId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
}
