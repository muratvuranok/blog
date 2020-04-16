namespace blog.data.models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Post : CoreEntity
    {
        [
            Required,
            MaxLength(100)
        ]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        public string FullName { get; set; }

        //navigation Property

       public virtual ICollection<PostCategory> PostCategories { get; set; }
        public virtual ICollection<PostImage> PostImage { get; set; }
    }
}