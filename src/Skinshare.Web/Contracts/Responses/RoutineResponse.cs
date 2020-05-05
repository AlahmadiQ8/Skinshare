using System.Collections.Generic;

namespace Skinshare.Web.Contracts.Responses
{
    public class RoutineResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Identifier { get; set; }
        public IEnumerable<StepResponse> Steps { get; set; }
        public string Href { get; set; }
    }
}