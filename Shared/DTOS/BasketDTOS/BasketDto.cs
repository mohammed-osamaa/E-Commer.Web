using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.BasketDTOS
{
    public class BasketDto
    {
        public string Id { get; set; } = null!;
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
