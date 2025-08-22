using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFcoreDemo.Models.Domain
{
    public class Post 
    {
        [Key]   
        public int PostId { get; set; }
        [StringLength(256)]
        [Required]
        public string? Title { get; set; }

        public string? Content { get; set; }

        public int BlogId { get; set; }

        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; } = null!;
    }
}
