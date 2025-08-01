using Velo.Models;
using Velo.Models.Dtos;
using Velo.Services.Interfaces;

namespace Velo.Services;

public class PdfService(VeloDbContext context) : IPDFService
{
    private readonly VeloDbContext _context = context;

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

            // The folder doesn't exists
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fullPath = Path.Combine(folderPath, uniqueName);

            var newFile = new PdfFile
            {
                FileName = pdfFile.FileName,
                FileSize = (double)pdfFile.Length / (1024 * 1024), // Calculate the Mb Size
                FilePath = fullPath,
                UploadDate = DateTime.Now
            };

            _context.PdfFiles.Add(newFile);
            await _context.SaveChangesAsync();

            return newFile;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<PdfFile> GetPdfFileByIdAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<PdfFile>> GetPdfFilesAsync()
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