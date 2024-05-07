using System.Runtime.CompilerServices;

namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class TestStaticDictionaryMediatorWithOblivionRepo_Internals
{
    [TestClass]
    public class IsBadEffect : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public IsBadEffect() : base(1) { }

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
            bool isBad = this._mediator.IsBadEffect(effect);

            Assert.IsFalse(isBad);
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
            bool isBad = this._mediator.IsBadEffect(effect);

            Assert.IsTrue(isBad);
        }
    }

    [TestClass]
    public class GetCommonEffectsAtApprentice : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public GetCommonEffectsAtApprentice() : base(1) { }

        [TestMethod]
        [DataRow(new string[] { "Alkanet Flower", "Clouded Funnel Cap" }, new string[] { "Restore Intelligence" })]
        [DataRow(new string[] { "Bonemeal", "Mort Flesh" }, new string[] { "Damage Fatigue" })]
        public void IngredientsMustHaveAtLeastAllExpectedEffects_ShouldBeTrue(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsTrue(expectedEffects.All(ee => commonEffects.Contains(ee)), "Not all expected effects found");
        }

        [TestMethod]
        [DataRow(new string[] { "Alkanet Flower", "Corn" }, new string[] { "Restore Intelligence" })]
        [DataRow(new string[] { "Bonemeal", "Motherwort Sprig" }, new string[] { "Damage Fatigue" })]
        public void IngredientsShouldNotHaveCommonEffectsOfNextLevel(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsFalse(expectedEffects.All(ee => commonEffects.Contains(ee)), "Common effects found");
        }
    }
    
    [TestClass]
    public class GetCommonEffectsAtJourneyman : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public GetCommonEffectsAtJourneyman() : base(2) { }

        [TestMethod]
        [DataRow(new string[] { "Alkanet Flower", "Corn" }, new string[] { "Restore Intelligence" })]
        [DataRow(new string[] { "Bonemeal", "Motherwort Sprig" }, new string[] { "Damage Fatigue" })]
        public void IngredientsMustHaveAtLeastAllExpectedEffects_ShouldBeTrue(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsTrue(expectedEffects.All(ee => commonEffects.Contains(ee)), "Not all expected effects found");
        }

        [TestMethod]
        [DataRow(new string[] { "Bloodgrass", "Daedra Silk" }, new string[] { "Chameleon" })]
        [DataRow(new string[] { "Daedra Silk", "Daedroth Teeth" }, new string[] { "Burden" })]
        public void IngredientsShouldNotHaveCommonEffectsOfNextLevel(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsFalse(expectedEffects.All(ee => commonEffects.Contains(ee)), "Common effects found");
        }
    }
    
    [TestClass]
    public class GetCommonEffectsAtExpert : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public GetCommonEffectsAtExpert() : base(3) { }

        [TestMethod]
        [DataRow(new string[] { "Bloodgrass", "Daedra Silk" }, new string[] { "Chameleon" })]
        [DataRow(new string[] { "Daedra Silk", "Daedroth Teeth" }, new string[] { "Burden" })]
        public void IngredientsMustHaveAtLeastAllExpectedEffects_ShouldBeTrue(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsTrue(expectedEffects.All(ee => commonEffects.Contains(ee)), "Not all expected effects found");
        }

        [TestMethod]
        [DataRow(new string[] { "Bloodgrass", "Columbine Root Pulp" }, new string[] { "Chameleon" })]
        [DataRow(new string[] { "Daedra Venin", "Fennel Seeds" }, new string[] { "Paralyze" })]
        public void IngredientsShouldNotHaveCommonEffectsOfNextLevel(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsFalse(expectedEffects.All(ee => commonEffects.Contains(ee)), "Common effects found");
        }
    }
    
    [TestClass]
    public class GetCommonEffectsAtMaster : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public GetCommonEffectsAtMaster() : base(4) { }

        [TestMethod]
        [DataRow(new string[] { "Bloodgrass", "Columbine Root Pulp" }, new string[] { "Chameleon" })]
        [DataRow(new string[] { "Daedra Venin", "Fennel Seeds" }, new string[] { "Paralyze" })]
        public void IngredientsMustHaveAtLeastAllExpectedEffects_ShouldBeTrue(string[] ingredients, string[] expectedEffects)
        {
            string[] commonEffects = this._mediator.GetCommonEffects(ingredients);

            Assert.IsTrue(expectedEffects.All(ee => commonEffects.Contains(ee)), "Not all expected effects found");
        }
    }
    
}
