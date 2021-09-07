using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;

namespace Blogger.Repositories
{
  public class CommentRepository
  {
    private readonly IDbConnection _db;

    public CommentRepository(IDbConnection db)
    {
      _db = db;
    }
    // internal List<Comment> Get()
    // {
    //   string sql = @"
    //   SELECT
    //   a.*,
    //   b.*
    //   FROM comments b
    //   JOIN accounts a ON CreatorId = a.id
    //   ";
    //   return _db.Query<Profile, Comment, Comment>(sql, (Profile, comments) =>
    //   {
    //     comments.CreatorId = Profile.Id;
    //     return comments;
    //   }, splitOn: "id").ToList();
    // }
    //REVIEW get comments by ID, does it work?
    internal Comment Get(int id)
    {
      string sql = @"
      SELECT
      a.*,
      c.*
      FROM comments c
      JOIN accounts a ON c.creatorId = a.id
      WHERE c.id = @id
      ";
      return _db.Query<Profile, Comment, Comment>(sql, (profile, comment) =>
      {
        comment.CreatorId = profile.Id;
        return comment;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }
    internal Comment Create(Comment newComment)
    {
      string sql = @"
      INSERT INTO comments
      (blog, body, creatorId)
      VALUES
      (@blog, @body, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newComment);
      return Get(id);
    }
    internal Comment Update(Comment updatedComment)
    {
      string sql = @"
      UPDATE comments
      SET
      blog = @Blog,
      body = @Body
      WHERE id = @Id;
      ";
      _db.Execute(sql, updatedComment);
      return Get(updatedComment.Id);
    }
    internal void Delete(int id)
    {
      string sql = "DELETE FROM comments WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}