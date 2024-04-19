namespace prototype_csharp_alchemy_helper_domain;

public interface IMediator
{
    IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects);
    
    IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects, string[] excludedIngredients, bool excludeBadPotions, bool exactlyMatchDesiredEffects);

    Recipe GetRecipeFromIngedients(string[] ingredients);
}
