namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class TestStaticDictionaryMediatorWithOblivionRepo_Public
{
    [TestClass]
    public class IngredientValidation : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public IngredientValidation() : base(0) { }
        
        [TestMethod]
        [DataRow(new string[] { "Alkanet Flower" }, new string[0])]
        [DataRow(new string[] { "Bergamot Seeds", "Cinnabar Polypore Red Cap" }, new string[0])]
        [DataRow(new string[] { "Flax Seeds", "Ham", "Mugwort Seeds" }, new string[0])]
        [DataRow(new string[] { "Somnalius Frond", "Tinder Polypore Cap", "Kaas" }, new string[] { "Kaas" })]
        [DataRow(new string[] { "Wheat Grain", "Stinkhorn Cap", "Bacon", "Mayo" }, new string[] { "Bacon", "Mayo" })]
        public void ValidateIngredients(string[] givenIngredients, string[] expectedInvalidIngredients)
        {
            IEnumerable<string> invalidIngredients = this._mediator.ValidateIngredients(givenIngredients);

            Assert.AreEqual(expectedInvalidIngredients.Count(), invalidIngredients.Count());
            Assert.IsTrue(new HashSet<string>(invalidIngredients).SetEquals(expectedInvalidIngredients));
        }
    }

    [TestClass]
    public class ApprenticeValidation : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public ApprenticeValidation() : base(1) { }

        [TestMethod]
        [DataRow(new string[] { "Restore Fatigue" }, new string[0])]
        [DataRow(new string[] { "Damage Luck", "Light", "Fortify Personality" }, new string[0])]
        [DataRow(new string[] { "Restore Health", "Burden", "Dispel" }, new string[] { "Dispel" })]
        [DataRow(new string[] { "Paralyze", "Cure Poison", "Fortify Willpower" }, new string[] { "Cure Poison", "Fortify Willpower" })]
        [DataRow(new string[] { "Resist Poison", "Shield", "Fortify Magicka" }, new string[] { "Shield", "Fortify Magicka" })]
        [DataRow(new string[] { "Fire Damage", "Feather", "Water Walking" }, new string[] { "Feather", "Water Walking" })]
        [DataRow(new string[] { "Resist Frost", "Damage Endurance", "Invisibility" }, new string[] { "Damage Endurance", "Invisibility" })]
        public void ValidateEffects(string[] givenEffects, string[] expectedInvalidEffects)
        {
            IEnumerable<string> invalidEffects = this._mediator.ValidateEffects(givenEffects);

            Assert.AreEqual(expectedInvalidEffects.Count(), invalidEffects.Count());
            Assert.IsTrue(new HashSet<string>(invalidEffects).SetEquals(expectedInvalidEffects));
        }
    }

    [TestClass]
    public class JourneymanValidation : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public JourneymanValidation() : base(2) { }

        [TestMethod]
        [DataRow(new string[] { "Resist Poison" }, new string[0])]
        [DataRow(new string[] { "Dispel", "Resist Fire" }, new string[0])]
        [DataRow(new string[] { "Resist Paralysis", "Restore Intelligence", "Damage Health" }, new string[] {  })]
        [DataRow(new string[] { "Shield", "Feather", "Damage Agility", "Fortify Speed" }, new string[] { "Fortify Speed" })]
        [DataRow(new string[] { "Light", "Cure Paralysis", "Damage Willpower", "Reflect Damage", "Fortify Strength" }, new string[] { "Reflect Damage", "Fortify Strength" })]
        [DataRow(new string[] { "Resist Frost", "Damage Endurance", "Silence", "Invisibility", "Fire Shield" }, new string[] { "Fire Shield" })]
        public void ValidateEffects(string[] givenEffects, string[] expectedInvalidEffects)
        {
            IEnumerable<string> invalidEffects = this._mediator.ValidateEffects(givenEffects);

            Assert.AreEqual(expectedInvalidEffects.Count(), invalidEffects.Count());
            Assert.IsTrue(new HashSet<string>(invalidEffects).SetEquals(expectedInvalidEffects));
        }
    }

    [TestClass]
    public class ExpertValidation : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public ExpertValidation() : base(3) { }

        [TestMethod]
        [DataRow(new string[] { "Light", "Fire Shield", "Burden" }, new string[] {  })]
        [DataRow(new string[] { "Shield", "Silence", "Night-Eye" }, new string[] {  })]
        [DataRow(new string[] { "Dispel", "Chameleon", "Feather", "Moegheid" }, new string[] { "Moegheid" })]
        [DataRow(new string[] { "Water Walking", "Fortify Speed", "Fire Shield", "Luiheid", "Swakheid" }, new string[] { "Luiheid", "Swakheid" })]
        [DataRow(new string[] { "Reflect Damage", "Fortify Strength", "Domheid", "Stadigheid" }, new string[] { "Domheid", "Stadigheid" })]
        public void ValidateEffects(string[] givenEffects, string[] expectedInvalidEffects)
        {
            IEnumerable<string> invalidEffects = this._mediator.ValidateEffects(givenEffects);

            Assert.AreEqual(expectedInvalidEffects.Count(), invalidEffects.Count());
            Assert.IsTrue(new HashSet<string>(invalidEffects).SetEquals(expectedInvalidEffects));
        }
    }

    [TestClass]
    public class MasterValidation : BaseStaticDictionaryMediatorWithOblivionRepoTester
    {
        public MasterValidation() : base(4) { }

        [TestMethod]
        [DataRow(new string[] { "Damage Fatigue", "Fortify Magicka", "Restore Fatigue", "Damage Magicka" }, new string[] {  })]
        [DataRow(new string[] { "Burden", "Fortify Health", "Invisibility", "Moegheid" }, new string[] { "Moegheid" })]
        [DataRow(new string[] { "Damage Endurance", "Damage Strength", "Luiheid", "Swakheid" }, new string[] { "Luiheid", "Swakheid" })]
        [DataRow(new string[] { "Damage Health", "Dispel", "Domheid" }, new string[] { "Domheid" })]
        [DataRow(new string[] { "Fire Shield", "Stadigheid" }, new string[] { "Stadigheid" })]
        public void ValidateEffects(string[] givenEffects, string[] expectedInvalidEffects)
        {
            IEnumerable<string> invalidEffects = this._mediator.ValidateEffects(givenEffects);

            Assert.AreEqual(expectedInvalidEffects.Count(), invalidEffects.Count());
            Assert.IsTrue(new HashSet<string>(invalidEffects).SetEquals(expectedInvalidEffects));
        }
    }

}
/*












*/
