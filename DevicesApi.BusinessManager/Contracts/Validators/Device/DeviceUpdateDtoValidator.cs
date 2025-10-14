using DevicesApi.Common.Devices.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.BusinessManager.Contracts.Validators.Device
{

    public class DeviceUpdateDtoValidator : AbstractValidator<DeviceUpdateDto>
    {
        public DeviceUpdateDtoValidator()
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name).MaximumLength(100);
            });

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
