using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MagellanTest.Model
{
    public class Item
    {        
        public int Id { get; set; }        
        public String[] ItemName { get; set; }
        public int? ParentItem { get; set; }
        public int Cost { get; set; }
        public DateTime ReqDate {  get; set; }
    }
}
