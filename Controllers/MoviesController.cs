using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        
        private readonly ApplicationContext _context;

        public MoviesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string sortOrder,string SearchFilter, int? pageNumber)
        {
            int pageSize = 8;
            
            if (pageNumber ==null)      pageNumber = 1;
            

            _context.Genres.ToList();
            _context.Producers.ToList();
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            
            IQueryable<Movie> movies;
            if (!String.IsNullOrEmpty(SearchFilter))
            {
                movies = from s in _context.Movies
                              where s.Title.Contains(SearchFilter)
                              select s;
            }
            else
            movies = from s in _context.Movies
                           select s;
             
                
            switch (sortOrder)
            {
                case "name_desc":
                    movies = movies.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    movies = movies.OrderBy(s => s.ReleaseDate);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(s => s.ReleaseDate);
                    break;

                default:
                    movies = movies.OrderBy(s => s.Title);
                    break;
            }
            //return View(await movies.AsNoTracking().ToListAsync());            
            //return View(await _context.Movies.ToListAsync());
            //return View(await movies.ToListAsync());
            
            return View(await PaginatedList<Movie>.CreateAsync(movies, pageNumber ?? 1, pageSize));
        }


        

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        class ProdList
        {
            public int Id { get; set; }
            public string ProducerName { get; set; }
        }
        // GET: Movies/Create
        public IActionResult Create()
        {

            List<Producer> ProducersList = new List<Producer> { };
            foreach (Producer producer1 in _context.Producers)
            {
                ProducersList.Add(producer1);
            }
            ViewBag.ListProd = ProducersList;
            List<Genre> GenreList = new List<Genre> { };
            foreach (Genre Genre1 in _context.Genres)
            {
                GenreList.Add(Genre1);
            }
            ViewBag.ListGenres = GenreList;           
           
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Price")] Movie movie, int? Genre, int? Producer)
        {
            if (Genre!=null)
            {
                movie.Genre = _context.Genres.Find(Genre);
            }
            if (Genre != null)
            {
                movie.Producer = _context.Producers.Find(Producer);
            }
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                //movie.Genre = _context.Genres.Find();

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _context.Genres.ToList();
            _context.Producers.ToList();

            /*List<Producer> ProducersList = new List<Producer> { };
            foreach (Producer producer1 in _context.Producers)
            {
                ProducersList.Add(producer1);

            }        */
            //ViewBag.ListProd = (IEnumerable<Producer>)(_context.Producers);
            ViewBag.ListProd = (_context.Producers);

            List<Genre> GenreList = new List<Genre> { };
            foreach (Genre Genre1 in _context.Genres)
            {
                GenreList.Add(Genre1);
            }
            ViewBag.ListGenres = GenreList;
            
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            ViewBag.SelectedProducer = movie.Producer;
            ViewBag.SelectedGenre = movie.Genre;
            //SelectList prodList = new SelectList(_context.Producers, "Id", "Name");
            
            //prodList.SelectedValues
            /*SelectList selectList = new SelectList(_context.Producers, "Id", "Name");
            foreach (var item in selectList.Items)
            {
                if item.
            }*/


            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Price")] Movie movie, int? Genre, int? Producer)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            if (Genre != null)
            {
                movie.Genre = _context.Genres.Find(Genre);
            }
            if (Producer != null)
            {
                movie.Producer = _context.Producers.Find(Producer);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
        
    }
}
