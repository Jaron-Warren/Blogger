using System;
using System.Threading.Tasks;
using Blogger.Models;
using Blogger.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blogger.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class CommentsController : ControllerBase
  {
    private readonly CommentService _commentService;

    public CommentsController(CommentService commentService)
    {
      _commentService = commentService;
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Comment>> Create([FromBody] Comment newcomment)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newcomment.CreatorId = userInfo.Id;
        Comment comment = _commentService.Create(newcomment);
        return Ok(comment);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Comment>> EditAsync([FromBody] Comment updatedcomment, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedcomment.CreatorId = userInfo.Id;
        updatedcomment.Id = id;
        Comment comment = _commentService.Edit(updatedcomment);
        return Ok(comment);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Comment>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _commentService.Delete(id, userInfo.Id);
        return Ok("comment deleted");
      }
      catch (Exception err)
      {
        return (BadRequest(err.Message));
      }
    }
  }
}