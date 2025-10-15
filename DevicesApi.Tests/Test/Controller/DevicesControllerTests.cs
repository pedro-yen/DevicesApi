using DevicesApi.Api.Controllers;
using DevicesApi.BusinessManager.Services.Devices;
using DevicesApi.Common.Devices.DTOs;
using DevicesApi.Common.Devices.Enums;
using DevicesApi.Data.Entities;
using DevicesApi.Tests.Fixtures.Devices;
using DevicesApi.Tests.Fixtures.Managers.BusinessManager;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Tests.Test.Controller;

public class DevicesControllerTests
{
    private readonly DevicesController _controller;

    public DevicesControllerTests()
    {
        var mockManager = DeviceBusinessManagerFixture.CreateMock();
        _controller = new DevicesController(mockManager.Object);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenDeviceExists()
    {
        // Arrange
        var id = DeviceFixtures.ActiveDevice.Id;

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var device = Assert.IsType<Device>(actionResult.Value);
        Assert.Equal(id, device.Id);
    }

    [Fact]
    public async Task GetAll_ReturnsAllDevices()
    {
        var result = await _controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var devices = Assert.IsAssignableFrom<IEnumerable<Device>>(okResult.Value);
        Assert.Equal(3, devices.Count());
    }

    [Fact]
    public async Task GetByBrand_ReturnsMatchingDevices()
    {
        var brand = "Sony";

        var result = await _controller.GetByBrand(brand);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var devices = Assert.IsAssignableFrom<IEnumerable<Device>>(okResult.Value);
        Assert.All(devices, d => Assert.Equal(brand, d.Brand));
    }

    [Fact]
    public async Task GetByState_ReturnsMatchingDevices()
    {
        var state = DeviceState.InUse;

        var result = await _controller.GetByState(state);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var devices = Assert.IsAssignableFrom<IEnumerable<Device>>(okResult.Value);
        Assert.All(devices, d => Assert.Equal(state, d.State));
    }

    [Fact]
    public async Task Create_ReturnsCreatedDevice()
    {
        var dto = new DeviceCreateDto
        {
            Name = "New Device",
            Brand = "LG",
            State = DeviceState.Active
        };

        var result = await _controller.Create(dto);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var device = Assert.IsType<Device>(createdResult.Value);
        Assert.Equal(dto.Name, device.Name);
        Assert.Equal(dto.Brand, device.Brand);
    }

    [Fact]
    public async Task Update_ReturnsUpdatedDevice()
    {
        var id = DeviceFixtures.InUseDevice.Id;
        var dto = new DeviceUpdateDto { Name = "Updated", Brand = "UpdatedBrand", State = DeviceState.InUse };

        var result = await _controller.Update(id, dto);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var device = Assert.IsType<Device>(okResult.Value);
        Assert.Equal(dto.Name, device.Name);
        Assert.Equal(dto.Brand, device.Brand);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent()
    {
        var id = DeviceFixtures.InactiveDevice.Id;

        var result = await _controller.Delete(id);

        Assert.IsType<NoContentResult>(result);
    }

}