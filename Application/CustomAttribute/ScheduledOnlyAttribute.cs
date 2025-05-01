using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CustomAttribute
{
    public class ScheduledOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is AppointmentStatus status)
            {
                return status == AppointmentStatus.Scheduled;
            }
            return false;
        }
    }

}
