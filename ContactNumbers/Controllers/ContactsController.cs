using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactNumbers.Models;

namespace ContactNumbers.Controllers
{
    public class ContactsController : Controller
    {
        private readonly ContactsContext _context;

        public ContactsController(ContactsContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contact.ToListAsync());
        }

        public IActionResult IndexPaging(string searchKey, int P_size = 5, int pg = 1)
        {
            string searchValue = searchKey;
            const int pageSize = 4;
            //int pageSize = P_size;
            if (pg < 1)
                pg = 1;
            int recsCount = 0;
            if (string.IsNullOrEmpty(searchValue))
                recsCount = _context.Contact.Count();
            else
                recsCount = _context.Contact.Where(e => e.Name.Contains(searchValue)).Count();


            var pager = new Pager(recsCount, pg, pageSize);

            int recSkip = (pg - 1) * pageSize;
            var vacationContext = new List<Contacts>();
            if (string.IsNullOrEmpty(searchValue))
                vacationContext = _context.Contact.OrderBy(n => n.Name).Skip(recSkip).Take(pager.PageSize).AsNoTracking().ToList();
            else
                vacationContext = _context.Contact.Where(e => e.Name.Contains(searchValue)).OrderBy(n => n.Name).Skip(recSkip).OrderBy(n => n.Name).Take(pager.PageSize).AsNoTracking().ToList();

            this.ViewBag.Pager = pager;
            return PartialView("_ContactList", vacationContext);

        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contacts = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contacts == null)
            {
                return NotFound();
            }

            return View(contacts);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Contacts/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address,Notes")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contacts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contacts);
        }

       

        private bool ContactsExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }


        [HttpGet]
        public async Task<Contacts> DetailsForModal(int id)
        {
            

            var contacts = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            return contacts;
        }



        [HttpPost]
 
        public string EditModal(int Id, string Name, string Phone, string Address, string Notes)
        {

            try
            {
                var contact = _context.Contact.Find(Id);
                contact.Name = Name;
                contact.Phone = Phone;
                contact.Address = Address;
                contact.Notes = Notes;
                _context.Update(contact);
                _context.SaveChanges();
                return "تمت عملية التعديل";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        [HttpPost]
        public string DeleteContact(int id)
        {
            var contacts =  _context.Contact.Find(id);
            _context.Contact.Remove(contacts);
            _context.SaveChanges();
            return "تمت عملية الحذف";
        }

    }
}
