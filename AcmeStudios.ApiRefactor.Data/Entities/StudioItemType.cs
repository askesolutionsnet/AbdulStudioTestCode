using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcmeStudios.ApiRefactor.Data;

public class StudioItemType
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudioItemTypeId { get; set; }
    [Required]
    public string Value { get; set; }
    [JsonIgnore]
    public ICollection<StudioItem> StudioItem { get; set; }
}
