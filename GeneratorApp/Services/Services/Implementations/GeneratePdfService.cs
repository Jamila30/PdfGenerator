using Application.Services.Interfaces;
using Docnet.Core;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.Text;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using Document = iText.Layout.Document;
using PageDimensions = Docnet.Core.Models.PageDimensions;
using pdfDocum = UglyToad.PdfPig;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;
using PdfReader = iText.Kernel.Pdf.PdfReader;

namespace Application.Services.Implementations
{
    public class GeneratePdfService : IGeneratePdfService
    {
        public string ExtractDataFromPdf(string path)
        {
            using (PdfReader pdfReader = new PdfReader(path))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            using (var textWriter = new StringWriter())
            {
                for (int pageNum = 1; pageNum <= pdfDocument.GetNumberOfPages(); pageNum++)
                {
                    var page = pdfDocument.GetPage(pageNum);
                    var strategy = new iText.Kernel.Pdf.Canvas.Parser.Listener.LocationTextExtractionStrategy();
                    var currentText = iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(page, strategy);
                    textWriter.WriteLine(currentText);
                }
                return textWriter.ToString();
            }
        }
        public string ExtractDataFromPdf_PdfPig(string path)
        {
            var text = string.Empty;
            using (var pdf = pdfDocum.PdfDocument.Open(path))
            {
                foreach (var page in pdf.GetPages())
                {
                    // Either extract based on order in the underlying document with newlines and spaces.
                    text = ContentOrderTextExtractor.GetText(page);
                }
            }
            return text;
        }
        public string ExtractDataFromPdf_Docnet(string path)
        {
            var text = string.Empty;
            using (var docReader = DocLib.Instance.GetDocReader(path, new PageDimensions()))
            {
                for (var i = 0; i < docReader.GetPageCount(); i++)
                {
                    using (var pageReader = docReader.GetPageReader(i))
                    {
                        text = pageReader.GetText();
                    }
                }
            }
            return text;
        }

        public void WriteToPdf(string path, string targetWord, string newWord)
        {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append(ExtractDataFromPdf_Docnet(path));
            strBuild.Replace(targetWord, newWord);
            using (PdfWriter writer = new PdfWriter(path))
            using (PdfDocument pdfDocument = new PdfDocument(writer))
            using (Document document = new Document(pdfDocument))
            {
                Paragraph paragraph = new Paragraph(strBuild.ToString());
                document.Add(paragraph);
            }

        }
    }
}
