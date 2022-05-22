using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Services.IServices;

namespace ArshaHamrah.Pages;

public class ContactModel : PageModel
{
    private readonly IMessageService _messageService;

    public ContactModel(IMessageService messageService)
    {
        _messageService = messageService;
    }


    [BindProperty] public Message UserMessage { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var result = await _messageService.Add(UserMessage);
        if (result.Code == 0) UserMessage = new Message();

        return Content(result.Message);
    }

    public void OnPost()
    {
    }
}