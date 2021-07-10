using Microsoft.AspNetCore.Mvc;
using something.Repository;
using System.Collections.Generic;
using something.Entites;
using something.Dtos;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace something.Controllers
{
    //GET /items
    [ApiController]
    [Route("items")]
    public class ItemController : ControllerBase
    {
        private readonly Iitemrepository repository;

        public ItemController(Iitemrepository repository)
        {
            this.repository = repository;
        } 

        [HttpGet]
        public async Task<IEnumerable<ItemDTO>> GetItems(){
            var items =(await repository.GetItems()).Select( item => item.AsDto());
            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDTO>> GetItem(Guid id){
            var item =await repository.GetItem(id);
            if (item is null){
                return NotFound();
            }
            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDTO>> CreateItem(CreatedItemDTO itemDTO){
            Item item = new(){
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                CreatedTime = DateTimeOffset.UtcNow
            };
            await repository.CreatedItem(item);
            return CreatedAtAction(nameof(GetItem),new{id = item.Id},item.AsDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Updateitem(Guid id,UpdateItemDTO itemDTO){
            var existingItem =await repository.GetItem(id);
            
            if (existingItem is null){
                return NotFound();
            }

            Item updateditem = existingItem with
            {
                Name =itemDTO.Name,
                Price = itemDTO.Price
            };

            await repository.UpdateItem(updateditem);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(Guid id){
            var existingItem =await repository.GetItem(id);
            
            if (existingItem is null){
                return NotFound();
            }
            await repository.DeleteItem(id);
            return NoContent();

        }


    }
}