using Microsoft.AspNetCore.Mvc;

namespace ConvertDoc.Controllers;

[ApiController]
[Route("api/v1.0/teste")]
public class Teste : ControllerBase
{

    [HttpGet]
    //[Route("teste")]
    public IActionResult Get()
    {
        return Ok("Hello-Word");
    }

}
