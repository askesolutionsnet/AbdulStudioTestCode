using AcmeStudios.ApiRefactor.Models;
using AcmeStudios.ApiRefactor.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using AcmeStudios.ApiRefactor.Data;

namespace AcemStudios.ApiRefactor.Controllers;

[Route("askesolutions/api/[controller]")]
[ApiController]
public class StudioItemController : ControllerBase
{
    private readonly ILogger<StudioItemController> _logger;
    private readonly IStudioService _studioService;

    public StudioItemController(ILogger<StudioItemController> logger, IStudioService studioService)
    {
        _logger = logger;
        _studioService = studioService;
    }

    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
    {
        try
        {
            var studioItems = await _studioService.GetAllStudioHeaderItems();

            if (studioItems.Data == null || studioItems.Data.Count < 0)
                return NotFound("Studio item not found");

            return Ok(studioItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            if (id == 0)
                return BadRequest("Invalid Id");

            var getStudioItems = await _studioService.GetStudioItemById(id);

            if (getStudioItems.Data == null)
                return NotFound("Studio item not found");

            return Ok(getStudioItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Add([FromQuery] StudioItems studioItem)
    {
        try
        {
            if (studioItem == null)
                return BadRequest("Invalid Request");


            var studioItems = await _studioService.AddStudioItem(studioItem);

            return CreatedAtAction(nameof(Get), studioItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromQuery] StudioItems studioItem)
    {
        try
        {
            if (id == 0)
                return BadRequest("Invalid Id");

            var getStudioItems = await _studioService.GetStudioItemById(id);

            if (getStudioItems.Data == null)
                return NotFound("Studio item not found");


            if (studioItem == null)
                return BadRequest("Invalid Request");

            var studioItems = await _studioService.UpdateStudioItem(id, studioItem);

            return Ok(studioItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            if (id == 0)
                return BadRequest("Invalid Id");

            var getStudioItems = await _studioService.GetStudioItemById(id);

            if (getStudioItems.Data == null)
                return NotFound("Studio item not found");


            var studioItems = await _studioService.DeleteStudioItem(id);

            return Ok(studioItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetStudioItemTypes()
    {
        try
        {
            var studioItemTypes = await _studioService.GetAllStudioItemTypes();

            if (studioItemTypes == null)
                return NotFound();

            return Ok(studioItemTypes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode(500, ex.Message);
        }
    }


}