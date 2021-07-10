using something.Dtos;
using something.Entites;

namespace something{

    public static class Extension
    {
        public static ItemDTO AsDto(this Item item){
            return new ItemDTO{
                Id = item.Id,
                Name = item.Name,
                Price = item.Price,
                CreatedTime = item.CreatedTime
            };
        }
    }

}