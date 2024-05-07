namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class TestStaticDictionaryMediatorWithMorrowindRepo_Internals
{
    [TestClass]
    public class IsBadEffect : BaseStaticDictionaryMediatorWithMorrowindRepoTester
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
    public class GetCommonEffects : BaseStaticDictionaryMediatorWithMorrowindRepoTester
    {
        [TestMethod]
        [DataRow(new string[] { "Comberry", "Adamantium Ore" }, new string[] { "Reflect", "Restore Magicka" })]
        [DataRow(new string[] { "WOLFSBANE PETALS", "bittergreen petals" }, new string[] { "Restore Intelligence", "Invisibility", "Drain Magicka", "Drain Endurance" })]
        [DataRow(new string[] { "ALIT HIDE", "bonemeal", "ScUtTlE" }, new string[] { "Telekinesis" })]
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
    public class GetIngredientsWithEffects : BaseStaticDictionaryMediatorWithMorrowindRepoTester
    {
        [TestMethod]
        [DataRow(new string[] { "Telekinesis" }, new string[] { "Alit Hide", "Bonemeal", "Scuttle" })]
        [DataRow(new string[] { "Vampirism" }, new string[] { "Vampire Dust" })]
        [DataRow(new string[] { "Dispel" }, new string[] { "Timsa-Come-By flowers", "Pearl", "Moon Sugar", "Bungler's Bane" })]
        [DataRow(new string[] { "Dispel" }, new string[] { "Timsa-Come-By flowers", "Pearl", "Moon Sugar", "Bungler's Bane" })]
        [DataRow(new string[] { "night eye" }, new string[] { "Bear Pelt", "Daedra's Heart", "Grahl Eyeball", "Kagouti Hide", "Snow Bear Pelt", "Snow Wolf Pelt", "Wolf Pelt" })]
        [DataRow(new string[] { "CURE PARALYZATION" }, new string[] { "Willow Anther", "Scamp Skin", "Netch Leather", "Corkbulb Root" })]
        [DataRow(new string[] { "InViSiBiLiTy" }, new string[] { "Bittergreen Petals", "Diamond", "Lloramor Spines", "Wolfsbane Petals" })]
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

    [TestClass]
    public class IsIngredient : BaseStaticDictionaryMediatorWithMorrowindRepoTester
    {
        [TestMethod]
        [DataRow("Ash Salts")]
        [DataRow("bread")]
        [DataRow("Guar HIDE")]
        [DataRow("METEOR Slime")]
        [DataRow("Scrib CabBAGE")]
        [DataRow("TraMA Root")]
        public void ValidIngredients(string ingredient)
        {
            bool isIngredient = this._mediator.IsIngredient(ingredient);

            Assert.IsTrue(isIngredient);
        }

        [TestMethod]
        [DataRow("Kaas")]
        [DataRow("Bacon")]
        [DataRow("Kouse")]
        [DataRow("Melk")]
        [DataRow("Koffie")]
        [DataRow("Jou ma")]
        public void InvalidIngredients(string ingredient)
        {
            bool isIngredient = this._mediator.IsIngredient(ingredient);

            Assert.IsFalse(isIngredient);
        }
    }

    [TestClass]
    public class IsEffect : BaseStaticDictionaryMediatorWithMorrowindRepoTester
    {
        [TestMethod]
        [DataRow("Detect Animal")]
        [DataRow("Resist Frost")]
        [DataRow("DRAIN Fatigue")]
        [DataRow("Night EYE")]
        [DataRow("WATER BREATHING")]
        [DataRow("cure common disease")]
        [DataRow("paralYZE")]
        [DataRow("Resist Shock")]
        [DataRow("TELEKinesis")]
        [DataRow("Invisibility")]
        public void ValidEffects(string ingredient)
        {
            bool isEffect = this._mediator.IsEffect(ingredient);

            Assert.IsTrue(isEffect);
        }

        [TestMethod]
        [DataRow("Slaap")]
        [DataRow("Loop")]
        [DataRow("Blahblah")]
        [DataRow("Blahblah")]
        [DataRow("Moeg")]
        [DataRow("Opgewonde")]
        [DataRow("Jou pa")]
        public void InvalidEffects(string ingredient)
        {
            bool isEffect = this._mediator.IsEffect(ingredient);

            Assert.IsFalse(isEffect);
        }
    }

}