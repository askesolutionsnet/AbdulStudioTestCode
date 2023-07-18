using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeStudios.ApiRefactor.Models;

public class StudioItemTypes
{
    public int StudioItemTypeId { get; set; }
    [Required]
    public string Value { get; set; }
}
