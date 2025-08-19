using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EFcoreDemo.Models.Common;

namespace EFcoreDemo.Models.Domain
{
    public class Post 
    {
        public int PostId { get; set; }
        [StringLength(256)]
        [Required]
        public string? Title { get; set; }

        public string? Content { get; set; }

        public int BlogId { get; set; }

        //navigation property using m-1
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; } = null!;
    }
}
