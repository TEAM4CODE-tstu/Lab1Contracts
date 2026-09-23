using System;
using System.Linq;
using Xunit;
using SortingAggregator.Domain;

namespace Contracts.Tests
{
    public class UnitTest1
    {

        [Fact]
        public void Sort_TypicalArray_ReturnsSortedCopy()
        {
            // Arrange
            int[] input = { 5, 3, 8, 1 };

            // Act
            int[] result = Operations.Sort(input);

            // Assert — упорядочен по неубыванию
            for (int i = 1; i < result.Length; i++)
                Assert.True(result[i - 1] <= result[i]);
        }

        [Fact]
        public void Sort_TypicalArray_PreservesMultiset()
        {
            int[] input = { 5, 3, 8, 1 };
            int[] result = Operations.Sort(input);

            Assert.True(result.OrderBy(x => x).SequenceEqual(input.OrderBy(x => x)));
        }

        [Fact]
        public void Sort_DoesNotMutateOriginal()
        {
            int[] input = { 5, 3, 8, 1 };
            int[] original = (int[])input.Clone();

            Operations.Sort(input);

            Assert.Equal(original, input);
        }

        [Fact]
        public void Sort_SingleElement_ReturnsSameElement()
        {
            int[] input = { 42 };
            int[] result = Operations.Sort(input);

            Assert.Single(result);
            Assert.Equal(42, result[0]);
        }

        [Fact]
        public void Sort_AlreadySorted_ReturnsSameOrder()
        {
            int[] input = { 1, 2, 3, 4, 5 };
            int[] result = Operations.Sort(input);

            Assert.Equal(input, result);
        }

        [Fact]
        public void Sort_ReverseSorted_ReturnsAscending()
        {
            int[] input = { 9, 7, 5, 3, 1 };
            int[] result = Operations.Sort(input);

            Assert.Equal(new[] { 1, 3, 5, 7, 9 }, result);
        }

        [Fact]
        public void Sort_DuplicateElements_PreservesAll()
        {
            int[] input = { 3, 1, 3, 1, 2 };
            int[] result = Operations.Sort(input);

            Assert.Equal(new[] { 1, 1, 2, 3, 3 }, result);
        }

        [Fact]
        public void Sort_NegativeNumbers_SortsCorrectly()
        {
            int[] input = { -5, -1, -3, 0, 2 };
            int[] result = Operations.Sort(input);

            Assert.Equal(new[] { -5, -3, -1, 0, 2 }, result);
        }

        [Fact]
        public void Sort_NullInput_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => Operations.Sort(null!));
        }

        [Fact]
        public void Sort_EmptyArray_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => Operations.Sort(Array.Empty<int>()));
        }


        [Fact]
        public void FindMinMax_TypicalArray_ReturnsCorrectMinMax()
        {
            int[] input = { 5, 3, 8, 1 };

            var (min, max) = Operations.FindMinMax(input);

            Assert.Equal(1, min);
            Assert.Equal(8, max);
        }

        [Fact]
        public void FindMinMax_MinLessOrEqualMax()
        {
            int[] input = { 10, -4, 7, 0, 3 };

            var (min, max) = Operations.FindMinMax(input);

            Assert.True(min <= max);
        }

        [Fact]
        public void FindMinMax_MinAndMaxExistInArray()
        {
            int[] input = { 5, 3, 8, 1 };

            var (min, max) = Operations.FindMinMax(input);

            Assert.Contains(min, input);
            Assert.Contains(max, input);
        }

        [Fact]
        public void FindMinMax_SingleElement_MinEqualsMax()
        {
            int[] input = { 7 };

            var (min, max) = Operations.FindMinMax(input);

            Assert.Equal(7, min);
            Assert.Equal(7, max);
        }

        [Fact]
        public void FindMinMax_AllSameElements_MinEqualsMax()
        {
            int[] input = { 4, 4, 4, 4 };

            var (min, max) = Operations.FindMinMax(input);

            Assert.Equal(4, min);
            Assert.Equal(4, max);
        }

        [Fact]
        public void FindMinMax_NegativeNumbers_ReturnsCorrect()
        {
            int[] input = { -10, -3, -7, -1 };

            var (min, max) = Operations.FindMinMax(input);

            Assert.Equal(-10, min);
            Assert.Equal(-1, max);
        }

        [Fact]
        public void FindMinMax_NullInput_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => Operations.FindMinMax(null!));
        }

        [Fact]
        public void FindMinMax_EmptyArray_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => Operations.FindMinMax(Array.Empty<int>()));
        }


        [Fact]
        public void Sum_TypicalArray_ReturnsCorrectSum()
        {
            int[] input = { 5, 3, 8, 1 };

            long result = Operations.Sum(input);

            Assert.Equal(17, result);
        }

        [Fact]
        public void Sum_SingleElement_ReturnsThatElement()
        {
            int[] input = { 42 };

            long result = Operations.Sum(input);

            Assert.Equal(42, result);
        }

        [Fact]
        public void Sum_NegativeAndPositive_ReturnsCorrectSum()
        {
            int[] input = { -5, 3, -8, 10 };

            long result = Operations.Sum(input);

            Assert.Equal(0, result);
        }

        [Fact]
        public void Sum_AllNegative_ReturnsNegativeSum()
        {
            int[] input = { -1, -2, -3 };

            long result = Operations.Sum(input);

            Assert.Equal(-6, result);
        }

        [Fact]
        public void Sum_ResultWithinBounds()
        {
            int[] input = { 5, 3, 8, 1 };

            long sum = Operations.Sum(input);
            int min = input.Min();
            int max = input.Max();

            Assert.True(sum >= (long)min * input.Length);
            Assert.True(sum <= (long)max * input.Length);
        }

        [Fact]
        public void Sum_NullInput_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => Operations.Sum(null!));
        }

        [Fact]
        public void Sum_EmptyArray_ThrowsException()
        {
            Assert.ThrowsAny<Exception>(() => Operations.Sum(Array.Empty<int>()));
        }
    }
}