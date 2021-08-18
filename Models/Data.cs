using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Data
    {
    }
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }
        
        public decimal? Price { get; set; }
        public Producer? Producer { get; set; }
        public Genre? Genre { get; set; }
    }
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Movie> Movies { get; set; }

    }
    public class ProducerPlus
    {
        public Producer ProducerItem { get; set; }
        
        public int Count { get; set; }

    }

}
