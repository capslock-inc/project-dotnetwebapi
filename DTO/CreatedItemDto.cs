using System.ComponentModel.DataAnnotations;
namespace something.Dtos{

    public record CreatedItemDTO{
        [Required]
        public string Name {get; init;}  
        [Required]
        [Range(1,1000)]
        public decimal Price {get; init;}

    }

}