using System.Collections.Generic;

namespace BusinessLayer.Entities
{
    public class ComicResult
    {
        public int ComicId { get; set; }
        public string Title { get; set; }
        public List<string> AuthorName { get; set; }
        public string SerieName { get; set; }
        public int? SerieSeqNumber { get; set; }
        public string PublisherName { get; set; }
        public string AuthorNames { get; set; }
        public int Stock { get; set; } 

        public override string ToString()
        {
            string authorNames = "";
            for (int i = 0; i < AuthorName.Count; i++)
            {
                if(i != AuthorName.Count-1)
                {
                    authorNames += AuthorName[i] + ", ";
                }
                else
                {
                    authorNames += AuthorName[i];
                }
            }
            return authorNames;
        }
    }
}
