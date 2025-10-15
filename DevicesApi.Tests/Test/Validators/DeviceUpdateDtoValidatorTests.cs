using DevicesApi.BusinessManager.Contracts.Validators.Device;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Test.Validators;

public class DeviceUpdateDtoValidatorTests
{
    private readonly DeviceUpdateDtoValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_WhenDtoIsValid()
    {
        var dto = new DeviceUpdateDto { Name = "Router", Brand = "Sony", State = DeviceState.InUse };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotValidateName_WhenNull()
    {
        var dto = new DeviceUpdateDto { Name = null, Brand = "Sony", State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(d => d.Name);
    }

    [Fact]
    public void Should_HaveError_WhenNameTooLong()
    {
        var dto = new DeviceUpdateDto { Name = new string('X', 101), Brand = "Sony", State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.Name);
    }

    [Fact]
    public void Should_NotValidateBrand_WhenNull()
    {
        var dto = new DeviceUpdateDto { Name = "Router", Brand = null, State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveValidationErrorFor(d => d.Brand);
    }

    [Fact]
    public void Should_HaveError_WhenBrandTooLong()
    {
        var dto = new DeviceUpdateDto { Name = "Router", Brand = new string('Y', 101), State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.Brand);
    }

    [Fact]
    public void Should_HaveError_WhenStateIsInvalid()
    {
        var dto = new DeviceUpdateDto { Name = "Router", Brand = "Sony", State = (DeviceState)999 };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.State);
    }
}
