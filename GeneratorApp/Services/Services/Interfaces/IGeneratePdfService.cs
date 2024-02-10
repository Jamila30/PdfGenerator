namespace Application.Services.Interfaces
{
    public interface IGeneratePdfService 
    {
        string ExtractDataFromPdf(string path);
        string ExtractDataFromPdf_PdfPig(string path);
        string ExtractDataFromPdf_Docnet(string path);
        public void WriteToPdf(string path, string targetWord, string newWord);
    }
}
