using Microsoft.AspNetCore.Mvc;
using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_api;

[ApiController]
public class Potions : Controller
{
    readonly Dictionary<string, string> parameterNames = new Dictionary<string, string>
    {
        { "desiredEffects", "de" },
        { "excludedIngredients", "ei" },
        { "excludeBadPotions", "ebp" },
        { "exactlyMatchDesiredEffects", "emde" },
        { "ingredients", "i" }
    };

    private readonly ILogger<Potions> _logger;
    private readonly IMediator _mediator;
    
    public Potions(ILogger<Potions> logger, IMediator mediator)
    {
        this._logger = logger;
        this._mediator = mediator;
    }

    
    private ActionResult GetRecipesGivenDesiredEffects()
    {
        string? rawDesiredEffects = this.HttpContext.Request.Query[this.parameterNames["desiredEffects"]];
        string? rawExcludedIngredients = this.HttpContext.Request.Query[this.parameterNames["excludedIngredients"]];
        string? rawExcludeBadPotions = this.HttpContext.Request.Query[this.parameterNames["excludeBadPotions"]];
        string? rawExactlyMatchDesiredEffects = this.HttpContext.Request.Query[this.parameterNames["exactlyMatchDesiredEffects"]];

        if (rawDesiredEffects == null || rawDesiredEffects.Length < 1)
        {
            return new BadRequestObjectResult($"Desired effects ({parameterNames["desiredEffects"]}) are required");
        }

        string[] desiredEffects = rawDesiredEffects
            .Split(",")
            .Select(de => de.Trim())
            .ToArray();
        string[] invalidEffects = this._mediator.ValidateEffects(desiredEffects).ToArray();

        if (invalidEffects.Any())
        {
            return new BadRequestObjectResult($"Invalid desired effects provided: [{string.Join(", ", invalidEffects)}]");
        }

        string[] excludedIngredients = new string[0];
        bool excludeBadPotions = false;
        bool exactlyMatchDesiredEffects = false;

        if (rawExcludedIngredients != null && rawExcludedIngredients.Length > 0)
        {
            excludedIngredients = rawExcludedIngredients
                .Split(",")
                .Select(de => de.Trim())
                .ToArray();
        }

        if (!string.IsNullOrEmpty(rawExcludeBadPotions))
        {
            excludeBadPotions = 
                rawExcludeBadPotions.ToLower() == "true" ||
                rawExcludeBadPotions.ToLower() == "1";
        }

        if (!string.IsNullOrEmpty(rawExactlyMatchDesiredEffects))
        {
            exactlyMatchDesiredEffects =
                rawExactlyMatchDesiredEffects.ToLower() == "true" ||
                rawExactlyMatchDesiredEffects.ToLower() == "1";
        }

        IEnumerable<Recipe> viableRecipes = this._mediator.GetRecipesWithDesiredEffects(desiredEffects, excludedIngredients, excludeBadPotions, exactlyMatchDesiredEffects);
        CollectionResponse<Recipe> responsePayload = new CollectionResponse<Recipe>(viableRecipes);

        return new OkObjectResult(responsePayload);
    }

    private ActionResult GetRecipeFromIngredients()
    {
        string? rawIngredients = this.HttpContext.Request.Query[parameterNames["ingredients"]];

        if (string.IsNullOrEmpty(rawIngredients))
        {
            return new BadRequestObjectResult($"Ingredients ({parameterNames["ingredients"]}) are required");
        }

        string[] ingredients = rawIngredients
            .Split(",")
            .Select(i => i.Trim())
            .ToArray();
        string[] invalidIngredients = this._mediator.ValidateIngredients(ingredients).ToArray();

        if (invalidIngredients.Any())
        {
            return new BadRequestObjectResult($"Invalid ingredients were provided: [{string.Join(", ", invalidIngredients)}]");
        }

        if (ingredients.Count() < 2)
        {
            return new BadRequestObjectResult($"A minimum of 2 ingredients are required");
        }
        else if (ingredients.Count() > 4)
        {
            return new BadRequestObjectResult($"A maximum of 4 ingredients are allowed");
        }

        Recipe resultingRecipe = this._mediator.GetRecipeFromIngedients(ingredients);

        return new OkObjectResult(resultingRecipe);
    }

    [HttpGet]
    [Route($"{Constants.ApiBasePath}/v2/{{dataset}}/ingredients/{{level}}/with-effects")]
    public ActionResult GetIngredientsWithEffects()
    {
        string? rawDesiredEffects = this.HttpContext.Request.Query[this.parameterNames["desiredEffects"]];

        if (rawDesiredEffects == null || rawDesiredEffects.Length < 1)
        {
            return new BadRequestObjectResult($"Desired effects ({parameterNames["desiredEffects"]}) are required");
        }

        string[] desiredEffects = rawDesiredEffects
            .Split(",")
            .Select(de => de.Trim())
            .ToArray();
        Dictionary<string, string[]> desiredEffectsIngredients = this._mediator.GetIngredientsWithDesiredEffects(desiredEffects);

        return new OkObjectResult(desiredEffectsIngredients);
    }

    [HttpGet]
    [Route($"{Constants.ApiBasePath}/v2/{{dataset}}/potions/recipes/with-effects")]
    [Route($"{Constants.ApiBasePath}/v2/{{dataset}}/potions/{{level}}/recipes/with-effects")]
    public ActionResult GetRecipesGivenDesiredEffectsV2(string dataset, string? level)
    {
        return this.GetRecipesGivenDesiredEffects();
    }

    [HttpGet]
    [Route($"{Constants.ApiBasePath}/v2/{{dataset}}/potions/from-ingredients")]
    [Route($"{Constants.ApiBasePath}/v2/{{dataset}}/potions/{{level}}/from-ingredients")]
    public ActionResult GetRecipeFromIngredientsV2(string dataset, string? level)
    {
        return this.GetRecipeFromIngredients();
    }
    
    [Obsolete("Upgraded to v2.")]
    [HttpGet]
    [Route($"{Constants.ApiBasePath}/v1/potions/recipes/with-effects")]
    public ActionResult GetRecipesFromDesiredEffects()
        => this.GetRecipesGivenDesiredEffects();

    [Obsolete("Upgraded to v2.")]
    [HttpGet]
    [Route($"{Constants.ApiBasePath}/v1/potions/from-ingredients")]
    public ActionResult GetRecipesFromIngredients()
        => this.GetRecipeFromIngredients();
}
