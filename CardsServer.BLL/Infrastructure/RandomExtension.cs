namespace CardsServer.BLL.Infrastructure
{
    public static class RandomExtension
    {
        public static int GenerateRecoveryCode()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000, 100000);

            return randomNumber;
        }
    }
}
