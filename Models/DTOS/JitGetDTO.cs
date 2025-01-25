namespace JitAPI.Models.DTOS
{
    public class JitGetDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public UserGetDTO User { get; set; }
}
}
