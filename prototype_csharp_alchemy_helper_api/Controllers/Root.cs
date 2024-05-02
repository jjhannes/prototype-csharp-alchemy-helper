using Microsoft.AspNetCore.Mvc;
using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_api;

[ApiController]
public class Root : Controller
{
    [HttpGet]
    [Route($"/")]
    public ActionResult GetHealthcheck()
    {
        return new OkObjectResult($"CSAPI {Environment.GetEnvironmentVariable("ENV_PLATFORM") ?? "N/A"} RUNNING");
    }
}