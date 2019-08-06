using JsonToJsonCompareEngine;
using NUnit.Framework;

namespace Tests
    {
    public class Tests
        {
        [SetUp]
        public void Setup()
            {
            }

        [Test]
        public void Compare()
            {
            Engine eng = new Engine("/Users/newtonacho/Documents/Folder1/Test.json", "/Users/newtonacho/Documents/Folder1/Test1.json");
            eng.Compare();
            Assert.Pass();
            }
        }
    }