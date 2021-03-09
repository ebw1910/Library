namespace LibraryCRM.Models.ViewModels
{
    public class BookViewModel
    {
        public int BookID { get; set; }

        public string Name { get; set; }
        
        public string Author { get; set; }

        public int Count { get; set; }
        
        public int GenresID { get; set; }
        
        public GenreViewModel Genres { get; set; }
    }
}