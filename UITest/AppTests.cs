using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class AppTests
    {
        IApp app;
        Platform platform;

        public AppTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            AppResult[] results = app.WaitForElement(c => c.Marked("Start developing now"));
            app.Screenshot("Welcome screen.");
            app.Tap(q => q.Marked("UpperBodyCheck"));
            app.Tap(q => q.Marked("CreateWorkout"));

            Assert.IsTrue(results.Any());
        }
    }
}