namespace UniversityAPI.Helpers.QueryParameters
{
    public class StudentQueryParameters : PersonQueryParam
    {
        public string OrderBy { get; set; } = "LastName";

    }
}
