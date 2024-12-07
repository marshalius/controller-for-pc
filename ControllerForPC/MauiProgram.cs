using ControllerForPC.Pages;
using ControllerForPC.Services;
using ControllerForPC.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace ControllerForPC
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //builder.Services.AddSingleton<IConnectionManager, ConnectionManager>();
            //builder.Services.AddSingleton<ITcpService, TcpService>();
            //builder.Services.AddTransient<ConnectionPage>();

            return builder.Build();
        }
    }
}
