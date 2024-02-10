using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeneratorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController : ControllerBase
    {
        readonly IGeneratePdfService _generatePdfService;

        public PdfController(IGeneratePdfService generatePdfService)
        {
            _generatePdfService=generatePdfService;
        }

        [HttpPost("ExtractDataFromPdf")]
        public IActionResult ExtractDataFromPdf([FromQuery] string inPath)
        {
            string data = _generatePdfService.ExtractDataFromPdf(inPath);
            return Ok(data);
        }

        [HttpPost("ExtractDataFromPdf_PdfPig")]
        public IActionResult ExtractDataFromPdfPig([FromQuery] string inPath)
        {
            string data = _generatePdfService.ExtractDataFromPdf_PdfPig(inPath);
            return Ok(data);
        }

        [HttpPost("ExtractDataFromPdf_Docnet")]
        public IActionResult ExtractDataFromPdfDocnet([FromQuery] string inPath)
        {
            string data = _generatePdfService.ExtractDataFromPdf_Docnet(inPath);
            return Ok(data);
        }

        [HttpPost("WriteToPDf")]
        public IActionResult WriteToPDf(string inPath,string targetWord,string newWord)
        {
            _generatePdfService.WriteToPdf(inPath,targetWord,newWord);
            return Ok();
        }

    }
}
