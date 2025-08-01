using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Velo.Models;
using Velo.Services.Interfaces;

namespace Velo.Pages.Dashboard;

public class Index(IPDFService pdfService) : PageModel
{
    public List<PdfFile> PdfFiles { get; set; } = [];

    public async Task<IActionResult> OnGet()
    {
        PdfFiles = await pdfService.GetPdfFilesAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostUploadFileAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return new BadRequestObjectResult(new
            {
                message = "No file was uploaded."
            });
        }
        
        try
        {
            var newPdf = await pdfService.SavePdfAsync(file);
            
            return new OkObjectResult(new
            {
                message = "File uploaded successfully!", 
                pdf = newPdf
            });
        }
        catch (Exception ex)
        {
            return new StatusCodeResult(500);
        }
    }
}