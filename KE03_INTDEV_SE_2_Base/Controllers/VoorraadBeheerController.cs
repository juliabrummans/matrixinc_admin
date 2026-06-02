using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace KE03_INTDEV_SE_2_Base.Controllers
{
    public class VoorraadBeheerController : Controller
    {
        private readonly MatrixIncDbContext _context;

        public VoorraadBeheerController(MatrixIncDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            var producten = _context.Products.ToList();
            return View(producten);

        }
    }
}
