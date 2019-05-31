using System.Collections.Generic;
using System.Threading.Tasks;
using ContactApi.data.Models;
using ContactApi.interfaces;

namespace ContactApi.services
{
  public class ContactService : IContactService
  {
    public List<Contact> GetAll() => Contact.GetContacts();

    public Contact GetById(int id) => Contact.Get(id);

    public Contact Add(Contact contact) => Contact.Add(contact);

    public Contact Update(int id, Contact contact) => Contact.Update(id, contact);

    public bool Delete(int id) => Contact.Delete(id);
  }
}