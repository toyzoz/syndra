namespace GoodRandom
{
    public static class MyRandom
    {
        public static string GetRandom()
        {
            return new Random().Next(1, 10) + "hello";
        }
    }
}
