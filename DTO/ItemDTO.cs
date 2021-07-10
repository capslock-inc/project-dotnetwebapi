using System;

namespace something.Dtos
{
    public record ItemDTO {
        public Guid Id{get; init;}
        public string Name {get; init;}  
        public decimal Price {get; init;}
        public DateTimeOffset CreatedTime {get; init;}
    }


}