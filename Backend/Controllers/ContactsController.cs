using BackendRestAPI.Model;
using BackendRestAPI.Services.Contacts;
using BackendRestAPI.Services.Users;
using BackendRestAPI.Services.Dictionaries;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using BackendRestAPI.Requests.Contacts;
using BackendRestAPI.Services.Contacts.ContactsRequestMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace BackendRestAPI.Controllers
{
    public class ContactsController : ApiController
    {
        private readonly IContactsService _contactsService;
        private readonly IUserService _userService;
        private readonly IDictionariesService _dictionaryService;
        private readonly IContactsRequestMapper _requestMapper;

        public ContactsController(IContactsService contactsService, 
            IUserService userService, 
            IDictionariesService dictionariesService, 
            IContactsRequestMapper requestMapper)
        {
            _contactsService = contactsService;
            _userService = userService;
            _dictionaryService = dictionariesService;
            _requestMapper = requestMapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllContactsBasicData()
        {
            // get contacts from db
            var contactsResult = await _contactsService.GetAllContactsAsync();
            if (contactsResult.IsError) { 
                return Problem(contactsResult.Errors); 
            }
            
            // map to response
            var contacts = contactsResult.Value;
            var response = _requestMapper.MapContactListToResponse(contacts);
            return Ok(response);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddContact(AddContactRequest request)
        {
            // get username for contacts owner field
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                return Unauthorized("Username claim not found.");
            }

            // map request to contact
            var newContact = await _requestMapper.MapAddRequestToContactAsync(request, username);
            if (newContact.IsError)
            {
                return Problem(newContact.Errors);
            }

            // validate entered data
            List<Error> errors = await _contactsService.ValidateContactDataAsync(newContact.Value, username);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }

            // add contact to db
            ErrorOr<Created> addResult = await _contactsService.AddContactAsync(newContact.Value);
            if(addResult.IsError)
            {
                return Problem(addResult.Errors);
            }

            return CreatedAtAction(
                nameof(GetContactDetails),
                new { id = newContact.Value.Id },
                _requestMapper.MapContactToContactResponse(newContact.Value)
            );
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetContactDetails(Guid id)
        {
            // get details from db
            ErrorOr<Contact> contactResult = await _contactsService.GetContactAsync(id);

            return contactResult.Match(
                contact => Ok(_requestMapper.MapContactToDetailsResponse(contactResult.Value)),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            // get subcategory to check if it should be deleted
            ErrorOr<Contact> getContactResult = await _contactsService.GetContactAsync(id);
            if (getContactResult.IsError)
            {
                return Problem(getContactResult.Errors);
            }
            Subcategory subcategory = getContactResult.Value.Subcategory;

            // delete contact
            ErrorOr<Deleted> deleteResult = await _contactsService.DeleteContactAsync(id);
            if (deleteResult.IsError)
            {
                return Problem(deleteResult.Errors);
            }

            // check if subcategory should be deleted
            if (subcategory.Contacts.Count == 0 && subcategory.Category.AllowCustomSubcategories)
            {
                var removeSubcategoryResult = await _dictionaryService.RemoveSubcategoryAsync(subcategory.Name);
                if (removeSubcategoryResult.IsError)
                {
                    return Problem(removeSubcategoryResult.Errors);
                }
            }

            return NoContent();
        }

        [HttpPut("{id:Guid}")]
        [Authorize]
        public async Task<IActionResult> EditContact(Guid id, EditContactRequest request)
        {
            // get username for contacts owner field
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                return Unauthorized("Username claim not found.");
            }

            // map request to contact
            var newContactData = await _requestMapper.MapEditRequestToContactAsync(request, username);
            if (newContactData.IsError)
            {
                Problem(newContactData.Errors);
            }
            Contact contactData = newContactData.Value;
            contactData.Id = id;

            // validate entered data
            List<Error> errors = await _contactsService.ValidateContactDataAsync(contactData, username);
            if (errors.Count > 0)
            {
                return Problem(errors);
            }

            // get subcategory to check if it should be deleted
            ErrorOr<Contact> getContactResult = await _contactsService.GetContactAsync(id);
            if (getContactResult.IsError)
            {
                return Problem(getContactResult.Errors);
            }
            Subcategory subcategory = getContactResult.Value.Subcategory;

            // edit contact
            ErrorOr<Updated> editResult = await _contactsService.EditContactAsync(id, contactData);
            if (editResult.IsError)
            {
                return Problem(getContactResult.Errors);
            }

            // check if subcategory should be deleted
            if (subcategory.Contacts.Count == 0 && subcategory.Category.AllowCustomSubcategories)
            {
                var removeSubcategoryResult = await _dictionaryService.RemoveSubcategoryAsync(subcategory.Name);
                if (removeSubcategoryResult.IsError)
                {
                    return Problem(removeSubcategoryResult.Errors);
                }
            }

            return NoContent();
        }
        
    }
}
