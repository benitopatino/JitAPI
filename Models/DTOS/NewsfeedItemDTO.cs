namespace JitAPI.Models.DTOS;

public class NewsfeedItemDTO(
    Guid jitId,
    string jitContent,
    DateTime jitDateCreated,
    Guid authorUserId,
    string authorFullName,
    string avatarUrl,
    string userName)
{
    public Guid Id { get; set; } = jitId;
    public string Content { get; set; } = jitContent;
    public DateTime DateCreated { get; set; } = jitDateCreated;
    public Guid AuthorUserId { get; set; } = authorUserId;
    public string AuthorFullName { get; set; } = authorFullName;
    public string AvatarUrl { get; set; } = avatarUrl;
    public string UserName { get; set; } = userName;
}