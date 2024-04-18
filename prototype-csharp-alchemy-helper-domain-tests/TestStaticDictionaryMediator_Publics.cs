namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class TestStaticDictionaryMediator_Publics
{
    [TestClass]
    public class DetermineRecipe : BaseStaticDictionaryMediatorTester
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
                var viableRecipes = this._mediator.DetermineRecipe(desiredEffects);

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
    }
}
