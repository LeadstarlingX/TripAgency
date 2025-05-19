using Application.DTOs.Common;
using Domain.Enum;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Contact
{
    public class ContactTypeDto : BaseDto<int>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ContactTypeEnum Type { get; set; } //Email, Phone...
    }

    public class ContactTypesDto
    {
        public IList<ContactTypeDto> ContactTypes { get; set; } = [];
    }
}
