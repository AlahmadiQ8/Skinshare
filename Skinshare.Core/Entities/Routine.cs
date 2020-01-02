using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Skinshare.Core.Interfaces;

namespace Skinshare.Core.Entities
{
    public class Routine : IEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Identifier { get; set; }

        public IEnumerable<Step> Steps { get; set; }
    }
}