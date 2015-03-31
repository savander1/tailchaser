namespace TailChaser.UI
{
    internal static class DoubleExtensions
    {
        internal static bool Between(this double toTest, int lowerBound, int upperBound, bool fullOpenRange = false)
        {
            return fullOpenRange ? toTest >= lowerBound && toTest <= upperBound : toTest >= lowerBound && toTest < upperBound;
        }
    }
}