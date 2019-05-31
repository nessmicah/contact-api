using System.Collections.Generic;
using System.Threading.Tasks;
using ContactApi.data.Models;

namespace ContactApi.interfaces
{
  public interface IContactService
  {
    List<Contact> GetAll();

    Contact GetById(int id);

    Contact Add(Contact contact);

    Contact Update(int id, Contact contact);

    bool Delete(int id);
  }
}
