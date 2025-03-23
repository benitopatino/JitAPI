using JitAPI.Models.DTOS;

namespace JitAPI.Models.Interface;

public interface INewsfeedService
{
    /// <summary>
    /// Return a list of newsfeed items for the user.
    /// </summary>
    /// <param name="userId">The ID of the user whose newsfeed will be returned.</param>
    /// <returns></returns>
    IEnumerable<NewsfeedItemDTO> GetNewsfeed(Guid userId);
}
