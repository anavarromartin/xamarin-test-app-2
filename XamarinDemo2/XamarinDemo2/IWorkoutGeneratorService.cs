using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XamarinDemo2
{
    public interface IWorkoutGeneratorService
    {
        Task<WorkoutResponse> GenerateWorkout(List<string> focusAreas);
    }

    public class WorkoutResponse
    {
        public List<Workout> exercises { get; set; }

        public override string ToString()
        {
            var items = string.Join(", ", exercises.Select(item => item.ToString()));
            return $"Exercises: {items}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (exercises != null ? exercises.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            WorkoutResponse other = (WorkoutResponse)obj;
            return exercises.SequenceEqual(other.exercises);
        }
    }

    public class Workout
    {
        public string exercise { get; set; }
        public string reps { get; set; }
        public string description { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Workout other = (Workout)obj;
            return this.exercise == other.exercise &&
                   this.reps == other.reps &&
                   this.description == other.description;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + (exercise != null ? exercise.GetHashCode() : 0);
                hash = hash * 23 + (reps != null ? reps.GetHashCode() : 0);
                hash = hash * 23 + (description != null ? description.GetHashCode() : 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return $"Exercise: {exercise}, Reps: {reps}, Description: {description}";
        }
    }
}