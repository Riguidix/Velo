namespace Velo.Models;

public class PdfFile
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public int PageCount { get; set; }
    public double FileSize { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadDate { get; set; }
}