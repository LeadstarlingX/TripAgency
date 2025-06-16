using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Customer
{
    public class CreateCustomerContactDto : BaseDto<long>
    {
        public int ContactTypeId {  get; set; }
        public required string Value {  get; set; }
    }
}
