namespace DataGenerator.Test
{
    public class GenDataTest
    {
        public string StringExample { get; set; }
        public int IntExample { get; set; }
        public double DoubleExample { get; set; }
        public bool BoolExample { get; set; }

        public GenDataTest Get()
        {
            return new GenDataTest
            {
                BoolExample = true,
                DoubleExample = 2.0,
                IntExample = 2,
                StringExample = "hello"
            };
        }
    }



}
