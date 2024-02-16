using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Xamarin.Forms;

namespace XamarinDemo2
{
    public partial class App : Application
    {
        protected static IServiceProvider ServiceProvider { get; set; }

        public App()
        {
            InitializeComponent();

            SetupServices();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        void SetupServices()
        {
            var services = new ServiceCollection();

            // Add View Models here
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<WorkoutPageViewModel>();

            // Add core services here
            services
                .AddSingleton<IWorkoutGeneratorService,
                OpenAiWorkoutGeneratorService>();
            services.AddSingleton<INavigationService, NavigationService>();

            var openAiApi = RestService.For<IOpenAiApi>("https://api.openai.com/v1", new RefitSettings
            {
                AuthorizationHeaderValueGetter = (rq, ct) => Task.FromResult(Helpers.Secrets.openApiToken)
            });
            var sampleApi = RestService.For<ISampleApis>("https://api.sampleapis.com");
            services.AddSingleton(sampleApi);
            services.AddSingleton(openAiApi);

            ServiceProvider = services.BuildServiceProvider();
        }

        public static BaseViewModel GetViewModel<TViewModel>()
            where TViewModel : BaseViewModel
            => ServiceProvider.GetService<TViewModel>();
    }
}

