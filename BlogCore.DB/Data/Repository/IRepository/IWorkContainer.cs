using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.DB.Data.Repository.IRepository
{
    public interface IWorkContainer : IDisposable
    {
        ICategoriaRepository Categoria { get; }
        void Save();
    }
}
