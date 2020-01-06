using System.ComponentModel.DataAnnotations;
using Skinshare.Core.Interfaces;

namespace Skinshare.Core.Entities
{
    public class Step : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public int Order { get; set; }

        [Required]
        [MaxLength(10)]
        public string PartOfDay { get; set; }

        public int RoutineId { get; set; }

        public Routine Routine { get; set; }
    }
}