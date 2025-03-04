namespace Blog.Models;

public class UserLastCommentsCount
{
    public UserLastCommentsCount(string userName, int lastCommentsCount)
    {
        UserName = userName;
        LastCommentsCount = lastCommentsCount;
    }

    public string UserName { get; set; }
    public int LastCommentsCount { get; set; }
}