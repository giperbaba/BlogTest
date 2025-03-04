namespace Blog.Models;

public class UserCommentCount
{
    public UserCommentCount(string userName, int commentCount)
    {
        UserName = userName;
        CommentCount = commentCount;
    }

    public string UserName { get; set; }
    public int CommentCount { get; set; }
}