using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace FreeBilling.Web.Pages
{
    public class ContactModel : PageModel
    {
        public string Title { get; set; } = "Contact Me";
        public string Message { get; set; } = " ";
        public void OnGet()
        {
            
        }

        public void OnPost() 
        {
            Message = "Not Implemented";
        }
    }
}
