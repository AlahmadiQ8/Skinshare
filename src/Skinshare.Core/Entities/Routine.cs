using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Skinshare.Core.Interfaces;

namespace Skinshare.Core.Entities
{
    public class Routine : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(140)]
        public string Title { get; set; }

        [MaxLength(280)]
        public string Description { get; set; }

        public string Identifier { get; set; }

        [Required]
        public IEnumerable<Step> Steps { get; set; }
    }
}