using Velo.Models;
using Velo.Models.Dtos;

namespace Velo.Services.Interfaces;

public interface IPDFService
{
    Task<PdfFile?> SavePdfAsync(IFormFile file);
    Task<List<PdfFile>> GetPdfFilesAsync();
    Task<PdfFile?> GetPdfFileByIdAsync(int id);
    Task<PdfFile> UpdatePdfAsync(PdfFile pdfFile);
    void SafeDeletePdfFile(PdfFile pdfFile);
    void DeletePdfFile(PdfFile pdfFile);
}