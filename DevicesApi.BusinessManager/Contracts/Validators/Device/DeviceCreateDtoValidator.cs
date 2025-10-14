using DevicesApi.Common.Devices.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.BusinessManager.Contracts.Validators.Device
{
    public class DeviceCreateDtoValidator : AbstractValidator<DeviceCreateDto>
    {
        public DeviceCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Brand).NotEmpty().MaximumLength(100);
            RuleFor(x => x.State).IsInEnum();
        }
    }
}
