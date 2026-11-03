using System.Linq;
using Todo.Core;
using Xunit;

namespace Todo.Core.Tests
{
    public class TodoListTests
    {
        [Fact]
        public void Add_IncreasesCount()
        {
            var list = new TodoList();
            var item = list.Add("  task  ");

            Assert.Equal(1, list.Count);
            Assert.Equal("task", item.Title);
        }

        [Fact]
        public void Remove_ById_Works()
        {
            var list = new TodoList();
            var item = list.Add("task");

            var removed = list.Remove(item.Id);

            Assert.True(removed);
            Assert.Equal(0, list.Count);
        }

        [Fact]
        public void Find_ReturnsMatches()
        {
            var list = new TodoList();
            list.Add("Buy milk");
            list.Add("Read book");

            var results = list.Find("buy").ToList();

            Assert.Single(results);
            Assert.Equal("Buy milk", results[0].Title);
        }
    }
}
