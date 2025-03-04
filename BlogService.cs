using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog;

public static class BlogService
{
    public static async Task<IList<UserCommentCount>> NumberOfCommentsPerUser(MyDbContext context)
    {
        var comments = await context.BlogComments
            .GroupBy(c => c.UserName)
            .Select(c => new UserCommentCount(c.Key, c.Count()))
            .ToListAsync();
        return comments;
        
    }
    
    public static async Task<IList<PostSortByLastCommentDate>> PostsOrderedByLastCommentDate(MyDbContext context)
    { 
        var posts = await context.BlogPosts
            .Where(p => p.Comments.Any()) 
            .Select(p => new PostSortByLastCommentDate(
                p.Title, 
                p.Comments.OrderByDescending(c => c.CreatedDate).FirstOrDefault().CreatedDate,
                p.Comments.OrderByDescending(c => c.CreatedDate).FirstOrDefault().Text))
            .ToListAsync();
        return posts.OrderByDescending(p => p.LastCommentCreatedDate).ToList();
    }

    public static async Task<IList<UserLastCommentsCount>> NumberOfLastCommentsLeftByUser(MyDbContext context)
    { 
        return await context.BlogPosts
            .Where(p => p.Comments.Any()) 
            .Select(p => new 
            { 
                UserName = p.Comments.OrderByDescending(c => c.CreatedDate).FirstOrDefault().UserName
            })
            .GroupBy(p => p.UserName)
            .Select(g => new UserLastCommentsCount(g.Key, g.Count()))
            .ToListAsync();
    }
}

