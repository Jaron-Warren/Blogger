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
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly BlogService _blogService;

    public AccountController(AccountService accountService, BlogService blogService)
    {
      _accountService = accountService;
      _blogService = blogService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    //REVIEW i need help
    // [HttpGet("blogs")]
    // [Authorize]
    // public async Task<ActionResult<Blog>> GetBlogs()
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     return Ok(_blogService.GetAccountBlogs(userInfo.Id));
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
    [HttpPut]
    [Authorize]
    //REVIEW picture value can't be null or it breaks
    public async Task<ActionResult<Account>> EditAsync([FromBody] Account updatedAccount)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedAccount.Id = userInfo.Id;
        Account account = _accountService.Edit(updatedAccount, userInfo.Email);
        return Ok(account);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
  }


}