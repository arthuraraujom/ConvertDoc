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
            lStrHtml = ModelDocuments.LoadDataHtmlAgreement(agreement, lStrHtml);

            return ConverterHtmlToPdf.Convert(lStrHtml);

        }
        catch (Exception ep) 
        {
            return BadRequest(ep.Message);
        }        
    }

    [HttpPost]
    [Route("budget")]
    public IActionResult Budget(DTOBudget budget)
    {
        try
        {
            var lStrHtml = ModelDocuments.FactoryHtml(TypeDocument.Budget);
            lStrHtml = ModelDocuments.LoadDataHtmlBudget(budget, lStrHtml);

            return ConverterHtmlToPdf.Convert(lStrHtml);

        }
        catch (Exception ep) 
        {
            return BadRequest(ep.Message);
        }        
    }

}
