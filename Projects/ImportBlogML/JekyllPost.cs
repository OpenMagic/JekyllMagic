using System;
using System.IO;
using System.Linq;
using System.Text;
using Utilities.FileFormats.BlogML;

namespace ImportBlogML
{
    public static class JekyllPost
    {
        public static void Convert(Post post, string outputFolder)
        {
            var postFileName = Path.Combine(outputFolder, GetPostFileName(post));

            Console.WriteLine("Writing {0}...", postFileName);

            var text = new StringBuilder();

            text.AppendLine("---");
            text.AppendLine("layout: post");
            text.AppendLine("title: " + post.Title);

            if (post.Tags != null && post.Tags.TagList != null && post.Tags.TagList.Any())
            {
                throw new System.Exception("not tested");
                text.AppendLine("tags:");

                foreach (var tag in post.Tags.TagList)
                {
                    text.AppendLine("- " + tag.REF);
                }                    
            }
            text.AppendLine("---");
            text.AppendLine(post.Content);

            File.WriteAllText(postFileName, text.ToString());
        }

        private static string GetPostFileName(Post post)
        {
            var fileName = post.PostURL.ToLowerInvariant();

            // Remove /post/ from start of file name.
            fileName = fileName.Substring(6);

            // Remove .aspx file extension.
            fileName = fileName.Substring(0, fileName.Length - 5);

            fileName = fileName.Replace("/", "-") + ".md";

            return fileName;
        }
    }
}
