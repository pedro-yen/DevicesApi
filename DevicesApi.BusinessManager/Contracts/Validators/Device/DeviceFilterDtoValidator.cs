using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevicesApi.Common.Devices.DTOs;

namespace DevicesApi.BusinessManager.Contracts.Validators.Device
{
    public class DeviceFilterDtoValidator : AbstractValidator<DeviceFilterDto>
    {
        public DeviceFilterDtoValidator()
        {
            When(x => x.Brand != null, () =>
            {
                RuleFor(x => x.Brand).MaximumLength(100);
            });

            When(x => x.State != null, () =>
            {
                RuleFor(x => x.State).IsInEnum();
            });
        }
    }
}
