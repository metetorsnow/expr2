using Microsoft.EntityFrameworkCore;

namespace expr2.Pages.Models;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new ExprContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<ExprContext>>()))
        {
            if (context == null || context.Items == null)
            {
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }

            // Look for any movies.
            if (context.Items.Any())
            {
                return;   // DB has been seeded
            }

            context.Items.AddRange(
                new Item
                {
                    ItemId = 1,
                    ItemName = "圆珠笔",
                    CategoryId = "文具",
                    Origin = "江苏",
                    Specification="0.5mm",
                    Model = "黑色",
                    ItemImage = null
                },

                new Item
                {
                    ItemId = 2,
                    ItemName = "钢笔",
                    CategoryId = "文具",
                    Origin = "江苏",
                    Specification = "0.5mm",
                    Model = "黑色",
                    ItemImage = null
                },

                new Item
                {
                    ItemId = 3,
                    ItemName = "美工刀",
                    CategoryId = "刀具",
                    Origin = "河南",
                    Specification = "0.5mm",
                    Model = "白色",
                    ItemImage = null
                },

                new Item
                {
                    ItemId = 4,
                    ItemName = "A4纸",
                    CategoryId = "文具",
                    Origin = "江苏",
                    Specification = "-",
                    Model = "-",
                    ItemImage = null
                }
            );
            context.SaveChanges();
        }
    }
}
