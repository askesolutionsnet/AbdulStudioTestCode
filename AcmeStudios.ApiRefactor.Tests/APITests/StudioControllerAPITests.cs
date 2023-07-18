using Microsoft.Extensions.Logging;
using Moq;
using AcemStudios.ApiRefactor.Controllers;
using AcmeStudios.ApiRefactor.Models;
using Microsoft.AspNetCore.Mvc;
using AcmeStudios.ApiRefactor.Business;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace AcemStudios.ApiRefactor.Tests;

[TestClass]
public class StudioControllerAPITests
{
    private StudioItemController _controller;
    private Mock<ILogger<StudioItemController>> _loggerMock;
    private Mock<IStudioService> _studioServiceMock;

    [TestInitialize]
    public void Setup()
    {
        _loggerMock = new Mock<ILogger<StudioItemController>>();
        _studioServiceMock = new Mock<IStudioService>();
        _controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);
    }

    [TestMethod]
    public async Task GetAllStudioItems_ReturnsOkResult()
    {
        // Arrange
        _studioServiceMock.Setup(service => service.GetAllStudioHeaderItems())
            .ReturnsAsync(new ServiceResponse<List<StudioItemHeaders>>()
            {
                Data = new List<StudioItemHeaders>()
                {
                new StudioItemHeaders { StudioItemId = 1, Name = "Item 1", Description = "Description 1" },
                new StudioItemHeaders { StudioItemId = 2, Name = "Item 2", Description = "Description 2" }
                },
                Success = true
            });

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);

        // Act
        var result = await controller.Get();

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult.Value);
        var studioItems = okResult.Value as ServiceResponse<List<StudioItemHeaders>>;
        Assert.IsTrue(studioItems.Success);
        Assert.AreEqual(2, studioItems.Data.Count);
    }

    [TestMethod]
    public async Task GetStudioItemById_WithValidId_ReturnsOkResult()
    {
        // Arrange
        int StudioItemId = 1;

        _studioServiceMock.Setup(service => service.GetStudioItemById(StudioItemId))
            .ReturnsAsync(new ServiceResponse<StudioItems>()
            {
                Data = new StudioItems { Name = "Item 1", Description = "Description 1" },
                Success = true
            });

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);

        // Act
        var result = await controller.GetById(StudioItemId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult.Value);
        var studioItem = okResult.Value as ServiceResponse<StudioItems>;
        Assert.IsTrue(studioItem.Success);
        Assert.AreEqual(1, StudioItemId);
    }

    [TestMethod]
    public async Task GetById_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int StudioItemId = 0;

        // Act
        var result = await _controller.GetById(StudioItemId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid Id", badRequestResult.Value);
    }


    [TestMethod]
    public async Task AddStudioItem_WithValidItem_ReturnsCreatedAtActionResult()
    {
        // Arrange
        int StudioItemId = 1;

        _studioServiceMock.Setup(service => service.AddStudioItem(It.IsAny<StudioItems>()))
            .ReturnsAsync(new ServiceResponse<List<StudioItems>>()
            {
                Data = new List<StudioItems>()
                                { new StudioItems { Name = "Item 1", Description = "Description 1" } },
                Success = true
            });

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);
        var newItem = new StudioItems { Name = "New Item", Description = "New Description" };

        // Act
        var result = await controller.Add(newItem);

        // Assert
        Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        var createdAtActionResult = result as CreatedAtActionResult;
        Assert.AreEqual(nameof(StudioItemController.Get), createdAtActionResult.ActionName);
        Assert.IsNotNull(createdAtActionResult.Value);
        var studioItem = createdAtActionResult.Value as ServiceResponse<List<StudioItems>>;
        Assert.IsTrue(studioItem.Success);
        Assert.AreEqual(1, StudioItemId);
    }

    [TestMethod]
    public async Task Add_WithInvalidRequest_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.Add(null);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid Request", badRequestResult.Value);
    }


    [TestMethod]
    public async Task UpdateStudioItem_WithValidIdAndItem_ReturnsOkResult()
    {
        // Arrange
        int StudioItemId = 1;

        _studioServiceMock.Setup(service => service.GetStudioItemById(1))
            .ReturnsAsync(new ServiceResponse<StudioItems>()
            {
                Data = new StudioItems { Name = "Item 1", Description = "Description 1" },
                Success = true
            });

        _studioServiceMock.Setup(service => service.UpdateStudioItem(StudioItemId, It.IsAny<StudioItems>()))
            .ReturnsAsync(new ServiceResponse<StudioItems>()
            {
                Data = new StudioItems { Name = "Updated Item", Description = "Updated Description" },
                Success = true
            });

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);
        var updatedItem = new StudioItems { Name = "Updated Item", Description = "Updated Description" };

        // Act
        var result = await controller.Update(1, updatedItem);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult.Value);
        var studioItem = okResult.Value as ServiceResponse<StudioItems>;
        Assert.IsTrue(studioItem.Success);
        Assert.AreEqual(1, StudioItemId);
        Assert.AreEqual("Updated Item", studioItem.Data.Name);
    }

    [TestMethod]
    public async Task Update_WithInvalidId_ReturnsBadRequest()
    {
        // Arrange
        int StudioItemId =0;

        // Act
        var result = await _controller.Update(StudioItemId, new StudioItems());

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        var badRequestResult = result as BadRequestObjectResult;
        Assert.AreEqual("Invalid Id", badRequestResult.Value);
    }



    [TestMethod]
    public async Task Delete_WithNotFoundId_ReturnsNotFound()
    {
        // Arrange
        _studioServiceMock.Setup(service => service.GetStudioItemById(1))
            .ReturnsAsync(new ServiceResponse<StudioItems>()
            {
                Data = null,
                Success = true
            });

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
        var notFoundResult = result as NotFoundObjectResult;
        Assert.AreEqual("Studio item not found", notFoundResult.Value);
    }


    [TestMethod]
    public async Task DeleteStudioItem_WithValidId_ReturnsOkResult()
    {
        // Arrange
        int StudioItemId = 1;

        _studioServiceMock.Setup(service => service.GetStudioItemById(StudioItemId))
            .ReturnsAsync(new ServiceResponse<StudioItems>()
            {
                Data = new StudioItems { Name = "Item 1", Description = "Description 1" },
                Success = true
            });

        _studioServiceMock.Setup(service => service.DeleteStudioItem(StudioItemId))
            .ReturnsAsync(new ServiceResponse<List<StudioItems>>()
            {
                Data = null,
                Success = true
            });

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);

        // Act
        var result = await controller.Delete(StudioItemId);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult.Value);
        var studioItem = okResult.Value as ServiceResponse<List<StudioItems>>;
        Assert.IsTrue(studioItem.Success);
        Assert.IsNull(studioItem.Data);
    }

    [TestMethod]
    public async Task GetStudioItemTypes_ReturnsOkResult()
    {
        // Arrange
        _studioServiceMock.Setup(service => service.GetAllStudioItemTypes())
            .ReturnsAsync(new ServiceResponse<List<StudioItemTypes>>()
            {
                Data = new List<StudioItemTypes>()
                                            { 
                                               new StudioItemTypes { StudioItemTypeId = 1, Value = "Type 1" },
                                               new StudioItemTypes { StudioItemTypeId = 2, Value = "Type 2" }
                                            },
                Success = true
            }); 

        var controller = new StudioItemController(_loggerMock.Object, _studioServiceMock.Object);

        // Act
        var result = await controller.GetStudioItemTypes();

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult.Value);
        var studioItemTypes = okResult.Value as ServiceResponse<List<StudioItemTypes>>;
        Assert.AreEqual(2, studioItemTypes.Data.Count);
    }



}