using Microsoft.AspNetCore.Mvc;
using MotorinApi.Models;
using MotorinApi.Dtos;

namespace MotorinApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class CollectionItemController : Controller
{
    DataContextDapper _dapper;

    public CollectionItemController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetCollectionItem/{collectionItemId}")]
    public ActionResult<CollectionItem> GetSingleCollectionItem(Guid collectionItemId)
    {
        string sql = @"
        SELECT [CollectionItemId],
            [CollectionId],
            [CatalogId],
            [Condition],
            [IsInPackage],
            [PackageCondition],
            [AcquisitionDate],
            [PurchasePrice],
            [EstimatedValue],
            [UserNotes],
            [UserImageUrl],
            [Quantity],
            [CreatedAt],
            [UpdatedAt],
            [IsDeleted],
            [DeletedAt] FROM CoreSchema.CollectionItems
                WHERE CollectionItemId = @CollectionItemId";

        var parameters = new { CollectionItemId = collectionItemId };

        CollectionItem? collectionItem = _dapper.LoadDataSingle<CollectionItem>(sql, parameters);

        if (collectionItem == null)
        {
            return NotFound();
        }

        return collectionItem;
    }

    [HttpGet("GetCollectionItems")]
    public IEnumerable<CollectionItem> GetCollectionItems()
    {
        string sql = @"
        SELECT [CollectionItemId],
            [CollectionId],
            [CatalogId],
            [Condition],
            [IsInPackage],
            [PackageCondition],
            [AcquisitionDate],
            [PurchasePrice],
            [EstimatedValue],
            [UserNotes],
            [UserImageUrl],
            [Quantity],
            [CreatedAt],
            [UpdatedAt],
            [IsDeleted],
            [DeletedAt] FROM CoreSchema.CollectionItems
        ";

        IEnumerable<CollectionItem> collections = _dapper.LoadData<CollectionItem>(sql);
        return collections;
    }

    [HttpPost("AddCollectionItem")]
    public IActionResult AddCollectionItem(CreateCollectionItemDto collectionItem)
    {
        string sql = @"
        INSERT INTO CoreSchema.CollectionItems (
            [CollectionId],
            [CatalogId],
            [Condition],
            [IsInPackage],
            [PackageCondition],
            [AcquisitionDate],
            [PurchasePrice],
            [EstimatedValue],
            [UserNotes],
            [UserImageUrl],
            [Quantity],
            [IsDeleted],
            [DeletedAt]) 
        VALUES (
            @CollectionId,
            @CatalogId,
            @Condition,
            @IsInPackage,
            @PackageCondition,
            @AcquisitionDate,
            @PurchasePrice,
            @EstimatedValue,
            @UserNotes,
            @UserImageUrl,
            @Quantity,
            @IsDeleted,
            @DeletedAt
        )";

        var parameters = new
        {
            collectionItem.CollectionId,
            collectionItem.CatalogId,
            collectionItem.Condition,
            collectionItem.IsInPackage,
            collectionItem.PackageCondition,
            collectionItem.AcquisitionDate,
            collectionItem.PurchasePrice,
            collectionItem.EstimatedValue,
            collectionItem.UserNotes,
            collectionItem.UserImageUrl,
            collectionItem.Quantity,
            collectionItem.IsDeleted,
            collectionItem.DeletedAt
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        throw new Exception("Failed to Add Collection Item");
    }

}