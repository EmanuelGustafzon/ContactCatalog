namespace Infrastructure.Services
{
    public static class GenerateUniqueID
    {
        public static string Generate()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
