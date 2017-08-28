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
        
        }
    }

}
