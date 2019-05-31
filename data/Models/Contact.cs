using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace ContactApi.data.Models
{
  public class Contact
  {
    private static string ConnectionString { get { return Globals.DbConnectionString; } }

    public int Id { get; set; }
    public Name Name { get; set; }
    public Address Address { get; set; }
    public List<Phone> Phone { get; set; }
    public string Email { get; set; }

    public static List<Contact> GetContacts()
    {
      using (var db = new LiteDatabase(ConnectionString))
      {
        // Get a collection (or create, if doesn't exist)
        var col = db.GetCollection<Contact>("contacts");

        if (col == null)
        {
          return null;
        }

        // Index document using document Name property
        col.EnsureIndex(x => x.Name);

        var results = col.FindAll();

        return results.ToList();
      }
    }

    public static Contact Get(int id)
    {
      using (var db = new LiteDatabase(ConnectionString))
      {
        var col = db.GetCollection<Contact>("contacts");

        if (col == null)
        {
          return null;
        }


        var result = col.FindById(id);

        return result;
      }
    }

    public static Contact Add(Contact contact)
    {
      using (var db = new LiteDatabase(ConnectionString))
      {
        var col = db.GetCollection<Contact>("contacts");

        if (col == null)
        {
          return null;
        }


        col.Insert(new Contact { Name = contact.Name, Address = contact.Address, Phone = contact.Phone, Email = contact.Email });

        col.EnsureIndex(x => x.Name);

        var result = col.FindOne(x => x.Name.First == contact.Name.First && x.Name.Last == contact.Name.Last);

        if (result != null)
        {
          return result;
        }
        return null;
      }
    }

    public static Contact Update(int id, Contact contact)
    {
      using (var db = new LiteDatabase(ConnectionString))
      {
        var col = db.GetCollection<Contact>("contacts");

        if (col == null)
        {
          return null;
        }


        col.Update(contact);

        col.EnsureIndex(x => x.Name);

        return col.FindById(id);
      }
    }

    public static bool Delete(int id)
    {
      using (var db = new LiteDatabase(ConnectionString))
      {
        var col = db.GetCollection<Contact>("contacts");

        if (col == null)
        {
          return false;
        }


        var result = col.Delete(id);

        return result;
      }
    }
  }
}


