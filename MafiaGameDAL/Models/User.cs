using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MafiaGameDAL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string PasswordHash { get; set; }
        public int? SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session? Session { get; set; }
        public string? Role { get; set; }
    }
}
