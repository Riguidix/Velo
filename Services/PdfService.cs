using Microsoft.EntityFrameworkCore;

using Velo.Models;
using Velo.Services.Interfaces;

namespace Velo.Services;

public class PdfService(VeloDbContext context) : IPDFService
{
    public async Task<PdfFile?> SavePdfAsync(IFormFile pdfFile)
    {
        // There's no File
        if (pdfFile.Length == 0) return null;
        
        // The file should'nt be bigger than 10Mb
        if (pdfFile.Length >= (10 * 1024 * 1024)) return null;

        try
        {
            var extension = Path.GetExtension(pdfFile.FileName);

            // The file is not a PDF
            if (extension != ".pdf") return null;

            var uniqueName = $"{Guid.NewGuid().ToString()}{extension}";
            var folderPath = Path.Combine("PrivateFiles", "Pdfs");
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), folderPath);

            // The folder doesn't exists
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(fullPath);

            var filePath = Path.Combine(fullPath, uniqueName);
            
            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await pdfFile.CopyToAsync(fileStream);
            }

            var newFile = new PdfFile
            {
                FileName = pdfFile.FileName,
                FileSize = (double)pdfFile.Length / (1024 * 1024), // Calculate the Mb Size
                FilePath = fullPath,
                UploadDate = DateTime.Now
            };

            context.PdfFiles.Add(newFile);
            await context.SaveChangesAsync();

            return newFile;
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    
    public async Task<List<PdfFile>> GetPdfFilesAsync()
    {
        return await context.PdfFiles.ToListAsync();
    }

    public async Task<PdfFile> GetPdfFileByIdAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<PdfFile> UpdatePdfAsync(PdfFile pdfFile)
    {
        throw new NotImplementedException();
    }

    public void SafeDeletePdfFile(PdfFile pdfFile)
    {
        throw new NotImplementedException();
    }

    public void DeletePdfFile(PdfFile pdfFile)
    {
        throw new NotImplementedException();
    }
}