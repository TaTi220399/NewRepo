using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CodeFirstDemo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create and save a new Blog

                /*Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();*/

                // Display all Blogs from the database
                

                Console.WriteLine("All blogs in the database:");
                ShowAllBlogs(db);

                Console.WriteLine("\nChoose an action (enter some number):\n1. Add a blog\n2. Update a blog\n3. Delete a blog\n4. Show all blogs\n\nPress another key to exit...");
                string answer = Console.ReadLine();
                while (new List<string> { "1", "2", "3", "4" }.Contains(answer))
                {
                    if (answer == "1")
                    {
                        Console.Write("Enter a name for a new Blog: ");
                        var name = Console.ReadLine();

                        var blog = new Blog { Name = name };
                        db.Blogs.Add(blog);
                        db.SaveChanges();
                    }
                    else if (answer == "2")
                    {
                        Console.WriteLine("Enter Id of blog: ");
                        Console.WriteLine("Blog's list:");
                        ShowAllBlogs(db);
                        var id = Convert.ToInt32(Console.ReadLine()); 
                        var blogObj = db.Blogs;
                        Blog blog = null;
                        try
                        {
                            blog = blogObj
                            // Загрузить блог под id
                            .Where(c => c.BlogId == id)
                            .First();
                        }
                        catch 
                        {
                            Console.WriteLine("Data could not be found. ID entered incorrectly");
                        }
                        // Внести изменения
                        if (blog != null) 
                        {
                            Console.Write("Enter a new name for a Blog №{0}: ", id);
                            blog.Name = Console.ReadLine();

                            db.SaveChanges();
                            Console.WriteLine("Update is complited.");
                        }
                        
                    }
                    else if (answer == "3")
                    {
                        Console.WriteLine("Enter Id of blog: ");
                        Console.WriteLine("Blog's list:");
                        ShowAllBlogs(db);
                        var id = Convert.ToInt32(Console.ReadLine());
                        var blogObj = db.Blogs;
                        Blog blog = null;
                        try
                        {
                            blog = blogObj
                            // Загрузить блог под id
                            .Where(c => c.BlogId == id)
                            .First();
                        }
                        catch
                        {
                            Console.WriteLine("Data could not be found. ID entered incorrectly");
                        }
                        // Внести изменения
                        if (blog != null)
                        {
                            blog = blogObj
                            .Where(b => b.BlogId == id)
                            .FirstOrDefault();

                            blogObj.Remove(blog);

                            db.SaveChanges();
                            Console.WriteLine("Delete is complited.");
                        }
                    }
                    else if (answer == "4")
                    {
                        ShowAllBlogs(db);
                    }

                    Console.WriteLine("\nChoose an action (enter some number):\n1. Add a blog\n2. Update a blog\n3. Delete a blog\n4. Show all blogs\n\nPress another key to exit...");
                    answer = Console.ReadLine();
                }

            }

        }

        static void ShowAllBlogs(BloggingContext db)
        {
            var query = from b in db.Blogs
                        orderby b.BlogId
                        select b;

            foreach (var item in query)
            {
                Console.WriteLine(item.BlogId + " " + item.Name);
            }
        }

        public class Blog
        {
            public int BlogId { get; set; }
            public string Name { get; set; }

            public virtual List<Post> Posts { get; set; }
        }

        public class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }

            public int BlogId { get; set; }
            public virtual Blog Blog { get; set; }
        }
        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }
        }
    }
}
