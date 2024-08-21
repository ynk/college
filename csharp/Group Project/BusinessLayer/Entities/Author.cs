using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities
{
    public class Author
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Author(string name)
        {
            SetName(name);
        }

        public Author(int id, string name) : this(name)
        {
            SetId(id);
        }

        private void SetId(int id)
        {
            if (id <= 0) throw new AuthorException("AuthorId is not valid.");
            Id = id;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new AuthorException("Author name is empty.");
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is Author other && other.Name.ToLower() == this.Name.ToLower();
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}