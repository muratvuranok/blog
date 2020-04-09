namespace blog.data.models
{ 
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    public class Category : CoreEntity
    { 
        [
            Required,
            MaxLength(50)
        ]
        public string Name { get; set; } 
        public virtual ICollection<Post> Posts { get; set; }
    }
}