using prototype_csharp_alchemy_helper_datastore;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("prototype-csharp-alchemy-helper-domain-tests")]
namespace prototype_csharp_alchemy_helper_domain;

public class StaticDictionaryMediator : BaseMediator
{
    [Obsolete($"Repo ought to be injected. Repo will default to [{nameof(StaticDictionaryRepo)}].")]
    public StaticDictionaryMediator(ILogger<IMediator> logger)
        : base(logger, new StaticDictionaryRepo()) { }

    public StaticDictionaryMediator(ILogger<IMediator> logger, IRepo repo)
        : base(logger, repo) { }

    internal bool IsBadEffect(string effect)
    {
        if (effect.Contains("cure", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("resist", StringComparison.InvariantCultureIgnoreCase))
        {
            return false;
        }

        return effect.Contains("burden", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("poison", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("blind", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("damage", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("drain", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("paralyz", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("weakness", StringComparison.InvariantCultureIgnoreCase) ||
            effect.Contains("vampirism", StringComparison.InvariantCultureIgnoreCase);
    }

    internal string[] GetCommonEffects(string[] ingredients)
    {
        List<KeyValuePair<string, string[]>> filteredIngredients = this._repo
            .GetEverything()
            .Where(i => ingredients.Contains(i.Key, StringComparer.InvariantCultureIgnoreCase))
            .ToList();

        List<string> commonEffects = new List<string>();

        for (int primary = 0; primary < filteredIngredients.Count; primary++)
        {
            KeyValuePair<string, string[]> primaryIngredient = filteredIngredients[primary];

            for (int secondary = primary + 1; secondary < filteredIngredients.Count; secondary++)
            {
                KeyValuePair<string, string[]> secondaryIngredient = filteredIngredients[secondary];

                string[] matches = primaryIngredient.Value
                    .Where(pe => secondaryIngredient.Value.Any(se => se.Equals(pe, StringComparison.InvariantCultureIgnoreCase)))
                    .ToArray();

                foreach (string match in matches)
                {
                    if (!commonEffects.Contains(match))
                    {
                        commonEffects.Add(match);
                    }
                }
            }
        }

        return commonEffects.ToArray();
    }
    
    internal bool IsIngredient(string ingredient)
    {
        return this._repo
            .GetEverything()
            .Keys
            .Any(k => k.Equals(ingredient, StringComparison.InvariantCultureIgnoreCase));
    }

    internal bool IsEffect(string effect)
    {
        return this._repo.GetEverything()
            .SelectMany(i => i.Value)
            .Distinct()
            .Any(e => e.Equals(effect, StringComparison.InvariantCultureIgnoreCase));
    }

    internal string[] GetIngredientsWithEffects(string[] effects)
    {
        return this._repo
            .GetEverything()
            .Where(i => i.Value.Any(ie => effects.Any(ge => ge.Equals(ie, StringComparison.InvariantCultureIgnoreCase))))
            .Select(i => i.Key)
            .ToArray();
    }

    /// <summary>
    /// Takes a collection of ingredients and returns any ingredients that are invalid.
    /// That means if the resulting collection is empty, all given ingredients are valid.
    /// Conversely, if the any items are returned, those ingredients are invalid
    /// </summary>
    /// <param name="ingredients"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IEnumerable<string> ValidateIngredients(string[] ingredients)
    {
        return ingredients
            .Where(i => !this.IsIngredient(i));
    }

    /// <summary>
    /// Takes a collection of effects and returns any effects that are invalid.
    /// That means if the resulting collection is empty, all given effects are valid.
    /// Conversely, if the any items are returned, those effects are invalid
    /// </summary>
    /// <param name="effects"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public override IEnumerable<string> ValidateEffects(string[] effects)
    {
        return effects
            .Where(e => !this.IsEffect(e));
    }

    public override IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects)
        => this.GetRecipesWithDesiredEffects(desiredEffects, new string[0], false, false);

    public IEnumerable<Recipe> DetermineRecipe(string[] desiredEffects, bool exactlyMatchDesiredEffects)
        => this.GetRecipesWithDesiredEffects(desiredEffects, new string[0], false, exactlyMatchDesiredEffects);

    public IEnumerable<Recipe> DetermineRecipe(string[] desiredEffects, string[] excludedIngredients)
        => this.GetRecipesWithDesiredEffects(desiredEffects, excludedIngredients, false, false);

    public IEnumerable<Recipe> DetermineRecipe(string[] desiredEffects, string[] excludedIngredients, bool excludeBadPotions)
        => this.GetRecipesWithDesiredEffects(desiredEffects, excludedIngredients, excludeBadPotions, false);

    public override IEnumerable<Recipe> GetRecipesWithDesiredEffects(string[] desiredEffects, string[] excludedIngredients, bool excludeBadPotions, bool exactlyMatchDesiredEffects)
    {
        List<Recipe> viableRecipes = new List<Recipe>();
        string[] possibleIngredients = this.GetIngredientsWithEffects(desiredEffects);

        if (excludedIngredients.Any())
        {
            int countBeforeFilter = possibleIngredients.Count();

            possibleIngredients = possibleIngredients
                .Where(pi => !excludedIngredients.Any(ei => ei.Equals(pi, StringComparison.InvariantCultureIgnoreCase)))
                .ToArray();

            if (countBeforeFilter != possibleIngredients.Count())
            {
                this._logger.LogWarning($"{countBeforeFilter - possibleIngredients.Count()} of {countBeforeFilter} excluded ingredients filtered out.");
            }
        }

        // Two ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                var commonEffects = this.GetCommonEffects([ primaryIngredient, secondaryIngredient ]);

                if (desiredEffects.All(de => commonEffects.Contains(de, StringComparer.InvariantCultureIgnoreCase)))
                {
                    viableRecipes.Add(new Recipe([ primaryIngredient, secondaryIngredient ], commonEffects, this.IsBadEffect));
                }
            }
        }

        // Three ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                
                for (int tertiary = secondary + 1; tertiary < possibleIngredients.Count(); tertiary++)
                {
                    string tertiaryIngredient = possibleIngredients[tertiary];
                    var commonEffects = this.GetCommonEffects([ primaryIngredient, secondaryIngredient, tertiaryIngredient ]);

                    if (desiredEffects.All(de => commonEffects.Contains(de, StringComparer.InvariantCultureIgnoreCase)))
                    {
                        viableRecipes.Add(new Recipe([ primaryIngredient, secondaryIngredient, tertiaryIngredient ], commonEffects, this.IsBadEffect));
                    }
                }
            }
        }

        // Four ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                
                for (int tertiary = secondary + 1; tertiary < possibleIngredients.Count(); tertiary++)
                {
                    string tertiaryIngredient = possibleIngredients[tertiary];
                    
                    for (int quaternary = tertiary + 1; quaternary < possibleIngredients.Count(); quaternary++)
                    {
                        string quaternaryIngredient = possibleIngredients[quaternary];
                        var commonEffects = this.GetCommonEffects([ primaryIngredient, secondaryIngredient, tertiaryIngredient, quaternaryIngredient ]);

                        if (desiredEffects.All(de => commonEffects.Contains(de, StringComparer.InvariantCultureIgnoreCase)))
                        {
                            viableRecipes.Add(new Recipe([ primaryIngredient, secondaryIngredient, tertiaryIngredient, quaternaryIngredient ], commonEffects, this.IsBadEffect));
                        }
                    }
                }
            }
        }

        if (excludeBadPotions)
        {
            int countBeforeFilter = viableRecipes.Count;

            viableRecipes = viableRecipes
                .Where(vr => !vr.BadEffects.Any())
                .ToList();

            if (countBeforeFilter != viableRecipes.Count)
            {
                this._logger.LogWarning($"{countBeforeFilter - viableRecipes.Count} of ${countBeforeFilter} recipies with bad effects filtered out.");
            }
        }

        if (exactlyMatchDesiredEffects)
        {
            int countBeforeFilter = viableRecipes.Count;

            viableRecipes = viableRecipes
                .Where(vr => new HashSet<string>(vr.Effects).SetEquals(desiredEffects))
                .ToList();

            if (countBeforeFilter != viableRecipes.Count)
            {
                this._logger.LogWarning($"{countBeforeFilter - viableRecipes.Count} of {countBeforeFilter} recipies with additional good effects filtered out.");
            }
        }

        viableRecipes = viableRecipes
            .OrderBy(vr => vr.BadEffects.Count())
            .ThenBy(vr => vr.Ingredients.Count())
            .ThenByDescending(vr => vr.GoodEffects.Count())
            .ToList();

        return viableRecipes;
    }

    public override Recipe GetRecipeFromIngedients(string[] ingredients)
    {
        string[] commonEffects = this.GetCommonEffects(ingredients);
        string[] sourceIngredients = this._repo
            .GetEverything()
            .Keys
            .Where(k => ingredients.Contains(k, StringComparer.InvariantCultureIgnoreCase))
            .ToArray();
        Recipe resultingResipe = new Recipe(sourceIngredients, commonEffects, this.IsBadEffect);
        
        return resultingResipe;
    }

    public override Dictionary<string, string[]> GetIngredientsWithDesiredEffects(string[] desiredEffects)
    {
        Dictionary<string, string[]> ingredientsWithDesiredEffects = this._repo
            .GetEverything()
            .Where(i => i.Value.Intersect(desiredEffects, StringComparer.CurrentCultureIgnoreCase).Any())
            .ToDictionary();

        Dictionary<string, string[]> desiredEffectsIngredients = desiredEffects
            .Select(de => 
            {
                string key = de;
                string[] value = ingredientsWithDesiredEffects
                    .Where(iwde => iwde.Value.Contains(de, StringComparer.CurrentCultureIgnoreCase))
                    .Select(iwde => iwde.Key)
                    .ToArray();

                return KeyValuePair.Create<string, string[]>(key, value);
            })
            .ToDictionary();

        return desiredEffectsIngredients;
    }

}
