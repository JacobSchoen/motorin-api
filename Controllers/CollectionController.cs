using MotorinApi.Models;
using Microsoft.AspNetCore.Mvc;
using MotorinApi.Dtos;

namespace MotorinApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CollectionController : Controller
{
    DataContextDapper _dapper;

    public CollectionController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetCollectionById/{collectionId}")]
    public ActionResult<Collections> GetCollectionById(Guid collectionId)
    {
        string sql = @"
        SELECT [CollectionId],
            [UserId],
            [CollectionType],
            [Name],
            [Description],
            [CreatedAt],
            [UpdatedAt],
            [IsDeleted],
            [DeletedAt] FROM CoreSchema.Collections
                WHERE CollectionId = @CollectionId";

        var parameters = new { CollectionId = collectionId };

        Collections collection = _dapper.LoadDataSingle<Collections>(sql, parameters);

        if (collectionId == null)
        {
            return NotFound();
        }
        return collection;
    }

    [HttpGet("GetCollections")]
    public IEnumerable<Collections> GetCollections()
    {
        string sql = @"
        SELECT [CollectionId],
            [UserId],
            [CollectionType],
            [Name],
            [Description],
            [CreatedAt],
            [UpdatedAt],
            [IsDeleted],
            [DeletedAt] FROM CoreSchema.Collections
        ";

        IEnumerable<Collections> collections = _dapper.LoadData<Collections>(sql);
        return collections;
    }

    [HttpPost("AddCollection")]
    public IActionResult AddCollection(CreateCollectionDto collection)
    {
        string sql = @"
        INSERT INTO CoreSchema.Collections (
            [UserId],
            [CollectionType],
            [Name],
            [Description],
            [IsDeleted],
            [DeletedAt]) 
        VALUES (
            @UserId,
            @CollectionType,
            @Name,
            @Description,
            @IsDeleted,
            @DeletedAt
        )";

        var parameters = new
        {
            collection.UserId,
            collection.CollectionType,
            collection.Name,
            collection.Description,
            collection.IsDeleted,
            collection.DeletedAt
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        throw new Exception("Failed to add Collection");
    }

}