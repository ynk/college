namespace BusinessLayer.Entities
{
    public class ComicAuthor
    {
        public ComicAuthor(int comicId, int authorId)
        {
            ComicId = comicId;
            AuthorId = authorId;
        }

        public ComicAuthor()
        {
        }

        public int ComicId { get; set; }
        public int AuthorId { get; set; }
    }
}
