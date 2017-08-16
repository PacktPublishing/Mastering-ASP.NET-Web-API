namespace PersonalBudget.Models
{
    public class TestData
    {
        public static void AddTestData(PersonalBudgetContext context)
        {
            var testUser1 = new AppUser
            {
                Id = 1,
                UserName = "mithunvp",
                Password = "abcd123",
                IsSuperUser = true
            };

            var testUser2 = new AppUser
            {
                Id = 2,
                UserName = "Yogi",
                Password = "abcd123",
                IsSuperUser = false
            };

            context.AppUsers.AddRange(testUser1, testUser2);

            context.SaveChanges();
        }
    }
}
