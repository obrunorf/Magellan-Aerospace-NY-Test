using MagellanTest.Model;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MagellanTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {

        [HttpGet("ItemById")]
        public ActionResult<Item> GetItem([FromServices] Repository repo, int id)
        {
            var item = repo.GetItem(id);
            return (item.Count>0) ? Ok(item) : NotFound();
        }
        [HttpGet("TotalCostByName")]
        public ActionResult<int> GetTotalCost([FromServices] Repository repo, String item_name)
        {
            var cost = repo.GetTotalCost(item_name);
            return (cost is not null) ? Ok(cost) : NotFound();
        }
        [HttpPost("Item")]
        public ActionResult<int> PostItem([FromServices] Repository repo, Item item)
        {
            var id = repo.InsertItem(item);
            return (id>0) ? Ok(id) : BadRequest();
        }


    }
}
