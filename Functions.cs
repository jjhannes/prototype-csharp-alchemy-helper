internal static class Functions
{
    internal static bool IsBadEffect(string effect)
    {
        effect = effect.ToLower();

        if (effect.Contains("cure") || effect.Contains("resist"))
        {
            return false;
        }

        return effect.Contains("burden") ||
            effect.Contains("poison") ||
            effect.Contains("blind") ||
            effect.Contains("damage") ||
            effect.Contains("drain") ||
            effect.Contains("paralyz") ||
            effect.Contains("weakness") ||
            effect.Contains("vampirism");
    }
}