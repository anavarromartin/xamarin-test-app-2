using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinDemo2
{
	public partial class WorkoutPage : ContentPage
	{	
		public WorkoutPage (List<string> selectedWorkouts)
		{
			InitializeComponent();

			var viewModel = (WorkoutPageViewModel) App.GetViewModel<WorkoutPageViewModel>();
			viewModel.SelectedWorkouts = selectedWorkouts;

			BindingContext = viewModel;
		}
	}
}

