using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OpenML.BookStore.Infrastructure.Helpers
{
    public static class DataTableToObject<T> where T : new()
    {

        // function that set the given object from the given data row
        private static void SetItemFromRow(T item, DataRow row)
        {
            try
            {
                // go through each column
                foreach (DataColumn c in row.Table.Columns)
                {
                    // find the property for the column
                    var p = item.GetType().GetProperty(c.ColumnName);

                    // if exists, set the value
                    if (p != null && row[c] != DBNull.Value)
                    {
                        p.SetValue(item, row[c], null);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow(DataRow row)
        {
            // create a new object
            var item = new T();

            // set the item
            SetItemFromRow(item, row);

            return item;
        }

        // function that creates a list of an object from the given data table
        public static List<T> CreateListFromTable(DataTable tbl)
        {
            return (from DataRow r in tbl.Rows select CreateItemFromRow(r)).ToList();
        }
    }

    public static class DataTableToObjectEnhanced<T> where T : new()
    {

        // function that set the given object from the given data row
        private static void SetItemFromRow(T item, DataRow row)
        {
            try
            {
                // go through each column
                foreach (DataColumn c in row.Table.Columns)
                {
                    // find the property for the column
                    var t = item.GetType();
                    var p = t.GetProperty(c.ColumnName);

                    // if exists, set the value
                    if (p != null && row[c] != DBNull.Value)
                    {
                        var rowType = row[c].GetType().Name;
                        var typeNullableSafe = Nullable.GetUnderlyingType(p.PropertyType);
                        var propType = typeNullableSafe?.Name;
                        var value = row[c];
                        
                        if (!string.IsNullOrEmpty(propType))
                        {
                            if (rowType != propType)
                            {
                                switch (propType)
                                {
                                    case "Decimal":
                                        value = Convert.ToDecimal(row[c]);
                                        break;
                                    case "Int32":
                                        value = Convert.ToInt32(row[c]);
                                        break;
                                } 
                            } 
                        }

                        p.SetValue(item, value, null);
                    }
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow(DataRow row)
        {
            // create a new object
            var item = new T();

            // set the item
            SetItemFromRow(item, row);

            return item;
        }

        // function that creates a list of an object from the given data table
        public static List<T> CreateListFromTable(DataTable tbl)
        {
            return (from DataRow r in tbl.Rows select CreateItemFromRow(r)).ToList();
        }
    }

    public static class CopyTypes
        {
        public static void CopyProperties(object objSource, object objDestination)
        {
            //get the list of all properties in the destination object
            var destProps = objDestination.GetType().GetProperties();

            //get the list of all properties in the source object
            foreach (var sourceProp in objSource.GetType().GetProperties())
            {
                foreach (var destProperty in destProps)
                {
                    //if we find match between source & destination properties name, set
                    //the value to the destination property
                    if (destProperty.Name == sourceProp.Name &&
                            destProperty.PropertyType.IsAssignableFrom(sourceProp.PropertyType))
                    {
                        destProperty.SetValue(destProps, sourceProp.GetValue(
                            sourceProp, new object[] { }), new object[] { });
                        break;
                    }
                }
            }
        }
    }
}
