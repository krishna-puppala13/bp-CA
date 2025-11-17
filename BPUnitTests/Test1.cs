using BPCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        // High due to diastolic >= 90 (even if systolic not high)
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

        // Low when below ideal ranges (but still within allowed min values)
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
        
               
    }
}
