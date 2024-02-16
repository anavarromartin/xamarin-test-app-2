using System;
using System.Collections.Generic;

namespace XamarinDemo2
{
    public class WorkoutPageViewModel : BaseViewModel
    {
        public List<string> SelectedWorkouts
        {
            get => _selectedWorkouts;
            set
            {
                if (value == SelectedWorkouts)
                    return;
                _selectedWorkouts = value;
                OnPropertyChanged(nameof(SelectedWorkouts));
                GenerateWorkouts();
            }
        }

        public List<Workout> Workouts
        {
            get => _workouts;
            set
            {
                if (value == Workouts)
                    return;
                _workouts = value;
                OnPropertyChanged(nameof(Workouts));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (value == IsLoading)
                    return;
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
                OnPropertyChanged(nameof(IsContentVisible));
            }
        }

        public bool IsContentVisible => !_isLoading;

        private List<string> _selectedWorkouts;
        private readonly IWorkoutGeneratorService _workoutGeneratorService;
        private List<Workout> _workouts;
        private bool _isLoading = true;

        public WorkoutPageViewModel(IWorkoutGeneratorService workoutGeneratorService)
        {
            _workoutGeneratorService = workoutGeneratorService;
        }

        private async void GenerateWorkouts()
        {
            IsLoading = true;
            Workouts = (await _workoutGeneratorService.GenerateWorkout(_selectedWorkouts)).exercises;
            IsLoading = false;
        }
    }
}