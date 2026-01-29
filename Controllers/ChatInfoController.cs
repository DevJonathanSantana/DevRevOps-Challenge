using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REVOPS.DevChallenge.Context.Entities;

namespace REVOPS.DevChallenge.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/[controller]")]
public class ChatInfoController() : ControllerBase
{
    /// <summary>
    /// This endpoint makes a request to Talk, searching for the chat with the specified <paramref name="chatId"/> and tells us if any attendant is assigned.
    /// </summary>
    [HttpGet(nameof(IsChatWithSomeone))]
    public async Task<ActionResult<bool>> IsChatWithSomeone(string chatId)
    {
        ChatInfo chatInfo;
        try
        {
            //var chatInfo = await chatInfoService.GetAsync(chatId);
            throw new NotImplementedException();

            if (chatInfo == null)
                return BadRequest("We could not find any chat for provided Id!");

            return Ok(chatInfo.IsAnyAttendantAssigned);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Unexpected error occurred while trying to conclude request: {e.Message}");
        }
    }
}
