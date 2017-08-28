using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Desktop
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
