using System;
using Blogger.Models;
using Blogger.Repositories;

namespace Blogger.Services
{
  public class CommentService
  {
    private readonly CommentRepository _repo;

    public CommentService(CommentRepository repo)
    {
      _repo = repo;
    }
    //REVIEW get comments by ID, does it work?
    internal Comment Get(int id)
    {
      Comment comments = _repo.Get(id);
      if (comments == null)
      {
        throw new Exception("Invalid Id");
      }
      return comments;
    }

    internal Comment Create(Comment newComment)
    {
      return _repo.Create(newComment);
    }
    internal Comment Edit(Comment updatedComment)
    {
      Comment original = Get(updatedComment.Id);
      if (updatedComment.CreatorId != original.CreatorId)
      {
        throw new Exception("you can't edit comments that aren't your's!");
      }
      updatedComment.CreatorId = updatedComment.CreatorId != null ? updatedComment.CreatorId : original.CreatorId;
      updatedComment.Body = updatedComment.Body != null ? updatedComment.Body : original.Body;
      updatedComment.Blog = updatedComment.Blog != 0 ? updatedComment.Blog : original.Blog;
      return _repo.Update(updatedComment);
    }

    internal void Delete(int commentId, string userId)
    {
      Comment commentToDelete = Get(commentId);
      if (commentToDelete.CreatorId != userId)
      {
        throw new Exception("you do not have permission to Delete");
      }
      _repo.Delete(commentId);
    }
  }
}