using System.Diagnostics;
using System.Linq;
using Events.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Events.Controllers
{




    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Packag.ToListAsync());
        }




        // عرض المحتوى المصفّى بناءً على PackagId
        public IActionResult EventPage(int id)
        {
            var filteredItems = _context.Events.Where(x => x.PackagId == id).ToList();
            var packgs = _context.Packag.SingleOrDefault(x => x.Id == id);

            ViewBag.FilteredItemss = packgs;
            return View("EventPage", filteredItems); 
        }

        public IActionResult EventPageAll(string searchString)
        {
            var allItems = _context.Events.AsQueryable(); 

            if (!string.IsNullOrEmpty(searchString))
            {
                allItems = allItems.Where(x => x.Name.Contains(searchString));
            }

            ViewBag.FilteredItemss = null;
            return View("EventPage", allItems.ToList());
        }



        //public IActionResult EventPage(int id)
        //{
        //    var filteredItems = _context.Events.Where(x => x.PackagId == id).ToList();
        //    var packgs = _context.Packag.SingleOrDefault(x => x.Id == id);

        //    ViewBag.FilteredItemss = packgs;
        //    return View(filteredItems);

        //}
        //public IActionResult EventPage()
        //{
        //    var filteredItems = _context.Events.ToList();
        //    var packgs = _context.Packag.ToList();

        //    ViewBag.FilteredItemss = packgs;
        //    return View(filteredItems);

        //}

        public async Task<IActionResult> BookEvent(int id)
        {
            var eventDetails = await _context.Events.FindAsync(id);
            if (eventDetails == null) return NotFound();

            ViewBag.PackageToBookEvent = eventDetails;

            return View();
        }

        public IActionResult Endtiketpage()
        {
            

            return View();
        }




        [HttpPost]
        public async Task<IActionResult> Endtiketpage(Booking Booking)
        {
       
            _context.Bookings.Add(Booking);
            await _context.SaveChangesAsync();

            var eventDetails = await _context.Events.FindAsync(Booking.Event_ID);
            if (eventDetails == null) return NotFound();
            ViewBag.PackageToBookEvent = eventDetails;

            return View( Booking);
        }
    }















}
