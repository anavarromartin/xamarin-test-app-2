using System.Collections.Generic;

namespace XamarinDemo2.UnitTests
{
    using Moq;

    public class MainPageViewModelTest
    {
        private Mock<INavigationService> navigationServiceSpy = new Mock<INavigationService>();
        private Mock<IWorkoutGeneratorService> workoutServiceSpy = new Mock<IWorkoutGeneratorService>();
        private MainPageViewModel _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new MainPageViewModel(
                navigationServiceSpy.Object,
                workoutServiceSpy.Object
            );
        }

        [Test]
        public void CreateWorkoutCommand_NavigatesToWorkoutPage()
        {
            _subject.UpperBodyChecked = true;
            _subject.LowerBodyChecked = true;

            _subject.CreateWorkoutCommand.Execute(null);

            navigationServiceSpy.Verify(x => x.NavigateTo<WorkoutPage>(
                It.Is<List<string>>(
                    list => list.Contains("upper body") &&
                            list.Contains("lower body"
                            ))));
        }
    }
}