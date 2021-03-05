using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace BookStore.Models.DTO
{
    [DataContract]
    public class BookDTO
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }
        [Required]
        [DataMember(Name="authorId")]
        public Guid AuthorId { get; set; }
        [Required]
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "releazeDate")]
        public DateTime ReleaseDate { get; set; }
        [DataMember(Name = "bookStatus")]
        public Guid? BookStatusId { get; set; }
    } 
}
