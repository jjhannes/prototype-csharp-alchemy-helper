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
    }
}
