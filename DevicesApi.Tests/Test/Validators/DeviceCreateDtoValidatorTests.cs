using DevicesApi.BusinessManager.Contracts.Validators.Device;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using FluentValidation.TestHelper;
using Xunit;

namespace DevicesApi.Tests.Test.Validators;
public class DeviceCreateDtoValidatorTests
{
    private readonly DeviceCreateDtoValidator _validator = new();

    [Fact]
    public void Should_HaveError_WhenNameIsEmpty()
    {
        var dto = new DeviceCreateDto { Name = "", Brand = "Sony", State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.Name);
    }

    [Fact]
    public void Should_HaveError_WhenBrandIsNull()
    {
        var dto = new DeviceCreateDto { Name = "Router", Brand = null!, State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldHaveValidationErrorFor(d => d.Brand);
    }

    [Fact]
    public void Should_NotHaveError_WhenDtoIsValid()
    {
        var dto = new DeviceCreateDto { Name = "Router", Brand = "Sony", State = DeviceState.Active };
        var result = _validator.TestValidate(dto);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
