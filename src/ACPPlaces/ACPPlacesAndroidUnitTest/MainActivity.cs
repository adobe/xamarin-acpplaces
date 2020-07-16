using System.Reflection;

using Android.App;
using Android.OS;
using Xamarin.Android.NUnitLite;
using Com.Adobe.Marketing.Mobile;
using System.Threading;

namespace ACPPlacesAndroidUnitTest
{
    [Activity(Label = "ACPPlacesAndroidUnitTest", MainLauncher = true)]
    public class MainActivity : TestSuiteActivity
    {
        static CountdownEvent latch = new CountdownEvent(1);
        protected override void OnCreate(Bundle bundle)
        {
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);

            // setup for all tests
            ACPCore.Application = this.Application;
            ACPCore.SetWrapperType(WrapperType.Xamarin);
            ACPCore.LogLevel = LoggingMode.Verbose;
            ACPIdentity.RegisterExtension();
            ACPSignal.RegisterExtension();
            ACPLifecycle.RegisterExtension();
            ACPPlaces.RegisterExtension();

            // start core
            ACPCore.Start(new CoreStartCompletionCallback());
            latch.Wait();
            latch.Dispose();
        }

        class CoreStartCompletionCallback : Java.Lang.Object, IAdobeCallback
        {
            public void Call(Java.Lang.Object callback)
            {
                ACPCore.ConfigureWithAppID("94f571f308d5/00fc543a60e1/launch-c861fab912f7-development");
                latch.Signal();
            }
        }
    }
}
