using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class TestStaticDictionaryMediator_Internals
{
    [TestClass]
    public class IsBadEffect : BaseStaticDictionaryMediatorTester
    {
        [TestMethod]
        [DataRow("Cure Disease")]
        [DataRow("Resist Poison")]
        [DataRow("Fortify Speed")]
        [DataRow("Restore Strength")]
        [DataRow("Dispel")]
        [DataRow("Recall")]
        [DataRow("Levitate")]
        [DataRow("Swift Swim")]
        [DataRow("Water Walking")]
        [DataRow("Water Breathing")]
        [DataRow("Detect Enchantment")]
        [DataRow("Cure Paralyzation")]
        public void ShouldReturnFalse(string effect)
        {
            if (this._mediator != null)
            {
                bool isBad = this._mediator.IsBadEffect(effect);

                Assert.IsFalse(isBad);
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} not initialised.");
            }
        }
        
        [TestMethod]
        [DataRow("Burden")]
        [DataRow("Poison")]
        [DataRow("Paralyze")]
        [DataRow("Drain Intelligence")]
        [DataRow("Damage Magicka")]
        [DataRow("Vampirism")]
        public void ShouldReturnTrue(string effect)
        {
            if (this._mediator != null)
            {
                bool isBad = this._mediator.IsBadEffect(effect);

                Assert.IsTrue(isBad);
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} not initialised.");
            }
        }
    }
    
    [TestClass]
    public class GetCommonEffects : BaseStaticDictionaryMediatorTester
    {
        [TestMethod]
        [DataRow(new string[] { "Comberry", "Adamantium Ore" }, new string[] { "Reflect", "Restore Magicka" })]
        public void IngredientsMustHaveAllAndOnlyExpectedEffects_ShouldBeTrue(string[] ingredients, string[] expectedEffects)
        {
            if (this._mediator != null)
            {
                string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

                Assert.IsTrue(expectedEffects.All(ee => commonEffects.Contains(ee)), "Not all expected effects found");
                Assert.IsTrue(commonEffects.All(ce => expectedEffects.Contains(ce)), "Additional common effects found");
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} not initialised.");
            }
        }

        [TestMethod]
        [DataRow(new string[] { "Comberry", "Adamantium Ore" }, new string[] { "Restore Magicka" })]
        [DataRow(new string[] { "Comberry", "Adamantium Ore" }, new string[] { "Reflect" })]
        public void IngredientsMustHaveAtLeastSomeExpectedEffects_ShouldSucceed(string[] ingredients, string[] expectedEffects)
        {
            if (this._mediator != null)
            {
                string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

                Assert.IsTrue(expectedEffects.Any(ee => commonEffects.Contains(ee)));
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} not initialised.");
            }
        }
    }

    [TestClass]
    public class GetIngredientsWithEffects : BaseStaticDictionaryMediatorTester
    {
        [TestMethod]
        [DataRow(new string[] { "Telekinesis" }, new string[] { "Alit Hide", "Bonemeal", "Scuttle" })]
        [DataRow(new string[] { "Vampirism" }, new string[] { "Vampire Dust" })]
        [DataRow(new string[] { "Dispel" }, new string[] { "Timsa-Come-By flowers", "Pearl", "Moon Sugar", "Bungler's Bane" })]
        public void IngredientMustHaveAllAndOnlyExpectedEffects(string[] effects, string[] expectedIngredients)
        {
            if (this._mediator != null)
            {
                string[] ingredientsWithGivenEffects = this._mediator.GetIngredientsWithEffects(effects);

                Assert.IsTrue(expectedIngredients.All(ee => ingredientsWithGivenEffects.Contains(ee)), "Not all expected ingredients found");
                Assert.IsTrue(ingredientsWithGivenEffects.All(ee => expectedIngredients.Contains(ee)), "Additional ingredients with given effects found");
            }
            else
            {
                Assert.Fail($"{nameof(this._mediator)} not initialised.");
            }
        }
    }

}