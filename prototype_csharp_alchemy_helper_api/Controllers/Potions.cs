using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_api;

[ApiController]
public class Potions
{
    private IMediator _mediator;
    
    public Potions(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpGet]
    [Route("/csapi/v1/potions/recipes/with-effects")]
    public ActionResult GetRecipesGivenDesiredEffects([FromQuery(Name = "de")]string rawDesiredEffects)
    {
        if (rawDesiredEffects == null || rawDesiredEffects.Length < 1)
        {
            return new BadRequestResult();
        }

        string[] desiredEffects = rawDesiredEffects
            .Split(",")
            .Select(de => de.Trim())
            .ToArray();

        IEnumerable<Recipe> viableRecipes = this._mediator.DetermineRecipe(desiredEffects);
        CollectionResponse<Recipe> responsePayload = new CollectionResponse<Recipe>(viableRecipes);

        return new OkObjectResult(responsePayload);
    }
}
