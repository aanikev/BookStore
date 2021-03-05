using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
       public Guid Id { get; set; }
       public string Name { get; set; }
       public string Surname { get; set; }
       public string LastName { get; set; }
       public string FullName { get; set; }
       public DateTime DateBirth { get; set; }
        
       public List<Book>Books { get; set; }
    }
}
