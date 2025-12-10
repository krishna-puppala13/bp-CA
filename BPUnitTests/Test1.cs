using BPCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BPUnitTests
{
    [TestClass]
    public class BloodPressureTests
    {
        // High due to systolic >= 140
        [TestMethod]
        public void Category_High_When_Systolic_High()
        {
            var bp = new BloodPressure
            {
                Systolic = 150,
                Diastolic = 70
            };

            Assert.AreEqual(BPCategory.High, bp.Category);
        }

        // High due to diastolic >= 90 
        [TestMethod]
        public void Category_High_When_Diastolic_High()
        {
            var bp = new BloodPressure
            {
                Systolic = 130,
                Diastolic = 95
            };

            Assert.AreEqual(BPCategory.High, bp.Category);
        }

        // PreHigh due to systolic in [120,139] and diastolic < 80
        [TestMethod]
        public void Category_PreHigh_When_Systolic_PreHigh()
        {
            var bp = new BloodPressure
            {
                Systolic = 125,
                Diastolic = 70
            };

            Assert.AreEqual(BPCategory.PreHigh, bp.Category);
        }

        // PreHigh due to diastolic in [80,89] and systolic < 120
        [TestMethod]
        public void Category_PreHigh_When_Diastolic_PreHigh()
        {
            var bp = new BloodPressure
            {
                Systolic = 110,
                Diastolic = 85
            };

            Assert.AreEqual(BPCategory.PreHigh, bp.Category);
        }

        // Ideal when systolic in [90,119] AND diastolic in [60,79]
        [TestMethod]
        public void Category_Ideal_When_Within_Ideal_Range()
        {
            var bp = new BloodPressure
            {
                Systolic = 110,
                Diastolic = 70
            };

            Assert.AreEqual(BPCategory.Ideal, bp.Category);
        }

        // Low when below ideal ranges (both systolic and diastolic below ideal)
        [TestMethod]
        public void Category_Low_When_Below_Ideal()
        {
            var bp = new BloodPressure
            {
                Systolic = 85,   // below ideal systolic (90)
                Diastolic = 55   // below ideal diastolic (60)
            };

            Assert.AreEqual(BPCategory.Low, bp.Category);
        }

        // Low when systolic < 90 but diastolic is in the normal (ideal) diastolic range
        [TestMethod]
        public void Category_Low_When_Systolic_Low_But_Diastolic_Normal()
        {
            var bp = new BloodPressure
            {
                Systolic = 85,   // below 90
                Diastolic = 75   // inside 60–79
            };

            Assert.AreEqual(BPCategory.Low, bp.Category);
        }

        // -------- AdviceMessage tests --------

        // for Low category
        [TestMethod]
        public void AdviceMessage_For_Low_Category()
        {
            var bp = new BloodPressure
            {
                Systolic = 85,
                Diastolic = 55
            };

            Assert.AreEqual(
                "Your blood pressure is low. Increase fluids and seek medical advice if symptoms occur.",
                bp.AdviceMessage);
        }

        // for Ideal category
        [TestMethod]
        public void AdviceMessage_For_Ideal_Category()
        {
            var bp = new BloodPressure
            {
                Systolic = 110,
                Diastolic = 70
            };

            Assert.AreEqual(
                "Your blood pressure is ideal. Maintain a healthy lifestyle!",
                bp.AdviceMessage);
        }

        //for PreHigh category
        [TestMethod]
        public void AdviceMessage_For_PreHigh_Category()
        {
            var bp = new BloodPressure
            {
                Systolic = 125,
                Diastolic = 70
            };

            Assert.AreEqual(
                "Your reading is slightly elevated. Consider reducing salt intake and managing stress.",
                bp.AdviceMessage);
        }

        //for High category
        [TestMethod]
        public void AdviceMessage_For_High_Category()
        {
            var bp = new BloodPressure
            {
                Systolic = 150,
                Diastolic = 95
            };

            Assert.AreEqual(
                "Your blood pressure is high. Please consult a healthcare professional.",
                bp.AdviceMessage);
        }

        // VALIDATION: fails when Systolic <= Diastolic (custom IValidatableObject rule)
        [TestMethod]
        public void Validation_Fails_When_Systolic_Less_Or_Equal_Diastolic()
        {
            var bp = new BloodPressure
            {
                Systolic = 90,
                Diastolic = 100
            };

            var context = new ValidationContext(bp);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(bp, context, results, true);

            Assert.IsFalse(isValid, "Expected validation to fail when Systolic <= Diastolic.");
            Assert.IsTrue(results.Exists(r =>
                r.ErrorMessage.Contains("Systolic value must be greater than Diastolic value")),
                "Expected custom validation message for systolic/diastolic rule.");
        }
    }
}
