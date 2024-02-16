using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using XamarinDemo2;

namespace UITest.Unit
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
                .ReturnsAsync(new CompletionResponse());
            subject = new OpenAiWorkoutGeneratorService(openAiApiSpy.Object);
        }

        [Test]
        public async Task GenerateWorkout_CallsOpenAiService()
        {
            await subject.GenerateWorkout(new List<string> { "legs", "arms" });

            openAiApiSpy.Verify(p => p.GetCompletion(
                It.Is<CompletionRequest>(
                    r => r.messages[1].content == "[\"legs\", \"arms\"]"
                         && r.messages[1].role == "user"
                )
            ));
        }
    }
}