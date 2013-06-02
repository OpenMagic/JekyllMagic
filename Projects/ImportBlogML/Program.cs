using System;
using System.IO;
using System.Linq;
using Utilities.FileFormats.BlogML;

namespace ImportBlogML
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var rootFolder = Directory.GetCurrentDirectory();

#if DEBUG
                rootFolder = @"C:\Users\Tim\Code\tim.26tp.com\import";
#endif

                var importFileName = Path.Combine(rootFolder, "BlogML.xml");
                var outputFolder = Path.Combine(rootFolder, "_posts");

#if DEBUG
                if (Directory.Exists(outputFolder))
                {
                    Directory.Delete(outputFolder, true);
                }
#endif

                Validate(outputFolder, importFileName);
                
                var blogML = new BlogML(importFileName);

                foreach (var post in blogML.Posts.PostList)
                {
                    JekyllPost.Convert(post, outputFolder);
                }                
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(ex.ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void Validate(string outputFolder, string importFileName)
        {
            if (!File.Exists(importFileName))
            {
                throw new FileNotFoundException(string.Format("Import file '{0}' not found.", importFileName), importFileName);
            }

            if (Directory.Exists(outputFolder))
            {
                if (Directory.EnumerateFiles(outputFolder).Any())
                {
                    throw new InvalidOperationException(string.Format("Output folder '{0}' must be empty.", outputFolder));
                }
            }
            else
            {
                Directory.CreateDirectory(outputFolder);
            }
        }
    }
}
