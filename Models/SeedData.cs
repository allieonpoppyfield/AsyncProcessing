namespace AsyncProcessing.Models;

public static class SeedData
{
    private static Guid FirstUserGuid = new("95982d58-9ee5-47bf-9c02-06b14c7804fd");
    private static Guid SecondUserGuid = new("0427eebe-454f-42b9-bd1e-6c7c6c68f4a9");
    public static void EnsurePopulated(IApplicationBuilder app)
    {
        ApplicationDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();

        if (!context.Users.Any())
        {
            context.Users.AddRange
            (
                new()
                {
                    Id = FirstUserGuid
                },
                new()
                {
                    Id = SecondUserGuid
                }
            );
            context.SaveChanges();
            Random gen = new Random();
            DateTime start = new DateTime(2022, 4, 1);
            for (int i = 0; i < 50; i++)
            {
                int range = (DateTime.Today - start).Days;
                context.UserSessions.AddRange
                (
                    new UserSession()
                    {
                        Id = Guid.NewGuid(),
                        UserId = FirstUserGuid,
                        CreatedDate = start.AddDays(gen.Next(range))

                    },
                    new UserSession()
                    {
                        Id = Guid.NewGuid(),
                        UserId = SecondUserGuid,
                        CreatedDate = start.AddDays(gen.Next(range))
                    }
                );
            }
            context.SaveChanges();
        }
    }
}
