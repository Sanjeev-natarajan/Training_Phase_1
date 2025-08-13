using paint;
using Moq;

namespace Paint.Tests
{
    public class PainterWithMockTests
    {
        Painter _sut;
        Mock<IBrush> mockedBrush;

        [SetUp]
        public void Setup()
        {
            mockedBrush = new Mock<IBrush>();
            mockedBrush.Setup(x => x.Proc).Returns("Mock Brush");
            mockedBrush.Setup(x => x.Size).Returns(10);
            mockedBrush.Setup(x => x.Variant).Returns("Mock Brand");
            mockedBrush.Setup(x => x.IsHighVariant()).Returns(true);

            _sut = new Painter("Sanjeev", mockedBrush.Object);
        }

        [Test]
        public void Painter_ShouldHaveBrush()
        {
            Assert.IsNotNull(_sut.brush);
        }

        [Test]
        public void Painter_Name_ShouldBeCorrect()
        {
            Assert.That(_sut.Name, Is.EqualTo("Sanjeev"));
        }

        [Test]
        public void Painter_ShouldUseMockedBrushProperties()
        {
            Assert.That(_sut.brush.Proc, Is.EqualTo("Mock Brush"));
            Assert.That(_sut.brush.Size, Is.EqualTo(10));
            Assert.That(_sut.brush.Variant, Is.EqualTo("Mock Brand"));
            Assert.IsTrue(_sut.brush.IsHighVariant());
        }
    }
}
