using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Enums {
    public enum Specialties {
        [Display(Name = "General Fitness")]
        GeneralFitness = 1,

        [Display(Name = "Weight Loss & Fat Burning")]
        WeightLoss = 2,

        [Display(Name = "Muscle Building & Bodybuilding")]
        Bodybuilding = 3,

        [Display(Name = "Strength & Powerlifting")]
        Powerlifting = 4,

        [Display(Name = "Cardio & Endurance")]
        Cardio = 5,

        [Display(Name = "CrossFit & Functional Training")]
        CrossFit = 6,

        [Display(Name = "Rehabilitation & Injury Recovery")]
        Rehabilitation = 7,

        [Display(Name = "Sports Performance & Athletics")]
        SportsPerformance = 8,

        [Display(Name = "Nutrition & Diet Coaching")]
        NutritionCoaching = 9,

        [Display(Name = "Kids & Youth Fitness")]
        YouthFitness = 10,

        [Display(Name = "Seniors & Elderly Fitness")]
        SeniorsFitness = 11,
    }
}
