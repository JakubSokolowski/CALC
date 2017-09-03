using System;
using Ninject;

namespace Calc.Core
{
    public static class IoC
    {
        public static IKernel Kernel { get; private set; } = new StandardKernel();


        // Binds all the information required
        public static void Setup()
        {
            BindViewModels();
        }

        // Bind all Singleton viewmodels
        private static void BindViewModels()
        {
            Kernel.Bind<ApplicationViewModel>().ToConstant(new ApplicationViewModel());
            Kernel.Bind<WelcomeViewModel>().ToConstant(new WelcomeViewModel());
            Kernel.Bind<BaseConverterViewModel>().ToConstant(new BaseConverterViewModel());
            Kernel.Bind<FloatRepresentationViewModel>().ToConstant(new FloatRepresentationViewModel());
        }

        private static void BindPages()
        {
        
        }
       



        // Get's a service from the IoC
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }

}
