namespace paint.Tests
{
    public class BrushTests
    {
        Brush _sut;

        [TestCase("Wall Brush", 8, "Asian Paints")]
        [TestCase("Floor Brush", 10, "Dulux")]
        [TestCase("Ceiling Brush", 12, "Nerolac")]
        public void Brush_ShouldStoreValuesCorrectly(string proc, int size, string variant)
        {
            _sut = new Brush(proc, size, variant);

            Assert.That(_sut.Proc, Is.EqualTo(proc));
            Assert.That(_sut.Size, Is.EqualTo(size));
            Assert.That(_sut.Variant, Is.EqualTo(variant));
        }

        [TestCase(8)]
        [TestCase(10)]
        [TestCase(15)]
        public void Brush_Size_ShouldBeValid(int size)
        {
            _sut = new Brush("Wall Brush", size, "Asian Paints");
            Assert.That(_sut.Size, Is.GreaterThanOrEqualTo(8));
        }
    }
}