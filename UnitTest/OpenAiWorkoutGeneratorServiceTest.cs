using Moq;
using NUnit.Framework;
using XamarinDemo2;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UnitTest
{
    public class OpenAiWorkoutGeneratorServiceTest
    {
        private OpenAiWorkoutGeneratorService subject;
        private Mock<IOpenAiApi> openAiApiSpy;

        [SetUp]
        public void Setup()
        {
            openAiApiSpy = new Mock<IOpenAiApi>();
            openAiApiSpy.Setup(p => p.GetCompletion(It.IsAny<CompletionRequest>()))
                .ReturnsAsync(new CompletionResponse()
                {
                    choices = new List<Choice>()
                    {
                        new Choice()
                        {
                            message = new Message()
                            {
                                role = "assistant",
                                content = @"
                                {
                                    ""exercises"": [
                                        {
                                            ""exercise"": ""Squats"",
                                            ""reps"": ""10-12"",
                                            ""description"": ""Pop a squat boi""
                                        } 
                                    ]
                                }
                                "
                            }
                        }
                    }
                });
            subject = new OpenAiWorkoutGeneratorService(openAiApiSpy.Object);
        }

        [Test]
        public async Task GenerateWorkout_CallsOpenAiService_AndReturnsGeneratedWorkout()
        {
            var actualWorkout = await subject.GenerateWorkout(new List<string> { "legs", "arms" });

            openAiApiSpy.Verify(p => p.GetCompletion(
                It.Is<CompletionRequest>(
                    r => r.messages[1].content == "[\"legs\", \"arms\"]"
                         && r.messages[1].role == "user"
                )
            ));

            var expectedWorkout = new WorkoutResponse()
            {
                exercises = new List<Workout>()
                {
                    new Workout()
                    {
                        exercise = "Squats",
                        reps = "10-12",
                        description = "Pop a squat boi"
                    }
                }
            };
            Assert.That(actualWorkout, Is.EqualTo(expectedWorkout));
        }
    }
}