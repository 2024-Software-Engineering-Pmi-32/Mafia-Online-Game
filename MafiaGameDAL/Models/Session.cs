using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaGameDAL.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public required int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public User? Creator { get; set; }
    }
}
