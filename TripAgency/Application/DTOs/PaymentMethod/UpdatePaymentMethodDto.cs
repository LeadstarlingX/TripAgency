using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PaymentMethod
{
    public class UpdatePaymentMethodDto:BaseDto<int>
    {
        public string Method { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
