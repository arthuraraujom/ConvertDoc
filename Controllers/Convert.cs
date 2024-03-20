using Microsoft.AspNetCore.Mvc;
using ConvertDoc.Commons;
using System.IO;

namespace ConvertDoc.Controllers;

[ApiController]
[Route("api/v1.0/convert")]
public class Convert : ControllerBase
{

    [HttpGet]
    [Route("htmlToPdf")]
    public IActionResult htmlToPdf(string pHtml)
    {

        if (pHtml == null || pHtml == "")
        {
            return BadRequest("O parametro HTML está vazio.");
        }

        return ConverterHtmlToPdf.Convert(pHtml);
    }

}
