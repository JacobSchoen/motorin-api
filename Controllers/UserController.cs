
using Microsoft.AspNetCore.Mvc;

namespace MotorinApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {

    }

    [HttpGet("GetUsers/{testValue}")]
    public string[] GetUsers(string testValue)
    {
        string[] responseArray = new string[]
        {
            "test 1",
            "test 2",
            testValue
        };
        return responseArray;

    }



}




