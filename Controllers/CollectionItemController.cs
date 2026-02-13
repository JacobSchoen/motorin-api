using Microsoft.AspNetCore.Mvc;
using MotorinApi.Models;

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
    public ActionResult<CollectionItem> GetSingleUser(Guid collectionItemId)
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


}