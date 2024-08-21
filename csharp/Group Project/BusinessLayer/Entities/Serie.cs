using BusinessLayer.Exceptions;
using System;

namespace BusinessLayer.Entities
{
    public class Serie
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Serie(string name)
        {
            SetName(name);
        }

        public Serie(int id, string name):this(name)
        {
            SetId(id);
        }

        private void SetId(int id)
        {
            if (id <= 0) throw new SerieException("SerieId is not valid.");
            Id = id;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new SerieException("Serie name is empty.");
            Name = name;
        }
    }
}
