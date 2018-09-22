using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using UniversityAPI.Helpers;

namespace UniwersityAPI.UnitTest
{
    [TestClass]
    public class ReflectionHelperTest
    {
        class FilterTestObject
        {
            public string Field1 { get; set; }
            public string Field2 { get; set; }
            public string Field3 { get; set; }

            public object ObjectInstance { get; set; } = null;
        }

        [TestMethod]
        public void PerformFilterOverString_CaseInsensitive()
        {
            List<FilterTestObject> testData = new List<FilterTestObject>();
            testData.Add(new FilterTestObject() { Field1 = "FieldValue", Field2 = "1_Field2", Field3 = "1_Field3" });
            testData.Add(new FilterTestObject() { Field1 = "FieldValue_", Field2 = "2_Field2", Field3 = "2_Field3" });
            testData.Add(new FilterTestObject() { Field1 = "_FieldValue__", Field2 = "3_Field2", Field3 = "3_Field3" });
            testData.Add(new FilterTestObject() { Field1 = "__FieldValue___", Field2 = "4_Field2", Field3 = "4_Field3" });

            var result = ReflectionHelper.PerformFilterOverString<FilterTestObject>("Field1", "fieldvalue", testData.AsQueryable(), false).ToList();
            Assert.AreEqual(result.Count, 4, "Case insensitive does not work.");

            result = ReflectionHelper.PerformFilterOverString<FilterTestObject>("Field1", "fieldvalue", testData.AsQueryable(), true).ToList();
            Assert.AreEqual(result.Count, 0, "Case sensitive does not work.");

            result = ReflectionHelper.PerformFilterOverString<FilterTestObject>("Field1", "FieldValue", testData.AsQueryable(), true).ToList();
            Assert.AreEqual(result.Count, 4, "Case sensitive does not work.");
        }

        [TestMethod]
        public void CheckIfObjectHasPropertyContaingValue_ContainsPropertyWithValue()
        {
            var testObj = new FilterTestObject() { Field1 = "1_Field1", Field2 = "1_Field2", Field3 = "1_Field3" };

            bool res = ReflectionHelper.CheckIfObjectHasPropertyContaingValue(testObj, "Field2", "1_Field2");
            Assert.IsTrue(res, "Object should has property with exact value.");

            res = ReflectionHelper.CheckIfObjectHasPropertyContaingValue(testObj, "Field2", "Field");
            Assert.IsTrue(res, "Object should has property with containsng value.");

            res = ReflectionHelper.CheckIfObjectHasPropertyContaingValue(testObj, "Field2", "test");
            Assert.IsFalse(res, "Object should not has property with containsng value.");

            res = ReflectionHelper.CheckIfObjectHasPropertyContaingValue(testObj, "UnknownField", "test");
            Assert.IsFalse(res, "Object should not has property with name UnknownField.");

            res = ReflectionHelper.CheckIfObjectHasPropertyContaingValue(testObj, "ObjectInstance", "test");
            Assert.IsFalse(res, "Property ObjectInstance is not string. Should not be avaluated.");
        }
    }
}
