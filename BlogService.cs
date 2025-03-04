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
        return await context.BlogPosts
            .Where(p => p.Comments.Any()) 
            .Select(p => new 
            { 
                PostTitle = p.Title, 
                LastComment = p.Comments.OrderByDescending(c => c.CreatedDate).FirstOrDefault()
            })
            .OrderByDescending(p => p.LastComment.CreatedDate)
            .Select(p => new PostSortByLastCommentDate(p.PostTitle, p.LastComment.CreatedDate, p.LastComment.Text))
            .ToListAsync();
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

