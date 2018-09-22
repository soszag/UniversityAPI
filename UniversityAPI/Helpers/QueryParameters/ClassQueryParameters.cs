
namespace UniversityAPI.Helpers.QueryParameters
{

    public class ClassQueryParameters : QueryParametersBase
    {
        public string OrderBy { get; set; } = "Name";

        // Filtering fields
        public string ClassID { get; set; }
        public string Name { get; set; }
    }
}
