using Xunit;
using System.Collections.Generic;
using XenDB.Connection;
using XenDB.Model;
using MySql.Data.MySqlClient;
using XenDB.Driver;

namespace XenDB.test.Driver {
    [Collection("SQLQueryDriver")]
    public class SQLQueryDriverTest {
        public class TestModel : AbstractModel {
            public override string TableName => "test";
            public string TestValue { get; set; } = "badValue";

            public TestModel() { }
            public TestModel(string testValue) { TestValue = testValue; }
        }

        private static TestModel _goodModel;
        private static TestModel _badModel;
        private static TestModel _okModel;

        public static class SQLQueryTestUtil {

            public static void SetupTestDatabase() {
                _goodModel = new TestModel("goodValue");
                _badModel = new TestModel("badValue");
                _okModel = new TestModel("okValue");

                MySqlCommand createTestTableCmd = ConnectionManager.
                    CreateDbCommand(" CREATE SCHEMA IF NOT EXISTS testdb; USE testdb; CREATE TABLE test (ID INTEGER PRIMARY KEY auto_increment, TestValue text);");

                createTestTableCmd.ExecuteNonQuery();
                createTestTableCmd.Connection.Close();
            }

            public static void TeardownTestDatabase() {
                MySqlCommand deleteTestTableCmd = ConnectionManager.CreateDbCommand("USE testdb; DROP TABLE IF EXISTS test; DROP SCHEMA IF EXISTS testdb;");
                deleteTestTableCmd.ExecuteNonQuery();
                deleteTestTableCmd.Connection.Close();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectOneReturnsCorrectResult {

            [Fact]
            public void TestSelectByID()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                _goodModel.Insert();

                TestModel model = SQLQueryDriver.SelectByID<TestModel>(1);

                Assert.NotNull(model);
                Assert.Equal(1, model.ID);
                Assert.Equal("goodValue", model.TestValue);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectManyReturnsCorrectResults {
            [Fact]
            public void TestSelectMany()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                _goodModel.Insert();
                _badModel.Insert();
                _okModel.Insert();

                List<TestModel> modelList = SQLQueryDriver.SelectMany<TestModel>("SELECT * FROM test");

                Assert.NotNull(modelList);
                Assert.Equal(3, modelList.Count);
                Assert.Equal("goodValue", modelList.ToArray()[0].TestValue);
                Assert.Equal(1, modelList.ToArray()[0].ID);
                Assert.Equal("badValue", modelList.ToArray()[1].TestValue);
                Assert.Equal(2, modelList.ToArray()[1].ID);
                Assert.Equal("okValue", modelList.ToArray()[2].TestValue);
                Assert.Equal(3, modelList.ToArray()[2].ID);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestSelectStringReturnsCorrectString {
            [Fact]
            public void TestSelectString()
            {
                SQLQueryTestUtil.SetupTestDatabase();

                MySqlCommand insertCmd = ConnectionManager.CreateDbCommand("INSERT INTO test (TestValue) VALUES ('goodValue')");
                insertCmd.ExecuteNonQuery();
                insertCmd.Connection.Close();

                string resultString = SQLQueryDriver.SelectString("SELECT TestValue FROM test WHERE id = 1");

                Assert.NotNull(resultString);
                Assert.Equal("goodValue", resultString);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestInsertPostsCorrectObjectToDB {
            [Fact]
            public void TestInsert()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                _goodModel.Insert();

                TestModel insertedModel = SQLQueryDriver.SelectByID<TestModel>(1);

                Assert.NotNull(insertedModel);
                Assert.Equal(1, insertedModel.ID);
                Assert.Equal("goodValue", insertedModel.TestValue);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestUpdatePostsCorrectUpdatesToDB {
            [Fact]
            public void TestUpdate()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                _goodModel.Insert();
                _goodModel.TestValue = "okValue";

                _goodModel.Update();
                TestModel updatedModel = SQLQueryDriver.SelectByID<TestModel>(1);

                Assert.NotNull(updatedModel);
                Assert.Equal(1, updatedModel.ID);
                Assert.Equal("okValue", updatedModel.TestValue);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestDeleteRemovesObjectFromFB {
            [Fact]
            public void TestDelete()
            {
                SQLQueryTestUtil.SetupTestDatabase();
                _goodModel.Insert();
                _badModel.Insert();
                _okModel.Insert();

                string commandText = "SELECT * FROM test";
                List<TestModel> modelList = SQLQueryDriver.SelectMany<TestModel>(commandText);

                Assert.NotNull(modelList);
                Assert.Equal(3, modelList.Count);

                //Second Arrange Act Assert Block

                _badModel.Delete();

                modelList = SQLQueryDriver.SelectMany<TestModel>(commandText);
                Assert.NotNull(modelList);
                Assert.Equal(2, modelList.Count);
                Assert.Equal("goodValue", modelList.ToArray()[0].TestValue);
                Assert.Equal(1, modelList.ToArray()[0].ID);
                Assert.Equal("okValue", modelList.ToArray()[1].TestValue);
                Assert.Equal(3, modelList.ToArray()[1].ID);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }

        [Collection("SQLQueryDriverTest")]
        public class TestUpsertInsertsAndUpdatesCorrectly {
            [Fact]
            public void TestUpsert() {
                SQLQueryTestUtil.SetupTestDatabase();
                TestModel insertedModel = _goodModel.Upsert();

                Assert.NotNull(insertedModel);
                Assert.Equal(1, insertedModel.ID);
                Assert.Equal("goodValue", insertedModel.TestValue);

                insertedModel.TestValue = "betterValue";
                insertedModel.Upsert();
                TestModel updatedModel = SQLQueryDriver.SelectByID<TestModel>(1);

                Assert.NotNull(updatedModel);
                Assert.Equal(1, updatedModel.ID);
                Assert.Equal("betterValue", updatedModel.TestValue);
                SQLQueryTestUtil.TeardownTestDatabase();
            }
        }
    }
}