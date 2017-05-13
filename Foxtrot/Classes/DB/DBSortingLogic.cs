
namespace Classes
{
    /// <summary>
    /// Thomas Nielsen
    /// </summary>
    class DBSortingLogic
    {
        public static int? ConvertToNullableInt(object objectFromReader)
            //used to convert int to nullable int (int?)
            //using this method increases readability and understanding of what's going on in the code  
        {
            return objectFromReader.ToString().Equals("") ? null : (int?)int.Parse(objectFromReader.ToString());
        }

        public static float? ConvertToNullableFloat(object objectFromReader)
        //used to convert float to nullable float (float?)
        //using this method increases readability and understanding of what's going on in the code  
        {
            return objectFromReader.ToString().Equals("") ? null : (float?)float.Parse(objectFromReader.ToString());
        }
    }
}
