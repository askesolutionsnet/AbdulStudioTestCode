using Microsoft.Extensions.Logging;
using Moq;
using AcemStudios.ApiRefactor.Controllers;
using AcmeStudios.ApiRefactor.Models;
using Microsoft.AspNetCore.Mvc;
using AcmeStudios.ApiRefactor.Business;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AcmeStudios.ApiRefactor.Data;

namespace AcemStudios.ApiRefactor.Tests;

[TestClass]
public class StudioServiceTests
{
    private Mock<IStudioRepository> mockRepository;
    private StudioService service;


    [TestInitialize]
    public void Setup()
    {
        mockRepository = new Mock<IStudioRepository>();
        service = new StudioService(mockRepository.Object);
    }

    [TestMethod]
    public async Task GetStudioItemById_ReturnsStudioItem()
    {
        // Arrange
        int itemId = 1;
        mockRepository.Setup(repo => repo.GetStudioItemById(itemId))
            .ReturnsAsync(new StudioItem { StudioItemId = itemId, Name = "Item 1" });
        
        // Act
        var result = await service.GetStudioItemById(itemId);

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(itemId, itemId);
        Assert.AreEqual("Item 1", result.Data.Name);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Here's your selected studio item", result.Message);
    }

    [TestMethod]
    public async Task GetStudioItemById_InvalidId_ReturnsNull()
    {
        // Arrange
        int itemId = 100;
        mockRepository.Setup(repo => repo.GetStudioItemById(itemId))
            .ReturnsAsync((StudioItem)null);

        // Act
        var result = await service.GetStudioItemById(itemId);

        // Assert
        Assert.IsNull(result.Data);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Here's your selected studio item", result.Message);
    }


    [TestMethod]
    public async Task AddStudioItem_NullInput_ReturnsNull()
    {
        // Arrange
        StudioItems studioItem = null;

        // Act
        var result = await service.AddStudioItem(studioItem);

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("New item added", result.Message);
    }


    [TestMethod]
    public async Task AddStudioItem_ReturnsAddedItem()
    {
        // Arrange
        var studioItem = new StudioItems { Name = "New Item", Description = "Description" };

        mockRepository.Setup(repo => repo.AddStudioItem(It.IsAny<StudioItem>()))
            .ReturnsAsync(new List<StudioItem> { new StudioItem { StudioItemId = 1, Name = "New Item" } });

        // Act
        var result = await service.AddStudioItem(studioItem);

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(1, result.Data.Count);
        Assert.AreEqual("New Item", result.Data[0].Name);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("New item added", result.Message);
    }

    [TestMethod]
    public async Task GetAllStudioHeaderItems_NoItems_ReturnsEmptyList()
    {
        // Arrange
        mockRepository.Setup(repo => repo.GetAllStudioHeaderItems())
            .ReturnsAsync(new List<StudioItem>());

        // Act
        var result = await service.GetAllStudioHeaderItems();

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(0, result.Data.Count);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Here's all the items in your studio", result.Message);
    }


    [TestMethod]
    public async Task GetAllStudioHeaderItems_ReturnsAllHeaderItems()
    {
        // Arrange
        mockRepository.Setup(repo => repo.GetAllStudioHeaderItems())
            .ReturnsAsync(new List<StudioItem> 
                {
                   new StudioItem { StudioItemId = 1, Name = "Item 1" } 
                });

        // Act
        var result = await service.GetAllStudioHeaderItems();

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(1, result.Data.Count);
        Assert.AreEqual("Item 1", result.Data[0].Name);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Here's all the items in your studio", result.Message);
    }

    [TestMethod]
    public async Task UpdateStudioItem_InvalidId_ReturnsNull()
    {
        // Arrange
        int itemId = 100;
        var studioItem = new StudioItems { Name = "Updated Item" };
        mockRepository.Setup(repo => repo.UpdateStudioItem(itemId, It.IsAny<StudioItem>()))
            .ReturnsAsync((StudioItem)null);

        // Act
        var result = await service.UpdateStudioItem(itemId, studioItem);

        // Assert
        Assert.IsNull(result.Data);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Update successful", result.Message);
    }


    [TestMethod]
    public async Task UpdateStudioItem_ReturnsUpdatedItem()
    {
        // Arrange
        int itemId = 1;
        var studioItem = new StudioItems { Name = "Updated Item" };
        mockRepository.Setup(repo => repo.UpdateStudioItem(itemId, It.IsAny<StudioItem>()))
            .ReturnsAsync(new StudioItem { StudioItemId = itemId, Name = "Updated Item" });

        // Act
        var result = await service.UpdateStudioItem(itemId, studioItem);

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(1, itemId);
        Assert.AreEqual("Updated Item", result.Data.Name);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Update successful", result.Message);
    }

    [TestMethod]
    public async Task DeleteStudioItem_ReturnsDeletedItem()
    {
        // Arrange
        int itemId = 1;
        mockRepository.Setup(repo => repo.DeleteStudioItem(itemId))
            .ReturnsAsync(new List<StudioItem> { new StudioItem { StudioItemId = itemId, Name = "Item 1" } });

        // Act
        var result = await service.DeleteStudioItem(itemId);

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(1, result.Data.Count);
        Assert.AreEqual(itemId, 1);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Item deleted", result.Message);
    }

    [TestMethod]
    public async Task GetAllStudioItemTypes_ReturnsAllItemTypes()
    {
        // Arrange
        mockRepository.Setup(repo => repo.GetAllStudioItemTypes())
            .ReturnsAsync(new List<StudioItemType> { new StudioItemType { StudioItemTypeId = 1, Value = "Type 1" } });

        // Act
        var result = await service.GetAllStudioItemTypes();

        // Assert
        Assert.IsNotNull(result.Data);
        Assert.AreEqual(1, result.Data.Count);
        Assert.AreEqual("Type 1", result.Data[0].Value);
        Assert.IsTrue(result.Success);
        Assert.AreEqual("Item types fetched", result.Message);
    }

}