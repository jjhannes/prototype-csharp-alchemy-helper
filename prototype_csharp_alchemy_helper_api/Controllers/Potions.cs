using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_api;

[ApiController]
public class Potions : Controller
{
    readonly Dictionary<string, string> parameterNames = new Dictionary<string, string>
    {
        { "desiredEffects", "de" },
        { "excludedIngredients", "ei" }
    };

    private IMediator _mediator;
    
    public Potions(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    [Route("/csapi/v1/potions/recipes/with-effects")]
    public ActionResult GetRecipesGivenDesiredEffects()
    {
        string? rawDesiredEffects = this.HttpContext.Request.Query[this.parameterNames["desiredEffects"]];
        string? rawExcludedIngredients = this.HttpContext.Request.Query[this.parameterNames["excludedIngredients"]];

        if (rawDesiredEffects == null || rawDesiredEffects.Length < 1)
        {
            return new BadRequestResult();
        }

        string[] desiredEffects = rawDesiredEffects
            .Split(",")
            .Select(de => de.Trim())
            .ToArray();
        string[] excludedIngredients = new string[0];

        if (rawExcludedIngredients != null && rawExcludedIngredients.Length > 0)
        {
            excludedIngredients = rawExcludedIngredients
                .Split(",")
                .Select(de => de.Trim())
                .ToArray();
        }

        IEnumerable<Recipe> viableRecipes = this._mediator.DetermineRecipe(desiredEffects, excludedIngredients);
        CollectionResponse<Recipe> responsePayload = new CollectionResponse<Recipe>(viableRecipes);

        return new OkObjectResult(responsePayload);
    }
}
