using System;
using System.Collections.Generic;
using something.Entites;
using System.Threading.Tasks;

namespace something.Repository{

//this is the interface which defines the functionality of the repository.
public interface Iitemrepository
    {
        Task<Item> GetItem(Guid id);
        Task<IEnumerable<Item>> GetItems();
        Task CreatedItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(Guid id);
    }

}