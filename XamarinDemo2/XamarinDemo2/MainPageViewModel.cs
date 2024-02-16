using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinDemo2
{
    public class MainPageViewModel : BaseViewModel
    {
        public ICommand CreateWorkoutCommand { get; private set; }
        private INavigationService Navigation { get; set; }
        private IWorkoutGeneratorService WorkoutGeneratorService;

        public bool UpperBodyChecked
        {
            get => upperBodyChecked;
            set
            {
                if (upperBodyChecked == value)
                    return;

                upperBodyChecked = value;
                OnPropertyChanged(nameof(UpperBodyChecked));
            }
        }

        public bool LowerBodyChecked
        {
            get => lowerBodyChecked;
            set
            {
                if (lowerBodyChecked == value)
                    return;

                lowerBodyChecked = value;
                OnPropertyChanged(nameof(LowerBodyChecked));
            }
        }


        private bool upperBodyChecked = false;
        private bool lowerBodyChecked = false;

        public MainPageViewModel(INavigationService navigation,
            IWorkoutGeneratorService workoutGeneratorService)
        {
            CreateWorkoutCommand = new Command(async () => await CreateWorkout());
            Navigation = navigation;
            WorkoutGeneratorService = workoutGeneratorService;
        }

        private async Task CreateWorkout()
        {
            Console.WriteLine($"Upper Body: {upperBodyChecked}");
            Console.WriteLine($"Lower Body: {lowerBodyChecked}");

            List<string> selectedWorkouts = new List<string>();

            if (upperBodyChecked)
            {
                selectedWorkouts.Add("upper body");
            }

            if (lowerBodyChecked)
            {
                selectedWorkouts.Add("lower body");
            }


            await Navigation.NavigateTo<WorkoutPage>(selectedWorkouts);
        }
    }
}

