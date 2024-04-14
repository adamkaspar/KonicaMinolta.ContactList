using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using KonicaMinolta.ContactList.Business.Services.Contacts;
using KonicaMinolta.Shared.Domain.Exceptions;
using KonicaMinolta.Shared.Domain.Contacts;

namespace KonicaMinolta.ContactList.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ContactListController : ControllerBase
{
    private readonly IContactListService contactListService;

    public ContactListController(IContactListService contactListService) 
        => (this.contactListService) = (contactListService);

    [SwaggerOperation(Summary = "Add contact.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResultDto))]    
    [HttpPost("Add")]
    public async Task<ActionResult<ContactResultDto>> Add(ContactDto contactDto, CancellationToken cancellationToken = default)
    {        
        var result = await contactListService.AddAsync(contactDto, cancellationToken);

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Find contact by id.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResultDto))]    
    [HttpGet("FindById/{id:int}")]
    public async Task<ActionResult<ContactResultDto>> FindById(int id, CancellationToken cancellationToken = default)
    {
        var result = await contactListService.FindByIdAsync(id, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Get list of all contacts.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactResultDto>))]    
    [HttpGet("GetAll")]
    public async Task<ActionResult<List<ContactResultDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var result = await contactListService.GetAllAsync(cancellationToken);

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Get list of all active/inactive contacts, base on input condition.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactResultDto>))]    
    [HttpGet("GetAll/{isActive:bool}")]
    public ActionResult<List<ContactResultDto>> GetAll(bool isActive)
    {
        var result = contactListService.GetAll(isActive);

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Get paged list of contacts.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactResultDto>))]    
    [HttpGet("PageAll/{skip:int}/{take:int}")]
    public async Task<ActionResult<List<ContactResultDto>>> PageAll(int skip, int take, CancellationToken cancellationToken = default)
    {
        var result = await contactListService.PageAllAsync(skip, take, cancellationToken);

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Update contact.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ContactResultDto))]    
    [HttpPut("Update/{id:int}")]
    public async Task<ActionResult<ContactResultDto>> Update(int id, ContactDto contactDto, CancellationToken cancellationToken = default)
    {
        var result = await contactListService.UpdateAsync(id, contactDto, cancellationToken);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [SwaggerOperation(Summary = "Remove contact.")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDto))]
    [ProducesResponseType(StatusCodes.Status200OK)]    
    [HttpDelete("Remove/{id:int}")]
    public async Task<ActionResult<bool>> Remove(int id, CancellationToken cancellationToken = default)
    {
        var result = await contactListService.RemoveAsync(id, cancellationToken);

        if (!result)
        {
            return NotFound();
        }

        return Ok();
    }
}
