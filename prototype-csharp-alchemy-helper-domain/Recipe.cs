namespace prototype_csharp_alchemy_helper_domain;

using System.Dynamic;
using System.Text;

public class Recipe
{
    internal Func<string, bool>? _isBadPredicate { get; private set; }

    public string[] Ingredients { get; internal set; }

    public string[] Effects { get; internal set; }

    public string[]? GoodEffects
    { 
        get => _isBadPredicate == null ? null : this.Effects.Where(e => !this._isBadPredicate(e)).ToArray();
    }

    public string[]? BadEffects 
    { 
        get => _isBadPredicate == null ? null : this.Effects.Where(e => this._isBadPredicate(e)).ToArray();
    }

    public Recipe(string[] ingredients, string[] effects)
        : this(ingredients, effects, null) { }

    public Recipe(string[] ingredients, string[] effects, Func<string, bool>? isBadPredicate)
    {
        this.Ingredients = ingredients;
        this.Effects = effects;

        if (isBadPredicate != null)
        {
            this._isBadPredicate = isBadPredicate;
        }
    }

    public override string ToString()
    {
        StringBuilder stringifiedEntity = new StringBuilder();

        // Format ingredients
        stringifiedEntity.Append($"Combining [{string.Join(" & ", this.Ingredients)}]");
        stringifiedEntity.Append(Environment.NewLine);

        // Format good effects
        if (this.GoodEffects != null)
        {
            stringifiedEntity.Append($"Grants [{string.Join(" & ", this.GoodEffects)}]");
            stringifiedEntity.Append(Environment.NewLine);
        }
        else
        {
            stringifiedEntity.Append($"Unfortunately good effects is undefined");
            stringifiedEntity.Append(Environment.NewLine);
        }

        // Format bad effects
        if (this.BadEffects != null)
        {
            if (this.BadEffects.Length > 0)
            {
                stringifiedEntity.Append($"But results in [{string.Join(" & ", this.BadEffects)}]");
            }
            else
            {
                stringifiedEntity.Append($"And has no bad effects");
            }
        }
        else
        {
            stringifiedEntity.Append($"But bad effects is undefined");
        }

        return stringifiedEntity.ToString();
    }
}