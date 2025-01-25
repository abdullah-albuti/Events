using Events.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private IWebHostEnvironment _webHostEnvironment;

    public AdminController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }





    [Authorize]
    public IActionResult  Delted_Events(int EventId)
    {
        var Event = _context.Events.FirstOrDefault(c => c.EventId == EventId);
        if (Event != null)
        {
            _context.Events.Remove(Event);
            _context.SaveChanges();
        }


        var Eventss = _context.Events.ToList();

        return RedirectToAction("eventadminpage", Eventss);

       

    }

    [Authorize]
    public IActionResult EditEvent(int id)
    {

        var s = _context.Events.FirstOrDefault(e => e.EventId == id);
        ViewBag.PackageToEvents = _context.Packag.ToList();

        return View(s);

    }








    [Authorize]
    public IActionResult update_Events(Event Eventc , IFormFile photo)
    {

        try
        {



            if (photo == null || photo.Length == 0)
            {
                return Content("No File Selected");
            }

            //wwwroot/images/p.png
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }

            Eventc.image = photo.FileName;


         
            
                _context.Events.Update(Eventc);
                _context.SaveChanges();
                return RedirectToAction("eventadminpage");

            


        }
        catch (Exception ex)
        {
            return Content($"Error: {ex.Message}");
        }


    }



















    [Authorize]
    [HttpPost]
    public IActionResult Delted_Packag(int Id)
    { 
        
        var package_ = _context.Packag.FirstOrDefault(c => c.Id == Id);
        if (package_ != null)
        {
            _context.Packag.Remove(package_);
            _context.SaveChanges();
        }



        return RedirectToAction("packageadminpage");

    }

    [Authorize]
    public IActionResult EditPackag(int id)
    {

        var s = _context.Packag.FirstOrDefault(e => e.Id == id);

        return View(s);

    }


    [Authorize]
    public IActionResult update_Packag(Packag packag, IFormFile photo)
    {

        try
        {



            if (photo == null || photo.Length == 0)
            {
                return Content("No File Selected");
            }

            //wwwroot/images/p.png
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }

            packag.image = photo.FileName;


            _context.Packag.Update(packag);
            _context.SaveChanges();
            return RedirectToAction("packageadminpage");




        }
        catch (Exception ex)
        {
            return Content($"Error: {ex.Message}");
        }


    }



    [Authorize]
    public IActionResult Editbooking(int id)
    {

        var s = _context.Bookings.FirstOrDefault(e => e.BookingId == id);
        ViewBag.EventTobooking = _context.Events.ToList();

        return View(s);

    }


    [Authorize]
    public IActionResult update_booking(Booking booking)
    {
        _context.Bookings.Update(booking);
        _context.SaveChanges();
        return RedirectToAction("bookingadminpage");

    }



    [Authorize]
    public IActionResult Delted_booking(int BookingId)
    {
        var bookings = _context.Bookings.FirstOrDefault(c => c.BookingId == BookingId);
        if (bookings != null)
        {
            _context.Bookings.Remove(bookings);
            _context.SaveChanges();
        }



        return RedirectToAction("bookingadminpage");


    }






    [Authorize]
    public async Task<IActionResult> Index()
    {

        ViewBag.packages = await _context.Packag.ToListAsync();
        ViewBag.bookings = await _context.Bookings.ToListAsync();
        return View(await _context.Events.ToListAsync());
    }
    [Authorize]

    public IActionResult CreateEvent()
    {


        ViewBag.PackageToEvent = _context.Packag.ToList();
   
        return View();
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateEvent(Event newEvent, IFormFile photo)
    {

        try
        {



            if (photo == null || photo.Length == 0)
            {
                return Content("No File Selected");
            }

            //wwwroot/images/p.png
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }

            newEvent.image = photo.FileName;


            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction("eventadminpage");


       



        }
        catch (Exception ex)
        {
            return Content($"Error: {ex.Message}");
        }


    }





    [Authorize]
    public IActionResult Createbooking()
    {
        ViewBag.EventTobooking = _context.Events.ToList();

     

        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Createbooking(Booking bookingss)
    {
        if (ModelState.IsValid)
        {
            _context.Bookings.Add(bookingss);
            await _context.SaveChangesAsync();
            return RedirectToAction("bookingadminpage");
        }
        return View();
    }







    [Authorize]
    public IActionResult CreatePackag()
    {
        return View();
    }


    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreatePackag(Packag packag, IFormFile photo)
    {
        try
        {

            if (photo == null || photo.Length == 0)
            {
                return Content("No File Selected");
            }

            //wwwroot/images/p.png
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", photo.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                photo.CopyTo(stream);
                stream.Close();
            }

            packag.image = photo.FileName;
         
                _context.Packag.Add(packag);
                await _context.SaveChangesAsync();
                return RedirectToAction("packageadminpage");
       
        }
        catch (Exception ex)
        {
            return Content($"Error: {ex.Message}");
        }
    }






    [Authorize]
    public async Task<IActionResult> Bookings()
    {
        var bookings = await _context.Bookings.Include(b => b.Event_ID).ToListAsync();
        return View(bookings);
    }

    [Authorize]

    public async Task<IActionResult> Packageadminpage() {


        return View(await _context.Packag.ToListAsync());
    }


    [Authorize]

    public async Task<IActionResult> Eventadminpage()
    {

        return View(await _context.Events.ToListAsync());
    }

    [Authorize]

    public async Task<IActionResult> Bookingadminpage()
    {


        return View(await _context.Bookings.ToListAsync());
    }
}