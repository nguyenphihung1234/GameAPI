using Microsoft.AspNetCore.Mvc;
using PlayerAssetAPI.Data;
using PlayerAssetAPI.Models;

namespace PlayerAssetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayerController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("getassetsbyplayer")]
        public IActionResult GetAssetsByPlayer()
        {
            var result = from pa in _context.PlayerAssets
                         join p in _context.Players on pa.PlayerId equals p.PlayerId
                         join a in _context.Assets on pa.AssetId equals a.AssetId
                         select new
                         {
                             PlayerName = p.PlayerName,
                             Level = p.Level,
                             Age = p.Age,
                             AssetName = a.AssetName
                         };

            return Ok(result.ToList());
        }
    }
}
