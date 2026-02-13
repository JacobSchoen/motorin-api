using Microsoft.AspNetCore.Mvc;
using MotorinApi.Models;

namespace MotorinApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class HotWheelsMasterCatalogController : ControllerBase
{
    DataContextDapper _dapper;

    public HotWheelsMasterCatalogController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetHotwheelMasterCatalogItem/{catalogId}")]
    public ActionResult<HotWheelsMasterCatalog> GetSingleCatalog(Guid catalogId)
    {
        string sql = @"
        SELECT [CatalogId],
            [ProductLine],
            [ModelName],
            [ToyNumber],
            [SeriesName],
            [SeriesNumber],
            [CastingName],
            [ColorVariant],
            [TampoDesign],
            [TreasureHunt],
            [SuperTreasureHunt],
            [ManufactureYear],
            [ProductNumber],
            [OfficialImageUrl],
            [Description],
            [CreatedAt],
            [UpdatedAt] FROM CoreSchema.HotWheelsMasterCatalog
                WHERE CatalogId = @CatalogId";

        var parameters = new { CatalogId = catalogId };

        HotWheelsMasterCatalog? hotWheelsMasterCatalog = _dapper.LoadDataSingle<HotWheelsMasterCatalog>(sql, parameters);

        if (hotWheelsMasterCatalog == null)
        {
            return NotFound();
        }

        return hotWheelsMasterCatalog;
    }
}