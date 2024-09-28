using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Helpers
{
    public class DataTableParams
    {
        // Pagination properties
        public int Start { get; set; }          // Equivalent to iDisplayStart
        public int Length { get; set; }         // Equivalent to iDisplayLength

        // Global search property
        public string Search { get; set; }      // Global search term (sSearch)

        // Sorting properties (array of order parameters)
        public List<List<object>> Order { get; set; }   // Sorting order

        // Column properties (array of columns)
        public List<Column> Columns { get; set; }

        // Draw count (legacy DataTables property)
        public int Draw { get; set; }           // Optional: This can represent the sEcho equivalent
    }

    // Order class to handle sorting
    public class Order
    {
        public int Column { get; set; }         // Column index for sorting (iSortCol_0)
        public string Dir { get; set; }         // Sorting direction: "asc" or "desc" (sSortDir_0)
    }

    // Column class to handle column-specific properties
    public class Column
    {
        public string Data { get; set; }        // Column data field (sColumns)
        public bool Searchable { get; set; }    // Whether the column is searchable (searchable)
        public bool Orderable { get; set; }     // Whether the column is orderable (orderable)
        public string Search { get; set; }      // Column-specific search term
    }

}
