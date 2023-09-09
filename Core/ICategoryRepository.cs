using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        void Add(Category category);
        Category Get(int id);
        void Update(Category category);
        void Delete(int id);

    }
}
