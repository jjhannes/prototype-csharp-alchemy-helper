﻿namespace prototype_csharp_alchemy_helper_domain;

public interface IMediator
{
    IEnumerable<Recipe> DetermineRecipe(string[] desiredEffects);
}