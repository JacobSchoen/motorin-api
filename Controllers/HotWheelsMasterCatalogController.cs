using Microsoft.AspNetCore.Mvc;
using MotorinApi.Models;
using MotorinApi.Dtos;
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

    [HttpGet("GetHotwheelMasterCatalogItems")]
    public IEnumerable<HotWheelsMasterCatalog> GetHotwheelMaterCatalogItems()
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
            [UpdatedAt] FROM CoreSchema.HotWheelsMasterCatalog";

        IEnumerable<HotWheelsMasterCatalog> catalogItems = _dapper.LoadData<HotWheelsMasterCatalog>(sql);
        return catalogItems;
    }

    [HttpPost("AddHotWheelMasterCatalog")]
    public IActionResult AddHotWheelMasterCatalog(CreateHotWheelsCatalogDto catalog)
    {
        string sql = @"
        INSERT INTO CoreSchema.HotWheelsMasterCatalog
            ([ProductLine],
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
            [Description])
        VALUES(
            @ProductLine,
            @ModelName,
            @ToyNumber,
            @SeriesName,
            @SeriesNumber,
            @CastingName,
            @ColorVariant,
            @TampoDesign,
            @TreasureHunt,
            @SuperTreasureHunt,
            @ManufactureYear,
            @ProductNumber,
            @OfficialImageUrl,
            @Description    
        )";

        var parameters = new
        {
            catalog.ProductLine,
            catalog.ModelName,
            catalog.ToyNumber,
            catalog.SeriesName,
            catalog.SeriesNumber,
            catalog.CastingName,
            catalog.ColorVariant,
            catalog.TampoDesign,
            catalog.TreasureHunt,
            catalog.SuperTreasureHunt,
            catalog.ManufactureYear,
            catalog.ProductNumber,
            catalog.OfficialImageUrl,
            catalog.Description
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        throw new Exception("Failed to Add Catalog Item");
    }

    [HttpPut("UpdateHotWheelMasterCatalog")]
    public IActionResult UpdateHotWheelMasterCatalog(UpdateHotWheelsCatalogDto catalog)
    {
        string sql = @"
        UPDATE CoreSchema.HotWheelsMasterCatalog 
        SET [ProductLine] = @ProductLine,
            [ModelName] = @ModelName,
            [ToyNumber] = @ToyNumber,
            [SeriesName] = @SeriesName,
            [SeriesNumber] = @SeriesNumber,
            [CastingName] = @CastingName,
            [ColorVariant] = @ColorVariant,
            [TampoDesign] = @TampoDesign,
            [TreasureHunt] = @TreasureHunt,
            [SuperTreasureHunt] = @SuperTreasureHunt,
            [ManufactureYear] = @ManufactureYear,
            [ProductNumber] = @ProductNumber,
            [OfficialImageUrl] = @OfficialImageUrl,
            [Description] = @Description,
            [UpdatedAt] = GETUTCDATE()
        WHERE CatalogId = @CatalogId";

        var parameters = new
        {
            catalog.ProductLine,
            catalog.ModelName,
            catalog.ToyNumber,
            catalog.SeriesName,
            catalog.SeriesNumber,
            catalog.CastingName,
            catalog.ColorVariant,
            catalog.TampoDesign,
            catalog.TreasureHunt,
            catalog.SuperTreasureHunt,
            catalog.ManufactureYear,
            catalog.ProductNumber,
            catalog.OfficialImageUrl,
            catalog.Description,
            catalog.CatalogId
        };

        if (_dapper.ExecuteSql(sql, parameters))
        {
            return Ok();
        }
        throw new Exception("Failed to update catalog item");
    }
}

