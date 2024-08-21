using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBestellingRepository BestellingRepository { get; }
        IKlantRepository KlantRepository { get; }
        int Complete();
    }
}
