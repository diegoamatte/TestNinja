using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.Tests.Fundamentals
{
    public class StackTests
    {
        private Stack<string> _stack;

        public StackTests()
        {
            _stack = new Stack<string>();
        }

        [Fact]
        public void Count_ReturnsZero_WhenStackIsEmpty()
        {
            Assert.Equal(0, _stack.Count);
        }

        [Fact]
        public void Push_ThrowsArgumentNullException_WhenArgumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Push(null));
        }

        public void Push_AddsItemToStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            Assert.Equal(3, _stack.Count);
        }

        [Fact]
        public void Push_AddsLastItemToStackPeak()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            Assert.Equal("c", _stack.Peek());
        }

        [Fact]
        public void Pop_ThrowsInvalidOperationException_WhenIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Pop());
        }

        [Fact]
        public void Pop_ReturnsPeekOfStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var expected = _stack.Peek();
            var result = _stack.Pop();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Peek_ThrowsInvalidOperationException_WhenStackIsEmpty()
        {
            Assert.Throws<InvalidOperationException>(() => _stack.Peek());
        }

        [Fact]
        public void Peek_ReturnsTopOfStack()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Peek();

            Assert.Equal("c", result);
        }

    }
}
