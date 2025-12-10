using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BPCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BPCalculatorTests
{
    [TestClass]
    public class BloodPressureTests
    {


        // Category: High 

        // High due to systolic >= 140 
        [TestMethod]
        public void Category_High_When_Systolic_At_Boundary_140()
        {
            var bp = new BloodPressure
            {
                Systolic = 140,   // boundary
                Diastolic = 70
            };

            Assert.AreEqual(BPCategory.High, bp.Category);
        }

        // High due to diastolic >= 90 
        [TestMethod]
        public void Category_High_When_Diastolic_At_Boundary_90()
        {
            var bp = new BloodPressure
            {
                Systolic = 130,
                Diastolic = 90    // boundary
            };

            Assert.AreEqual(BPCategory.High, bp.Category);
        }

        //  Category: PreHigh 

        // PreHigh because systolic in [120,139] and diastolic < 80
        [TestMethod]
        public void Category_PreHigh_When_Systolic_PreHigh_Range()
        {
            var bp = new BloodPressure
            {
                Systolic = 120,   // lower boundary of pre-high
                Diastolic = 70
            };

            Assert.AreEqual(BPCategory.PreHigh, bp.Category);
        }

        // PreHigh because diastolic in [80,89] and systolic < 120
        [TestMethod]
        public void Category_PreHigh_When_Diastolic_PreHigh_Range()
        {
            var bp = new BloodPressure
            {
                Systolic = 110,
                Diastolic = 80    // lower boundary of pre-high
            };

            Assert.AreEqual(BPCategory.PreHigh, bp.Category);
        }

        // Category: Ideal

        // Ideal when both systolic and diastolic are in ideal range
        [TestMethod]
        public void Category_Ideal_When_Within_Ideal_Range()
        {
            var bp = new BloodPressure
            {
                Systolic = 100,   // between 90 and 119
                Diastolic = 70    // between 60 and 79
            };

            Assert.AreEqual(BPCategory.Ideal, bp.Category);
        }

        // Another ideal case at boundaries
        [TestMethod]
        public void Category_Ideal_When_At_Ideal_Boundaries()
        {
            var bp = new BloodPressure
            {
                Systolic = 90,    // lower boundary
                Diastolic = 60    // lower boundary
            };

            Assert.AreEqual(BPCategory.Ideal, bp.Category);
        }

        // Category: Low 

        // Low when below ideal ranges but still within allowed minima
        [TestMethod]
        public void Category_Low_When_Below_Ideal()
        {
            var bp = new BloodPressure
            {
                Systolic = 85,     // below ideal systolic
                Diastolic = 55     // below ideal diastolic
            };

            Assert.AreEqual(BPCategory.Low, bp.Category);
        }

        // Another low case with only systolic low
        [TestMethod]
        public void Category_Low_When_Only_Systolic_Low()
        {
            var bp = new BloodPressure
            {
                Systolic = 80,    // low
                Diastolic = 65    // ideal range
            };

            Assert.AreEqual(BPCategory.Low, bp.Category);
        }

        //  Validation attribute tests (Range on Systolic/Diastolic) 

        private static bool TryValidate(BloodPressure bp, out List<ValidationResult> results)
        {
            var context = new ValidationContext(bp);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(bp, context, results, validateAllProperties: true);
        }

        [TestMethod]
        public void Validation_Succeeds_For_Valid_Values()
        {
            var bp = new BloodPressure
            {
                Systolic = BloodPressure.SystolicMin,
                Diastolic = BloodPressure.DiastolicMin
            };

            Assert.IsTrue(TryValidate(bp, out var results));
            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void Validation_Fails_When_Systolic_Below_Min()
        {
            var bp = new BloodPressure
            {
                Systolic = BloodPressure.SystolicMin - 1,
                Diastolic = 60
            };

            Assert.IsFalse(TryValidate(bp, out var results));
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Invalid Systolic Value"));
        }

        [TestMethod]
        public void Validation_Fails_When_Systolic_Above_Max()
        {
            var bp = new BloodPressure
            {
                Systolic = BloodPressure.SystolicMax + 1,
                Diastolic = 60
            };

            Assert.IsFalse(TryValidate(bp, out var results));
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Invalid Systolic Value"));
        }

        [TestMethod]
        public void Validation_Fails_When_Diastolic_Below_Min()
        {
            var bp = new BloodPressure
            {
                Systolic = 100,
                Diastolic = BloodPressure.DiastolicMin - 1
            };

            Assert.IsFalse(TryValidate(bp, out var results));
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Invalid Diastolic Value"));
        }

        [TestMethod]
        public void Validation_Fails_When_Diastolic_Above_Max()
        {
            var bp = new BloodPressure
            {
                Systolic = 100,
                Diastolic = BloodPressure.DiastolicMax + 1
            };

            Assert.IsFalse(TryValidate(bp, out var results));
            Assert.IsTrue(results.Any(r => r.ErrorMessage == "Invalid Diastolic Value"));
        }
    }
}
