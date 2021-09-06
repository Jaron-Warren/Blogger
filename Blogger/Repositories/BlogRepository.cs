using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blogger.Models;
using Dapper;

namespace Blogger.Repositories
{
  public class BlogRepository
  {
    private readonly IDbConnection _db;

    public BlogRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Blog> Get()
    {
      string sql = @"
      SELECT
      a.*,
      b.*
      FROM blogs b
      JOIN accounts a ON rCreatorId = a.id
      ";
      return _db.Query<Profile, Blog, Blog>(sql, (Profile, blogs) =>
      {
        blogs.CreatorId = Profile.Id;
        return blogs;
      }, splitOn: "id").ToList();
    }
    internal Blog Get(int id)
    {
      string sql = @"
      SELECT
      a.*,
      b.*
      FROM blogs b
      JOIN accounts a ON b.creatorId = a.id
      WHERE b.id = @id
      ";
      return _db.Query<Profile, Blog, Blog>(sql, (profile, blog) =>
      {
        blog.CreatorId = profile.Id;
        return blog;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }
    internal Blog Create(Blog newBlog)
    {
      string sql = @"
      INSERT INTO blogs
      (title, body, imgUrl, published, creatorId)
      VALUES
      (@title, @body, @imgUrl, @published, @creatorId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, newBlog);
      return Get(id);
    }
    internal Blog Update(Blog updatedBlog)
    {
      string sql = @"
      UPDATE blogs
      SAET
      title = @Title,
      body = @Body,
      imgUrl = @imgUrl,
      published = @published,
      WHERE id = @Id;
      ";
      _db.Execute(sql, updatedBlog);
      return Get(updatedBlog.Id);
    }
    internal void Delete(int id)
    {
      string sql = "DELETE FROM blogs WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}