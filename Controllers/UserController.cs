
using Microsoft.AspNetCore.Mvc;
using MotorinApi.Dtos;
namespace MotorinApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;

    public UserController(IConfiguration config)
    {
        Console.WriteLine(config.GetConnectionString("DefaultConnection"));
        _dapper = new DataContextDapper(config);

    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetSingleUser/{userId}")]
    public ActionResult<User> GetSingleUser(Guid userId)
    {
        string sql = @"
        SELECT [UserId],
            [UserName],
            [FirstName],
            [LastName],
            [Email],
            [Active] FROM CoreSchema.Users
                WHERE UserId = @UserId";

        var parameters = new { UserId = userId };

        User? user = _dapper.LoadDataSingle<User>(sql, parameters);

        if (user == null)
        {
            return NotFound();
        }

        return user;


    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sql = @"
        SELECT [UserId],
            [UserName],
            [FirstName],
            [LastName],
            [Email],
            [Active] 
        FROM CoreSchema.Users";

        IEnumerable<User> users = _dapper.LoadData<User>(sql);
        return users;
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE CoreSchema.Users
        SET [FirstName] = @FirstName,
            [UserName] = @UserName,
            [LastName] = @LastName,
            [Email] = @Email,
            [Active] = @Active
        WHERE UserId = @UserId";

        var parameters = new
        {
            user.FirstName,
            user.UserName,
            user.LastName,
            user.Email,
            user.Active,
            user.UserId
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");
    }


    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto user)
    {
        string sql = @"
        INSERT INTO CoreSchema.users( 
            [UserName],
            [FirstName],
            [LastName],
            [Email],
            [Active]
        ) VALUES (
            @UserName,
            @FirstName,
            @LastName,
            @Email,
            @Active
        )";

        var parameters = new
        {
            user.UserName,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Active,
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        throw new Exception("Failed to Add User");
    }


}
