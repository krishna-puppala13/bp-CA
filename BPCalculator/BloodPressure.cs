using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace BPCalculator
{
    // BP categories
    public enum BPCategory
    {
        [Display(Name = "Low Blood Pressure")] Low,
        [Display(Name = "Ideal Blood Pressure")] Ideal,
        [Display(Name = "Pre-High Blood Pressure")] PreHigh,
        [Display(Name = "High Blood Pressure")] High
    };

    public class BloodPressure : IValidatableObject
    {
        public const int SystolicMin = 70;
        public const int SystolicMax = 190;
        public const int DiastolicMin = 40;
        public const int DiastolicMax = 100;

        [Range(SystolicMin, SystolicMax, ErrorMessage = "Invalid Systolic Value")]
        public int Systolic { get; set; }                       // mmHG

        [Range(DiastolicMin, DiastolicMax, ErrorMessage = "Invalid Diastolic Value")]
        public int Diastolic { get; set; }                      // mmHG

        // validation: systolic must be greater than diastolic
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Systolic <= Diastolic)
            {
                yield return new ValidationResult(
                    "Systolic value must be greater than Diastolic value",
                     Array.Empty<string>()
                );
            }
        }

        // calculate BP category
        public BPCategory Category
        {
            get
            {
                

                if (Systolic >= 140 || Diastolic >= 90)
                    return BPCategory.High;

                if ((Systolic >= 120 && Systolic <= 139) ||
                    (Diastolic >= 80 && Diastolic <= 89))
                    return BPCategory.PreHigh;

                if (Systolic >= 90 && Systolic <= 119 &&
                    Diastolic >= 60 && Diastolic <= 79)
                    return BPCategory.Ideal;

                return BPCategory.Low;
            }
        }
        public string AdviceMessage
        {
            get
            {
                switch (Category)
                {
                    case BPCategory.Low:
                        return "Your blood pressure is low. Increase fluids and seek medical advice if symptoms occur.";
                    case BPCategory.Ideal:
                        return "Your blood pressure is ideal. Maintain a healthy lifestyle!";
                    case BPCategory.PreHigh:
                        return "Your reading is slightly elevated. Consider reducing salt intake and managing stress.";
                    case BPCategory.High:
                        return "Your blood pressure is high. Please consult a healthcare professional.";
                }
                return string.Empty;
            }
        }
    }
}