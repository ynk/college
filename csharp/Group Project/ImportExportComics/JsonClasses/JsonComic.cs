using System.Collections.Generic;

namespace ImportExportComics.JsonClasses
{
    
    public class JsonComic
    {
        public int ID { get; set; }
        public string Titel { get; set; }
        public int? Nr { get; set; }
        public Reeks Reeks { get; set; }
        public Uitgeverij Uitgeverij { get; set; }
        public List<Auteur> Auteurs { get; set; }

    }
   
    
}
