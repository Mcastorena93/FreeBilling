
using FreeBilling.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FreeBilling.Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly IEmailServices _emailService;
        public ContactModel(IEmailServices emailService)
        {
            _emailService = emailService;
        }
        public string Title { get; set; } = "Contact Me";
        public string Message { get; set; } = " ";

        [BindProperty]
        public ContactViewModel Contact { get; set; }  = new ContactViewModel();
        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (ModelState.IsValid)
            {
                _emailService.SendMail("dev@gmail.com", Contact.Email,Contact.Subject,Contact.Body);
                Contact = new ContactViewModel();
                ModelState.Clear();
                Message = "Sent.";
            }
            else
            {
                Message = "Please fix the errors before sending.";
            }
        }
    }
}
