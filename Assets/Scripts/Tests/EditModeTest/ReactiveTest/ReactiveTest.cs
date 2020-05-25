using NUnit.Framework;

namespace Tests
{
    public class ReactiveTest
    {
        [Test]
        [TestCase(3, 7)]
        [TestCase(1, 1)]
        [TestCase(1, 0)]
        [TestCase(0, 1)]
        [TestCase(0, 0)]
        // [TestCase(0, -1)]
        // [TestCase(-1, 0)]
        // [TestCase(-1, -1)]
        public void SkipOperator_SkipNNumberOfCalls(int skipNumber, int callTimes)
        {
            int index = 0;
            ObservableProperty<int> property = new ObservableProperty<int> {Value = -1};
            property.Skip(skipNumber).Subscribe(i => index++);

            for (int i = 0; i < callTimes; i++)
            {
                property.Value = i;
            }

            int expectedResult = callTimes - skipNumber + 1;

            // For negative numbers
            // if (skipNumber <= 0)
            // {
            //     expectedResult = callTimes + 1;
            // }
            //
            // if (callTimes <= 0)
            // {
            //     expectedResult = 0;
            //     if (skipNumber <= 0)
            //     {
            //         expectedResult = 1;
            //     }
            // }

            Assert.AreEqual(expectedResult, index);
        }

        [Test]
        public void OnceOperator_CalledOnce()
        {
            int index = 0;
            ObservableProperty<int> property = new ObservableProperty<int>();
            property.Once().Subscribe(i => index = i);

            property.Value = 1;
            property.Value = 3;
            property.Value = 5;
            property.Value = 7;
            property.Value = 9;

            Assert.AreEqual(0, index);
        }

        [Test]
        [TestCase(1)]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(7)]
        [TestCase(2)]
        [TestCase(9)]
        [TestCase(-1)]
        public void OnceOperator_UsingSkipOperator_CalledOnce(int skipNumber)
        {
            int index = 0;
            ObservableProperty<int> property = new ObservableProperty<int>();
            property.Skip(skipNumber).Once().Subscribe(i => index++);

            for (int i = 0; i < skipNumber + 3; i++)
            {
                property.Value = i;
            }

            Assert.AreEqual(1, index);
        }

        [Test]
        public void OnceOperator_UsingWhereOperator_CalledOnce()
        {
            int index = 0;

            ObservableProperty<int> property = new ObservableProperty<int>();
            property.Where(i => i >= 2).Once().Subscribe(i => index = i);

            for (int i = 0; i < 5; i++)
            {
                property.Value = i;
            }

            Assert.AreEqual(2, index);
        }

        [Test]
        public void WhereOperator_CallsCorrectly()
        {
            int index = 0;

            ObservableProperty<int> property = new ObservableProperty<int>();
            property.Where(i => i == 10).Subscribe(i => index = i);

            for (int i = 0; i < 35; i++)
            {
                property.Value = i;
            }

            Assert.AreEqual(10, index);
        }
    }
}