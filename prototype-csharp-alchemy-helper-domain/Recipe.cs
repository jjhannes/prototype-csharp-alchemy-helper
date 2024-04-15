namespace prototype_csharp_alchemy_helper_domain;

using System.Text;

public class Recipe
{
    public string[] Ingredients { get; set; }

    public string[] GoodEffects { get; set; }

    public string[] BadEffects { get; set; }

    public Recipe(string[] ingredients, string[] effects)
    {
        this.Ingredients = ingredients;
        this.GoodEffects = effects.Where(e => !Functions.IsBadEffect(e)).ToArray();
        this.BadEffects = effects.Where(e => Functions.IsBadEffect(e)).ToArray();
    }

    public override string ToString()
    {
        StringBuilder stringifiedEntity = new StringBuilder();

        // Format ingredients
        stringifiedEntity.Append($"Combining [{string.Join(" & ", this.Ingredients)}]");
        stringifiedEntity.Append(Environment.NewLine);

        // Format good effects
        stringifiedEntity.Append($"Grants [{string.Join(" & ", this.GoodEffects)}]");
        stringifiedEntity.Append(Environment.NewLine);

        // Format bad effects
        if (this.BadEffects.Length > 0)
        {
            stringifiedEntity.Append($"But results in [{string.Join(" & ", this.BadEffects)}]");
        }
        else
        {
            stringifiedEntity.Append($"And has no bad effects");
        }

        return stringifiedEntity.ToString();
    }
}