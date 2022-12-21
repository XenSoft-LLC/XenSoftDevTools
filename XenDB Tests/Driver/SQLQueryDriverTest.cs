using Xunit;
using System.Collections.Generic;
using XenDB.XenDriver.ConnectionThread;
using XenDB.XenDriver.DBDriver;
using XenDriver.Model;
using MySql.Data.MySqlClient;

namespace XenDB.test.Driver {
    [Collection("SQLQueryDriver")]
    public class SQLQueryDriverTest {
        public class TestModel : AbstractModel {
            public override string TableName => "test";
            public string TestValue { get; set; } = "badValue";

            public TestModel() { }
            public TestModel(string testValue) { TestValue = testValue; }
        }

        public static class SQLQueryTestUtil {
            private static string _createTestTableCommand = "CREATE TABLE test (ID INTEGER PRIMARY KEY auto_increment, TestValue text)";
            private static string _deleteTestTableCommand = "DROP TABLE IF EXISTS test";

            public static void SetupTestDatabase()
            {
                MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
                connection.Open();
                MySqlCommand deleteTableCommand = connection.CreateCommand();
                deleteTableCommand.CommandText = _deleteTestTableCommand;
                deleteTableCommand.ExecuteNonQuery();
                MySqlCommand createTestTableCommand = connection.CreateCommand();
                createTestTableCommand.CommandText = _createTestTableCommand;
                createTestTableCommand.ExecuteNonQuery();
            }

            public static void InsertTestModel(string testValue)
            {
                MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
                MySqlCommand insertCommand = connection.CreateCommand();
                connection.Open();
                insertCommand.CommandText = $"INSERT INTO test (TestValue) VALUES ('{testValue}')";
                insertCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectOneReturnsCorrectResult {

            [Fact]
            public void TestSelectOne()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                SQLQueryTestUtil.InsertTestModel("goodValue");
                SQLQueryDriver driver = new SQLQueryDriver();

                TestModel model = driver.SelectOne<TestModel>("SELECT * FROM test WHERE id = 1");

                Assert.NotNull(model);
                Assert.Equal(1, model.ID);
                Assert.Equal("goodValue", model.TestValue);
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectManyReturnsCorrectResults {
            [Fact]
            public void TestSelectMany()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                SQLQueryTestUtil.InsertTestModel("goodValue");
                SQLQueryTestUtil.InsertTestModel("badValue");
                SQLQueryTestUtil.InsertTestModel("okValue");

                List<TestModel> modelList = new SQLQueryDriver().SelectMany<TestModel>("SELECT * FROM test");

                Assert.NotNull(modelList);
                Assert.Equal(3, modelList.Count);
                Assert.Equal("goodValue", modelList.ToArray()[0].TestValue);
                Assert.Equal(1, modelList.ToArray()[0].ID);
                Assert.Equal("badValue", modelList.ToArray()[1].TestValue);
                Assert.Equal(2, modelList.ToArray()[1].ID);
                Assert.Equal("okValue", modelList.ToArray()[2].TestValue);
                Assert.Equal(3, modelList.ToArray()[2].ID);
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectStringReturnsCorrectString {
            [Fact]
            public void TestSelectString()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                MySqlConnection connection = ConnectionThreadManager.ConnectionThread();
                MySqlCommand insertCommand = connection.CreateCommand();
                connection.Open();
                insertCommand.CommandText = "INSERT INTO test (TestValue) VALUES ('goodValue')";
                insertCommand.ExecuteNonQuery();
                connection.Close();

                string resultString = new SQLQueryDriver().SelectString("SELECT TestValue FROM test WHERE id = 1");

                Assert.NotNull(resultString);
                Assert.Equal("goodValue", resultString);
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestInsertPostsCorrectObjectToDB {
            [Fact]
            public void TestInsert()
            {
                SQLQueryTestUtil.SetupTestDatabase();

                TestModel insertedModel = new SQLQueryDriver().Insert(new TestModel("goodValue"));

                Assert.NotNull(insertedModel);
                Assert.Equal(1, insertedModel.ID);
                Assert.Equal("goodValue", insertedModel.TestValue);
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestUpdatePostsCorrectUpdatesToDB {
            [Fact]
            public void TestUpdate()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                SQLQueryTestUtil.InsertTestModel("goodValue");

                TestModel model = new SQLQueryDriver().SelectOne<TestModel>("SELECT * FROM test WHERE id = 1");

                Assert.NotNull(model);
                Assert.Equal(1, model.ID);
                Assert.Equal("goodValue", model.TestValue);

                //Second Arrange Act Assert Block

                TestModel updatingModel = new TestModel("okValue") { ID = 1 };

                new SQLQueryDriver().Update(updatingModel);
                model = new SQLQueryDriver().SelectOne<TestModel>("SELECT * FROM test WHERE id = 1");

                Assert.NotNull(model);
                Assert.Equal(1, model.ID);
                Assert.Equal("okValue", model.TestValue);
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestDeleteRemovesObjectFromFB {
            [Fact]
            public void TestDelete()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                SQLQueryTestUtil.InsertTestModel("goodValue");
                SQLQueryTestUtil.InsertTestModel("badValue");
                SQLQueryTestUtil.InsertTestModel("okValue");

                string commandText = "SELECT * FROM test";
                List<TestModel> modelList = new SQLQueryDriver().SelectMany<TestModel>(commandText);

                Assert.NotNull(modelList);
                Assert.Equal(3, modelList.Count);

                //Second Arrange Act Assert Block

                TestModel modelToDelete = new TestModel { ID = 2 };

                new SQLQueryDriver().Delete(modelToDelete);

                modelList = new SQLQueryDriver().SelectMany<TestModel>(commandText);
                Assert.NotNull(modelList);
                Assert.Equal(2, modelList.Count);
                Assert.Equal("goodValue", modelList.ToArray()[0].TestValue);
                Assert.Equal(1, modelList.ToArray()[0].ID);
                Assert.Equal("okValue", modelList.ToArray()[1].TestValue);
                Assert.Equal(3, modelList.ToArray()[1].ID);
            }
        }
    }
}