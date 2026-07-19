using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly PdfService _pdfService;
        private readonly EmbeddingService _embeddingService;

        public TestController(
            PdfService pdfService,
            EmbeddingService embeddingService)
        {
            _pdfService = pdfService;
            _embeddingService = embeddingService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var text = _pdfService.ReadPdf("Documents/HRPolicy.pdf");

            var embedding = await _embeddingService.GenerateEmbeddingAsync(text);

            return Ok(new
            {
                Length = embedding.Length,
                FirstFiveValues = embedding.Take(5)
            });
        }
    }
}
