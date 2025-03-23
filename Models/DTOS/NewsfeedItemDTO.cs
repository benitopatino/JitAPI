namespace JitAPI.Models.DTOS;

public class NewsfeedItemDTO(
    Guid jitId,
    string jitContent,
    DateTime jitDateCreated,
    Guid authorUserId,
    string authorFullName)
{
    public Guid JitId { get; set; } = jitId;
    public string JitContent { get; set; } = jitContent;
    public DateTime JitDateCreated { get; set; } = jitDateCreated;
    public Guid AuthorUserId { get; set; } = authorUserId;
    public string AuthorFullName { get; set; } = authorFullName;
}