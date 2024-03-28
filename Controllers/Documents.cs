using ConvertDoc.Models;
using ConvertDoc.Commons;
using ConvertDoc.DTOs;
using Microsoft.AspNetCore.Mvc;
using static ConvertDoc.Commons.Consts;

namespace ConvertDoc.Controllers;

[ApiController]
[Route("api/v1.0/documents")]
public class Documents : ControllerBase
{

    [HttpPost]
    [Route("agreement")]
    public IActionResult Agreement(DTOAgreement agreement)
    {
        try
        {
            var lStrHtml = ModelDocuments.FactoryHtml(TypeDocument.Agreement);
            lStrHtml = ModelDocuments.LoadDataHtml(agreement, lStrHtml);

            return ConverterHtmlToPdf.Convert(lStrHtml);

        }
        catch (Exception ep) 
        {
            return BadRequest(ep.Message);
        }        
    }

    [HttpGet]
    [Route("teste")]
    public IActionResult Teste()
    {
        DTOAgreement agreement = new DTOAgreement();
        agreement.number = "001";

        return Ok(agreement);
    }

}
