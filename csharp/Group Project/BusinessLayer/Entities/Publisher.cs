using BusinessLayer.Exceptions;

namespace BusinessLayer.Entities
{
    public class Publisher
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Publisher(string name)
        {
            SetName(name);
        }

        public Publisher(int id, string name):this(name)
        {
            SetId(id);
        }

        private void SetId(int id)
        {
            if (id <= 0) throw new PublisherException("PublisherId is not valid.");
            Id = id;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new PublisherException("Publisher name is empty.");
            Name = name;
        }
    }
}