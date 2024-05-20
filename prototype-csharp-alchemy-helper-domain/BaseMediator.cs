namespace prototype_csharp_alchemy_helper_domain;

using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using prototype_csharp_alchemy_helper_datastore;

public abstract class BaseMediator : IMediator
{
    protected readonly ILogger<IMediator> _logger;
    
    protected readonly IRepo _repo;

    public BaseMediator(ILogger<IMediator> logger, IRepo repo)
    {
        this._logger = logger;
        this._repo = repo;
    }

    public abstract Recipe GetRecipeFromIngedients(string[] ingredients);

    public abstract IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects);

    public abstract IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects, string[] excludedIngredients, bool excludeBadPotions, bool exactlyMatchDesiredEffects);

    public abstract IEnumerable<string> ValidateEffects(string[] effects);

    public abstract IEnumerable<string> ValidateIngredients(string[] ingredients);

    public abstract Dictionary<string, string[]> GetIngredientsWithDesiredEffects(string[] desiredEffects);
}
