using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataService
{
    public interface IDataRepository<T> : IDisposable 
        where T : class
    {
        IEnumerable<T> GetBookList(); // получение всех объектов
        T GetBook(int id); // получение одного объекта по id
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
        void Save();  // сохранение изменений
    }
}
