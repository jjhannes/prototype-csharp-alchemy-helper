namespace prototype_csharp_alchemy_helper_domain;

public interface IMediator
{
    IEnumerable<string> ValidateIngredients(string[] ingredients);

    IEnumerable<string> ValidateEffects(string[] effects);

    IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects);
    
    IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects, string[] excludedIngredients, bool excludeBadPotions, bool exactlyMatchDesiredEffects);

    Recipe GetRecipeFromIngedients(string[] ingredients);

    Dictionary<string, string[]> GetIngredientsWithDesiredEffects(string[] desiredEffects);
}
