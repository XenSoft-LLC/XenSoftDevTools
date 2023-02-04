using Xunit;
using System.Collections.Generic;
using XenDB.Connection;
using XenDB.Model;
using MySql.Data.MySqlClient;
using XenDB.Driver;

namespace XenDB_Tests.Integration
{
    [Collection("SQLQueryDriver")]
    public class SQLQueryDriverIntegrationTests
    {
        public class TestModel : AbstractModel
        {
            public override string TableName => "test";
            public string TestValue { get; set; } = "badValue";

            public TestModel() { }
            public TestModel(string testValue) { TestValue = testValue; }
        }

        private static TestModel _goodModel;
        private static TestModel _badModel;
        private static TestModel _okModel;

        public static class SQLQueryDriverTestUtil
        {

            public static void SetupTestDatabase()
            {
                _goodModel = new TestModel("goodValue");
                _badModel = new TestModel("badValue");
                _okModel = new TestModel("okValue");

                MySqlCommand createTestTableCmd = ConnectionManager.
                    CreateDbCommand("CREATE SCHEMA IF NOT EXISTS testdb; USE testdb; CREATE TABLE test (ID INTEGER PRIMARY KEY auto_increment, TestValue text);");

                createTestTableCmd.ExecuteNonQuery();
                createTestTableCmd.Connection.Close();
            }

            public static void TeardownTestDatabase()
            {
                MySqlCommand deleteTestTableCmd = ConnectionManager.CreateDbCommand("USE testdb; DROP TABLE IF EXISTS test; DROP SCHEMA IF EXISTS testdb;");
                deleteTestTableCmd.ExecuteNonQuery();
                deleteTestTableCmd.Connection.Close();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectScalarReturnsCorrectResult
        {
            [Fact]
            public void TestSelectScalar()
            {
                SQLQueryDriverTestUtil.SetupTestDatabase();

                MySqlCommand insertCmd = ConnectionManager.CreateDbCommand("INSERT INTO test (TestValue) VALUES ('goodValue')");
                insertCmd.ExecuteNonQuery();
                insertCmd.Connection.Close();

                string resultString = QueryExecutor.SelectScalar("SELECT TestValue FROM test WHERE id = 1");

                Assert.NotNull(resultString);
                Assert.Equal("goodValue", resultString);
                SQLQueryDriverTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectOneReturnsCorrectResult
        {

            [Fact]
            public void TestSelectOne()
            {
                SQLQueryDriverTestUtil.SetupTestDatabase();
                _goodModel.Insert();

                TestModel model = QueryExecutor.SelectOne<TestModel>($"SELECT * FROM test WHERE ID=1;");

                Assert.NotNull(model);
                Assert.Equal(1, model.ID);
                Assert.Equal("goodValue", model.TestValue);
                SQLQueryDriverTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectManyReturnsCorrectResults
        {
            [Fact]
            public void TestSelectMany()
            {
                SQLQueryDriverTestUtil.SetupTestDatabase();
                _goodModel.Insert();
                _badModel.Insert();
                _okModel.Insert();

                List<TestModel> modelList = QueryExecutor.SelectMany<TestModel>("SELECT * FROM test");

                Assert.NotNull(modelList);
                Assert.Equal(3, modelList.Count);
                Assert.Equal("goodValue", modelList.ToArray()[0].TestValue);
                Assert.Equal(1, modelList.ToArray()[0].ID);
                Assert.Equal("badValue", modelList.ToArray()[1].TestValue);
                Assert.Equal(2, modelList.ToArray()[1].ID);
                Assert.Equal("okValue", modelList.ToArray()[2].TestValue);
                Assert.Equal(3, modelList.ToArray()[2].ID);
                SQLQueryDriverTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestInsertPostsCorrectObjectToDatabase
        {
            [Fact]
            public void TestInsert()
            {
                SQLQueryDriverTestUtil.SetupTestDatabase();
                _goodModel.Insert();

                TestModel insertedModel = QueryExecutor.SelectOne<TestModel>($"SELECT * FROM test WHERE ID=1;");

                Assert.NotNull(insertedModel);
                Assert.Equal(1, insertedModel.ID);
                Assert.Equal("goodValue", insertedModel.TestValue);
                SQLQueryDriverTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestUpdatePostsCorrectUpdatesToDatabase
        {
            [Fact]
            public void TestUpdate()
            {
                SQLQueryDriverTestUtil.SetupTestDatabase();
                _goodModel.Insert();

                _goodModel.TestValue = "okValue";
                _goodModel.Update();
                TestModel updatedModel = QueryExecutor.SelectOne<TestModel>($"SELECT * FROM test WHERE ID=1;");

                Assert.NotNull(updatedModel);
                Assert.Equal(1, updatedModel.ID);
                Assert.Equal("okValue", updatedModel.TestValue);
                SQLQueryDriverTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestDeleteRemovesCorrectObjectFromDatabase
        {
            [Fact]
            public void TestDelete()
            {
                SQLQueryDriverTestUtil.SetupTestDatabase();

                string commandText = "SELECT * FROM test";
                _goodModel.Insert();
                _badModel.Insert();
                _okModel.Insert();

                _badModel.Delete();
                List<TestModel> modelList = QueryExecutor.SelectMany<TestModel>(commandText);

                Assert.NotNull(modelList);
                Assert.Equal(2, modelList.Count);
                Assert.Equal("goodValue", modelList.ToArray()[0].TestValue);
                Assert.Equal(1, modelList.ToArray()[0].ID);
                Assert.Equal("okValue", modelList.ToArray()[1].TestValue);
                Assert.Equal(3, modelList.ToArray()[1].ID);

                SQLQueryDriverTestUtil.TeardownTestDatabase();
            }
        }
    }
}