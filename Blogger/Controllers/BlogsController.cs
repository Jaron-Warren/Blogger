using System;
using System.Collections.Generic;
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
  public class BlogsController : ControllerBase
  {
    private readonly BlogService _blogService;

    public BlogsController(BlogService blogService)
    {
      _blogService = blogService;
    }
    [HttpGet]
    public ActionResult<List<Blog>> Get()
    {
      try
      {
        List<Blog> blogs = _blogService.Get();
        return Ok(blogs);
      }
      catch (Exception error)
      {
        return BadRequest(error.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Blog> Get(int id)
    {
      try
      {
        Blog blog = _blogService.Get(id);
        return Ok(blog);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    // [HttpGet("{id}/comments")]
    // public ActionResult<Blog> Get(int id)
    // {
    //   try
    //   {
    //     List<Comment> comments = _commentService.Get(id);
    //     return Ok(comments);
    //   }
    //   catch (Exception err)
    //   {
    //     return BadRequest(err.Message);
    //   }
    // }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Blog>> Create([FromBody] Blog newBlog)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newBlog.CreatorId = userInfo.Id;
        Blog blog = _blogService.Create(newBlog);
        return Ok(blog);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]

    public async Task<ActionResult<Blog>> EditAsync([FromBody] Blog updatedBlog, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedBlog.CreatorId = userInfo.Id;
        updatedBlog.Id = id;
        Blog blog = _blogService.Edit(updatedBlog);
        return Ok(blog);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Blog>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _blogService.Delete(id, userInfo.Id);
        return Ok("blog post deleted");
      }
      catch (Exception err)
      {
        return (BadRequest(err.Message));
      }
    }
  }
}

// GET: '/api/blogs' Returns all pubished blogs
// GET: '/api/blogs/:id' Returns blog by Id
// GET: '/api/blogs/:id/comments' Returns comments for a blog
// POST: '/api/blogs' Create new Blog *
// PUT: '/api/blogs/:id' Edits Blog **
// DELETE: '/api/blogs/:id' Deletes Blog **