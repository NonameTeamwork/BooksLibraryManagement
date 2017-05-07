using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class AdminStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TransactionStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
