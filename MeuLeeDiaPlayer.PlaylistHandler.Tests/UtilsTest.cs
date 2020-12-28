using MeuLeeDiaPlayer.PlaylistHandler.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeuLeeDiaPlayer.PlaylistHandler.Tests
{
    public class UtilsTest
    {

        [Test]
        public void When_ToDictionary_SourceIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var nullSource = (IEnumerable<int>)null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullSource.ToDictionary(0));
        }

        [Test]
        public void When_ToDictionary_SourceHasValue_IsBuiltCorrectly()
        {
            // Arrange
            const string str = "bob";
            var ienumerable = Enumerable.Range(1, 10);

            // Act
            var dict = ienumerable.ToDictionary(str);

            // Assert
            int i = 0;
            foreach (var kvp in dict)
            {
                Assert.AreEqual(kvp.Key, ++i);
                Assert.AreEqual(kvp.Value, str);
            }
        }

        [Test]
        public void When_GetRandomValueInList_ListIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var nullList = (List<string>)null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullList.GetRandomValueInList(new Random()));
        }

        [Test]
        public void When_GetRandomValueInList_RandomIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var list = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => list.GetRandomValueInList(null));
        }

        [Test]
        public void When_GetRandomValueInList_ListAndRandomHaveValues_ReturnsValueInList()
        {
            // Arrange
            var list = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var results = new List<int>();
            const int arbitraryNumber = 10;
            var r = new Random();

            // Act
            for (int i = 0; i < arbitraryNumber; i++)
            {
                results.Add(list.GetRandomValueInList(r));
            }

            // Assert
            foreach (var result in results)
            {
                Assert.IsTrue(list.Contains(result));
            }
        }

        [Test]
        public void When_IsEmpty_SourceIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var nullSource = (ICollection<string>)null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullSource.IsEmpty());
        }

        [Test]
        public void When_IsEmpty_SourceContainsValues_ReturnsFalse()
        {
            // Arrange
            var list = new List<int> { 0 };

            // Act
            bool result = list.IsEmpty();

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void When_IsEmpty_SourceIsEmpty_ReturnsTrue()
        {
            // Arrange
            var list = new List<string>();

            // Act
            bool result = list.IsEmpty();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void When_AddIfNotNull_SourceIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var nullSource = (ICollection<string>)null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullSource.AddIfNotNull(null));
        }

        [Test]
        public void When_AddIfNotNull_SourceHasValueAndArgIsNotNull_IncrementsCountByOne()
        {
            // Arrange
            var list = new List<int>();
            int arg = 2;

            // Act
            int countBefore = list.Count;
            list.AddIfNotNull(arg);
            int countAfter = list.Count;

            // Assert
            Assert.AreEqual(countBefore + 1, countAfter);
        }

        [Test]
        public void When_AddIfNotNull_SourceHasValueAndArgIsNull_DoesNotIncrementCountByOne()
        {
            // Arrange
            var list = new List<string>();

            // Act
            int countBefore = list.Count;
            list.AddIfNotNull(null);
            int countAfter = list.Count;

            // Assert
            Assert.AreEqual(countBefore, countAfter);
        }

        [Test]
        public void When_NotContains_SourceIsNull_ThrowsArgumentNull()
        {
            // Arrange
            var nullSource = (IEnumerable<string>)null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nullSource.NotContains(string.Empty));
        }

        [Test]
        public void When_NotContains_SourceHasValueAndArgIsNotInSource_ReturnsTrue()
        {
            // Arrange
            var list = new List<T> { new T(0), new T(1), new T(2), new T(4) };

            // Act
            bool result = list.NotContains(new T(3));

            Assert.IsTrue(result);
        }

        [Test]
        public void When_NotContains_SourceHasValueAndArgIsInSource_ReturnsFalse()
        {
            var commonElement = new T(3);

            // Arrange
            var list = new List<T> { new T(0), new T(1), new T(2), commonElement, new T(4) };

            // Act
            bool result = list.NotContains(commonElement);

            Assert.IsFalse(result);
        }
    }

    internal record T
    {
        internal int I { get; }

        public T(int i) => I = i;
    }
}
