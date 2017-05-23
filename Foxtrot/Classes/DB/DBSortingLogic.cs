
using System.Data;
using Foxtrot.GUI;

namespace Foxtrot.Classes.DB
{
    /// <summary>
    /// Jonas Lykke & Thomas Nielsen
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

        public static bool DupeCheckCombiProductDataTable(int? productID, DataTable inputTable)
        {
            foreach (DataRow row in inputTable.Rows)
            {
                if (row[0].ToString().Contains(productID.ToString()))
                {
                    GUISortingLogic.Message("Produktet findes Allerede på Listen!");
                    return true;
                }
            }

            return false;
        }
    }
}
