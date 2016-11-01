namespace Domain.Extensions
{
    public static class IntExtensions
    {
        public static int LeftoverAfterDividedBy3(this int source)
        {
            return source % 3;
        }

        public static int DividedBy3(this int source)
        {
            return source / 3;
        }

        public static int DividedBy3PlusLeftovers(this int source)
        {
            return source / 3 + source.LeftoverAfterDividedBy3();
        }
    }
}