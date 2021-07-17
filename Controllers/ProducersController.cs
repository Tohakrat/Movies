 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Controllers
{
    public class ProducersController : Controller
    {
        private List<ProducerPlus> _producerPlusList;

        private readonly ApplicationContext _context;

        public ProducersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            _context.Producers.Load();
            _context.Movies.Load();
            int ProducersCount = _context.Producers.Count<Producer>();
            _producerPlusList = new List<ProducerPlus>();
            int i = 0;
            foreach (Producer  prod in _context.Producers)
            {
                _producerPlusList.Add(new ProducerPlus());
                _producerPlusList[i].Count = 0;
                _producerPlusList[i].ProducerItem = prod;
                    
                if (prod.Movies!=null)
                {
                    _producerPlusList[i].Count = prod.Movies.Count;
                }
                i++;
                //else _producerPlusList[i].Count = 0;
            }

            //_producerPlus = new List<ProducerPlus>(_context.Producers.)
            return View( _producerPlusList); 
            //return View(await _context.Producers.ToListAsync());
        }      
        // GET: Producer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        // GET: Producer/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Producer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age")] Producer producer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        // GET: Producer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        // POST: Producer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Producer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Producer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
