using System.ComponentModel.DataAnnotations;
using Skinshare.Core.Entities;

namespace Skinshare.Web.Contracts.Requests
{
    public class StepRequest
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public PartOfDay PartOfDay { get; set; }
    }
}