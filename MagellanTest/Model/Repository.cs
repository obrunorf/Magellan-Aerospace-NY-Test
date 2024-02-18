using Dapper;
using Npgsql;

namespace MagellanTest.Model
{
    public class Repository
    {        
        static String conString = new StreamReader("db_credentials").ReadLine();
        //"Host=$;Username=$;Password=$;Database=$"; 


        NpgsqlConnection GetConnection(String connectionString)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            return conn;
        }

        public List<Item?> GetItem(int id)
        {
            using var con = GetConnection(conString);
            var items = con.Query<Item?>("SELECT ID, ITEM_NAME AS ITEMNAME, PARENT_ITEM AS PARENTITEM, COST, REQ_DATE AS REQDATE  FROM ITEM I WHERE I.ID =" + id);
            return (List<Item?>)items;
        }

        public float? GetTotalCost(String item_name)
        {
            using var con = GetConnection(conString);
            var cost = con.Query<float?>("SELECT * FROM get_total_cost('" + item_name + "')");
            return cost.First();
        }

        public int? InsertItem(Item item)
        {
            using var con = GetConnection(conString);
            var sql = String.Format("INSERT INTO ITEM(id, item_name, parent_item, cost, req_date) "
                + " values((select max(id) + 1 from item),{0},{1},{2},'{3}') returning id ",
                "'{" + item.ItemName[0] + "}'",
                ((item.ParentItem > 0) ? item.ParentItem : "null"),
                item.Cost,
                item.ReqDate);            
            return con.Query<int?>(sql).First();
        }

    }
}
