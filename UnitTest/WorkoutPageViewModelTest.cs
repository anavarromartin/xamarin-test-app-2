using NUnit.Framework;
using XamarinDemo2;
using Moq;
using System.Collections.Generic;

namespace UnitTest
{
    public class WorkoutPageViewModelTest
    {
        private WorkoutPageViewModel _subject;
        private Mock<IWorkoutGeneratorService> _mockWorkoutGeneratorService;

        [SetUp]
        public void Setup()
        {
            _mockWorkoutGeneratorService = new Mock<IWorkoutGeneratorService>();
            _mockWorkoutGeneratorService
                .Setup(x => x.GenerateWorkout(It.IsAny<List<string>>()))
                .ReturnsAsync(new WorkoutResponse());
            
            _subject = new WorkoutPageViewModel(_mockWorkoutGeneratorService.Object);
        }


        [Test]
        public void SetsIsLoadingToFalse_WhenGenerateWorkoutsReturns()
        {
            Assert.That(_subject.IsLoading, Is.True);
            
            _subject.SelectedWorkouts = new List<string>() { "upper body", "lower body" };
            
            _mockWorkoutGeneratorService
                .Verify(x => x.GenerateWorkout(It.IsAny<List<string>>()), Times.Once);
            
            Assert.That(_subject.IsLoading, Is.False);
        }
    }
}