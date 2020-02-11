using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Skinshare.Core.Entities;

namespace Skinshare.Web.Contracts.Requests
{
    public class RoutineRequest
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public IEnumerable<StepRequest> Steps { get; set; }
    }
}