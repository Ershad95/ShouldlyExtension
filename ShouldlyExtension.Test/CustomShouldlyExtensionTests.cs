using System;
using System.Linq.Expressions;
using Shouldly;
using Xunit;

namespace ShouldlyExtension.Test
{
    public class CustomShouldlyExtensionTests
    {
        private class TestObject
        {
            public int Foo { get; set; }
            public int Bar { get; set; }
        }

        [Fact]
        public void ShouldBeEquivalentTo_ThrowsException_WhenObjectsDoNotHaveSameProperties()
        {
            // Arrange
            var firstObject = new TestObject { Foo = 1 };
            var secondObject = new TestObject { Bar = 2 };

            // Act & Assert
           Assert.Throws<ShouldAssertException>(() =>
                firstObject.ShouldBeEquivalentTo(secondObject, Array.Empty<Expression<Func<TestObject, object>>>()));
        }

        [Fact]
        public void ShouldBeEquivalentTo_ThrowsException_WhenObjectsHaveDifferentNumberOfProperties()
        {
            // Arrange
            var firstObject = new TestObject { Foo = 1 };
            var secondObject = new TestObject { Foo = 1, Bar = 2 };

            // Act & Assert
          Assert.Throws<ShouldAssertException>(() =>
                firstObject.ShouldBeEquivalentTo(secondObject, Array.Empty<Expression<Func<TestObject, object>>>()));
        }

        [Fact]
        public void ShouldBeEquivalentTo_ThrowsException_WhenObjectDoesNotHaveProperty()
        {
            // Arrange
            var firstObject = new TestObject { Bar=1,Foo = 1 };
            var secondObject = new TestObject { Bar = 2, Foo = 1 };

            // Act & Assert
           Assert.Throws<ShouldAssertException>(() =>
                firstObject.ShouldBeEquivalentTo(secondObject, new Expression<Func<TestObject, object>>[] { x => x.Bar }));
        }

        [Fact]
        public void ShouldBeEquivalentTo_ThrowsException_WhenObjectPropertiesDoNotMatch()
        {
            // Arrange
            var firstObject = new TestObject{ Foo = 1 };
            var secondObject = new TestObject{ Foo = 2 };

            // Act & Assert
         Assert.Throws<ShouldAssertException>(() =>
                firstObject.ShouldBeEquivalentTo(secondObject, Array.Empty<Expression<Func<TestObject, object>>>()));
        }

        [Fact]
        public void ShouldBeEquivalentTo_DoesNotThrowException_WhenObjectsAreEquivalent()
        {
            // Arrange
            var firstObject = new { Foo = 1 };
            var secondObject = new { Foo = 1 };

            // Act & Assert
            firstObject.ShouldBeEquivalentTo(secondObject, Array.Empty<Expression<Func<object, object>>>());
        }

        [Fact]
        public void ShouldBeEquivalentTo_DoesNotThrowException_WhenObjectsHaveMatchingProperties()
        {
            // Arrange
            var firstObject = new TestObject { Foo = 1, Bar = 2 };
            var secondObject = new TestObject { Foo = 1, Bar = 3 };

            // Act & Assert
            firstObject.ShouldBeEquivalentTo(secondObject, new Expression<Func<TestObject, object>>[] { x => x.Foo });
        }

        [Fact]
        public void ShouldBeEquivalentTo_DoesNotThrowException_WhenComparingObjectToEnumerable()
        {
            // Arrange
            var obj = new { Foo = 1 };
            var objects = new[] { new { Foo = 1 }, new { Foo = 1 } };

            // Act & Assert
            obj.ShouldBeEquivalentTo(objects, new Expression<Func<object, object>>[0]);
        }
    }
}
   