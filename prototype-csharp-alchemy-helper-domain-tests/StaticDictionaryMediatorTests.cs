using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_domain_tests;

[TestClass]
public class StaticDictionaryMediatorTests
{
    [TestClass]
    public class TestIsBadEffect
    {
        private StaticDictionaryMediator? _mediator;

        [TestInitialize]
        public void InitialiseDependencies()
        {
            this._mediator = new StaticDictionaryMediator();
        }

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
        }
    }
    
}