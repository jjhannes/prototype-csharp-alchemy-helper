﻿using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class TestStaticDictionaryMediator_Publics
{
    [TestClass]
    public class GetRecipesWithDesiredEffects : BaseStaticDictionaryMediatorTester
    {
        [TestMethod]
        [DataRow(new string[] { "Swift Swim", "Water Breathing", "Restore Fatigue" }, 9)]
        [DataRow(new string[] { "Fortify Speed", "Water Walking" }, 168)]
        [DataRow(new string[] { "Restore Health" }, 246)]
        [DataRow(new string[] { "Restore Health", "Fortify Health" }, 216)]
        [DataRow(new string[] { "Restore Magicka" }, 91)]
        [DataRow(new string[] { "Restore Magicka", "Fortify Magicka" }, 126)]
        [DataRow(new string[] { "Cure Common Disease", "Cure Poison" }, 588)]
        [DataRow(new string[] { "Cure Blight Disease", "Cure Poison" }, 71)]
        [DataRow(new string[] { "Cure Blight Disease", "Cure Common Disease" }, 63)]
        public void DesiredEffectsGivesXRecipesWithAllDesiredEffects(string[] desiredEffects, int expectedRecipeCount)
        {
            if (this._mediator != null)
            {
                var viableRecipes = this._mediator.GetRecipesWithDesiredEffects(desiredEffects);

                Assert.AreEqual(expectedRecipeCount, viableRecipes.Count());
                Assert.IsTrue(viableRecipes.All(r => desiredEffects.All(de => r.Effects.Any(re => re == de))));
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} is not initialised");
            }
        }

        [TestMethod]
        [DataRow(new string[] { "Swift Swim", "Water Breathing", "Restore Fatigue" }, new string[] { "Daedra Skin", "Golden Sedge Flowers", "Pearl" }, 2)]
        [DataRow(new string[] { "Fortify Speed", "Water Walking" }, new string[] { "Meadow Rye", "Nirthfly Stalks" }, 90)]
        [DataRow(new string[] { "Restore Health" }, new string[] { "Emerald", "Raw Stalhrim", "Sweetpulp" }, 50)]
        [DataRow(new string[] { "Restore Health", "Fortify Health" }, new string[] { "Emerald", "Raw Stalhrim", "Sweetpulp" }, 90)]
        [DataRow(new string[] { "Restore Magicka" }, new string[] { "Adamantium Ore", "Heartwood", "Frost Salts", "Void Salts" }, 4)]
        [DataRow(new string[] { "Restore Magicka", "Fortify Magicka" }, new string[] { "Adamantium Ore", "Heartwood", "Frost Salts", "Void Salts" }, 18)]
        public void DesiredEffectsExcludeIngredientsGivesXRecipesWithDesiredEffects(string[] desiredEffects, string[] excludedIngredients, int expectedRecipeCount)
        {
            if (this._mediator != null)
            {
                var viableRecipes = this._mediator.DetermineRecipe(desiredEffects, excludedIngredients);

                Assert.AreEqual(expectedRecipeCount, viableRecipes.Count());
                Assert.IsTrue(viableRecipes.All(r => desiredEffects.All(de => r.Effects.Any(re => re == de))));
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} is not initialised");
            }
        }

        [TestMethod]
        [DataRow(new string[] { "Swift Swim", "Water Breathing", "Restore Fatigue" }, new string[] { "Daedra Skin", "Golden Sedge Flowers", "Pearl" }, 2, 2)]
        [DataRow(new string[] { "Fortify Speed", "Water Walking" }, new string[] { "Meadow Rye", "Nirthfly Stalks", "Moon Sugar", "Snow Bear Pelt", "Snow Wolf Pelt", "Wolf Pelt" }, 6, 0)]
        [DataRow(new string[] { "Restore Health", "Fortify Health" }, new string[] { "Human Flesh", "Corprus Weepings", "Vampire Dust", "Emerald", "Raw Stalhrim", "Sweetpulp" }, 10, 3)]
        [DataRow(new string[] { "Restore Magicka", "Fortify Magicka" }, new string[] { "Adamantium Ore", "Heartwood", "Frost Salts", "Void Salts", "Emerald" }, 9, 9)]
        public void DesiredEffectsExcludeIngredientsGivesXRecipesWithDesiredEffectsAndYRecipesExcludingBadPotions(string[] desiredEffects, string[] excludedIngredients, int expectedRecipeCount, int expectedGoodPotionRecipeCount)
        {
            if (this._mediator != null)
            {
                var viableRecipes = this._mediator.DetermineRecipe(desiredEffects, excludedIngredients);
                var viableGoodRecipes = this._mediator.DetermineRecipe(desiredEffects, excludedIngredients, true);

                Assert.AreEqual(expectedRecipeCount, viableRecipes.Count());
                Assert.AreEqual(expectedGoodPotionRecipeCount, viableGoodRecipes.Count());
                Assert.IsTrue(viableRecipes.All(r => desiredEffects.All(de => r.Effects.Any(re => re == de))));
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} is not initialised");
            }
        }

        [TestMethod]
        [DataRow(new string[] { "Swift Swim", "Water Breathing", "Restore Fatigue" }, 5)]
        [DataRow(new string[] { "Fortify Speed", "Water Walking" }, 22)]
        [DataRow(new string[] { "Restore Health", "Fortify Health" }, 66)]
        [DataRow(new string[] { "Restore Magicka", "Fortify Magicka" }, 66)]
        public void DesiredEffectsExactlyMatched(string[] desiredEffects, int expectedRecipeCount)
        {
            if (this._mediator != null)
            {
                var viableRecipes = this._mediator.DetermineRecipe(desiredEffects, true);

                Assert.AreEqual(expectedRecipeCount, viableRecipes.Count());
                Assert.IsTrue(viableRecipes.All(vr => vr.Effects.Count() == desiredEffects.Count()));
                Assert.IsTrue(viableRecipes.All(r => desiredEffects.All(de => r.Effects.Any(re => re == de))));
                Assert.IsTrue(desiredEffects.All(de => viableRecipes.All(r => r.Effects.Any(re => re == de))));
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} is not initialised");
            }
        }
    }

    [TestClass]
    public class GetRecipeFromIngedients : BaseStaticDictionaryMediatorTester
    {
        [TestMethod]
        [DataRow(new string[] { "Bread", "Hound Meat" }, new string[] { "Restore Fatigue" })]
        [DataRow(new string[] { "Corprus Weepings", "Small Corprusmeat Hunk" }, new string[] { "Drain Fatigue" })]
        [DataRow(new string[] { "Bread", "Corprus Weepings" }, new string[] {  })]
        public void FromGivenIngredientsExpectGivenEffects(string[] givenIngredients, string[] givenEffects)
        {
            Recipe resultingRecipe = this._mediator.GetRecipeFromIngedients(givenIngredients);

            Assert.AreEqual(givenEffects.Count(), resultingRecipe.Effects.Count());

            if (resultingRecipe.Effects.Count() > 0)
            {
                Assert.IsTrue(new HashSet<string>(resultingRecipe.Effects).SetEquals(givenEffects));
            }
        }
    }

}
