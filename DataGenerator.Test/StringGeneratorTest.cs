using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Idioms;

namespace DataGenerator.Test
{
    public class StringGeneratorTest
    {
        private IFixture fixture;

        [SetUp]
        public void Setup()
        {
            fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Test]
        public void Guard_Assertion_Test()
        {
            var assertion = new GuardClauseAssertion(fixture);
            var constructors = typeof(ObjectToStringGenerator).GetConstructors();
            assertion.Verify(constructors);
        }

        [Test]
        public void If_Separator_Is_Null_Then_Default_To_Comma_Separator()
        {
            var sut = new ObjectToStringGenerator(new StringGeneratorOptions {Separator =  null});
            Assert.That(()=>sut.Options.Separator,Is.EqualTo(","));
        }

        [Test]
        public void If_WithPropertyName_Is_true_Then_Returns_Property_Name_on_String()
        {
            var genData = new GenDataTest().Get();

            var sut = new ObjectToStringGenerator(new StringGeneratorOptions {WithPropertyName = true});
            var actual = sut.Generate(genData);

            Assert.IsTrue(actual.Contains("BoolExample"));
        }

        [Test]
        public void If_WithPropertyName_Is_false_Then_Does_Not_Returns_Property_Name_on_String()
        {
            var genData = new GenDataTest().Get();

            var sut = new ObjectToStringGenerator(new StringGeneratorOptions {WithPropertyName = false});
            var actual = sut.Generate(genData);

            Assert.IsTrue(!actual.Contains("BoolExample"));
        }
    }
}
