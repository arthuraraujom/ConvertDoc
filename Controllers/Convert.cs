using Microsoft.AspNetCore.Mvc;
using ConvertDoc.Commons;
using System.IO;
using DinkToPdf.Contracts;

namespace ConvertDoc.Controllers;

[ApiController]
[Route("api/v1.0/convert")]
public class Convert : ControllerBase
{
   private readonly IConverter _converter;

    public Convert(IConverter converter){
        this._converter = converter;
    }


    [HttpGet]
    [Route("htmlToPdf")]
    public IActionResult htmlToPdf(string pHtml)
    {

        if (pHtml == null || pHtml == "")
        {
            return BadRequest("O parametro HTML está vazio.");
        }

        return ConverterHtmlToPdf.Convert(_converter, pHtml);
    }

}
