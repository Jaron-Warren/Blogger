using System;
using System.Collections.Generic;
using Blogger.Models;
using Blogger.Repositories;

namespace Blogger.Services
{
  public class BlogService
  {
    private readonly BlogRepository _repo;

    public BlogService(BlogRepository repo)
    {
      _repo = repo;
    }

    internal List<Blog> Get()
    {
      return _repo.Get();
    }

    internal Blog Get(int id)
    {
      Blog blog = _repo.Get(id);
      if (blog == null)
      {
        throw new Exception("Invalid Id");
      }
      return blog;
    }
    //REVIEW help!!
    // internal List<Blog> GetAccountBlogs(String id)
    // {
    //   List<Blog> blogs = _repo.GetAccountBlogs(id);
    // if (blogs == null)
    // {
    //   throw new Exception("Invalid Id");
    // }
    //REVIEW why doesn't this work?
    // return List<Blog> blogs;
    // }

    internal Blog Create(Blog newBlog)
    {
      return _repo.Create(newBlog);
    }
    internal Blog Edit(Blog updatedBlog)
    {
      Blog original = Get(updatedBlog.Id);
      if (updatedBlog.CreatorId != original.CreatorId)
      {
        throw new Exception("you can't edit blogs that aren't your's!");
      }
      updatedBlog.Title = updatedBlog.Title != null ? updatedBlog.Title : original.Title;
      updatedBlog.Body = updatedBlog.Body != null ? updatedBlog.Body : original.Body;
      updatedBlog.imgUrl = updatedBlog.imgUrl != null ? updatedBlog.imgUrl : original.imgUrl;
      updatedBlog.published = updatedBlog.published != null ? updatedBlog.published : original.published;
      updatedBlog.CreatorId = updatedBlog.CreatorId != null ? updatedBlog.CreatorId : original.CreatorId;
      return _repo.Update(updatedBlog);
    }

    internal void Delete(int blogId, string userId)
    {
      Blog blogToDelete = Get(blogId);
      if (blogToDelete.CreatorId != userId)
      {
        throw new Exception("you do not have permission to Delete");
      }
      _repo.Delete(blogId);
    }
  }
}