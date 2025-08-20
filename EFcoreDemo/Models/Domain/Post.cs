using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
