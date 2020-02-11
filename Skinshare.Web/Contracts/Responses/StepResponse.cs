using Skinshare.Core.Entities;

namespace Skinshare.Web.Contracts.Responses
{
    public class StepResponse
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public PartOfDay PartOfDay { get; set; }
    }
}