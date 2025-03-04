using System;

namespace Blog.Models;

public class PostSortByLastCommentDate
{
    public PostSortByLastCommentDate(string postTitle, DateTime lastCommentCreatedDate, string text)
    {
        PostTitle = postTitle;
        LastCommentCreatedDate = lastCommentCreatedDate;
        Text = text;
    }

    public string PostTitle { get; set; }
    public DateTime LastCommentCreatedDate { get; set; }
    public string Text { get; set; }
}