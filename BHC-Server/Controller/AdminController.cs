using BHC_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BHC_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly DB_BHCContext _context;
        public AdminController(DB_BHCContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult LoginAdmin(Login loginadmin)
        {
            var a = _context.QuanTriViens.FirstOrDefault(x => x.TaiKhoanQtv == loginadmin.UserName);
            if (a != null)
            {
                if (a.MatKhau == loginadmin.Password)
                {
                    return Ok(a.IdquanTriVien);
                }
                else
                {
                    return BadRequest("failed");
                }
            }
            else
            {
                return BadRequest("failed");
            }

        }


    }
}
