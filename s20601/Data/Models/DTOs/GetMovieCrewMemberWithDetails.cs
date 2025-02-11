namespace s20601.Data.Models.DTOs
{
    public class GetMovieCrewMemberWithDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Job { get; set; } = null!;
        public string? CharacterName { get; set; }
    }
}
