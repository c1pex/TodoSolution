using System;
using System.IO;
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

        [Fact]
        public void SaveAndLoad_RoundTrip_PreservesProperties()
        {
            var tempFile = Path.GetTempFileName();
            try
            {
                var originalList = new TodoList();
                var item1 = originalList.Add("First task");
                item1.MarkDone();
                var item2 = originalList.Add("Second task");

                originalList.Save(tempFile);

                var loadedList = new TodoList();
                loadedList.Load(tempFile);

                Assert.Equal(2, loadedList.Count);

                var loadedItem1 = Assert.Single(loadedList.Find("First task"));
                Assert.Equal(item1.Id, loadedItem1.Id);
                Assert.Equal("First task", loadedItem1.Title);
                Assert.True(loadedItem1.IsDone);

                var loadedItem2 = Assert.Single(loadedList.Find("Second task"));
                Assert.Equal(item2.Id, loadedItem2.Id);
                Assert.Equal("Second task", loadedItem2.Title);
                Assert.False(loadedItem2.IsDone);
            }
            finally
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
            }
        }
    }
}
