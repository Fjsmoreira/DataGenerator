using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Idioms;

namespace DataGenerator.Test
{
    [TestFixture]
    public class RandomDataGeneratorTest
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
            var constructors = typeof(RandomDataGenerator<>).GetConstructors();
            assertion.Verify(constructors);
        }

        [TestCase(-1),TestCase(-25)]
        public void CreateMany_Invalid_Number_Of_Objects_Throw_Exception(int numOfObject)
        {
            var sut = fixture.Create<RandomDataGenerator<GenDataTest>>();
            Assert.That(()=> sut.CreateMany(numOfObject).ToList(), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void CreateMany_Number_Of_Objects_Is_Zero_Returns_Empty_Enumerable()
        {
            var sut = fixture.Create<RandomDataGenerator<GenDataTest>>();
            Assert.True(!sut.CreateMany(0).Any());
        }

        [Test]
        public void Create_Generates_Has_Many_String_Has_Objects()
        {
            var sut = fixture.Create<RandomDataGenerator<GenDataTest>>();
            Assert.True(!sut.CreateMany(0).Any());
        }

        [TestCase(5), TestCase(6), TestCase(18)]
        public void Create_Calls_String_Generator_Once(int numOfObject)
        {
            var stringGenerator = fixture.Freeze<Mock<IStringGenerator>>();
            var sut = fixture.Create<RandomDataGenerator<GenDataTest>>();
            sut.CreateMany(numOfObject).ToList();
            stringGenerator.Verify(_ => _.Generate(It.IsAny<GenDataTest>()),Times.Exactly(numOfObject));
        }
    }
}
