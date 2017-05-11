
namespace Classes
{
    /// <summary>
    /// Thomas Nielsen
    /// </summary>
    class DBSortingLogic
    {
        public static int? ConvertToNullableInt(object objectFromReader)
        {
            return objectFromReader.ToString().Equals("") ? null : (int?)int.Parse(objectFromReader.ToString());
        }

        public static float? ConvertToNullableFloat(object objectFromReader)
        {
            return objectFromReader.ToString().Equals("") ? null : (float?)float.Parse(objectFromReader.ToString());
        }
    }
}
