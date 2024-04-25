using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EcommerceApi.Models.Database
{
    public class BDEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("newsequentialid()")]
        public Guid id { get; set; }
    }
}
