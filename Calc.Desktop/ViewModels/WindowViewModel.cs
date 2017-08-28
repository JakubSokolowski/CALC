using Calc.Core;
using System.Windows;
using System.Windows.Input;

namespace Calc.Desktop
{
    public class WindowViewModel : BaseViewModel
    {
       
        private Window mWindow;
        private int mOuterMarginSize = 10;       
        private int mWindowRadius = 10;
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;


        public double WindowMinimumWidth { get; set; } = 550;
        public double WindowMinimumHeight { get; set; } = 400;  
        
        public int ResizeBorder { get; set; } = 6;  
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize); 

        public Thickness InnerContentPadding => new Thickness(ResizeBorder); 

        public int OuterMarginSize
        {
            get => mWindow.WindowState == WindowState.Maximized ? 0 : mOuterMarginSize;
            set => mOuterMarginSize = value;
        }       
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize); 

        public int WindowRadius
        {
            get => mWindow.WindowState == WindowState.Maximized ? 0 : mWindowRadius;           
            set => mWindowRadius = value;
        }
        public CornerRadius WindowCornerRadius => new CornerRadius(OuterMarginSize);  
        
        public int TitleHeight { get; set; } = 42;
        public GridLength TitleHeightGridLenght => new GridLength(TitleHeight + ResizeBorder);       
        

      
        public ICommand MinimizeCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }
     
        public WindowViewModel(Window window) 
        {
            mWindow = window;

            // Listen out for the window resizing
            mWindow.StateChanged += (sender, e) =>
            {
                // Fire off events for all propertiees that are affected by a resize
                OnPropertyChanged(nameof(ResizeBorderThickness));
                OnPropertyChanged(nameof(OuterMarginSize));
                OnPropertyChanged(nameof(OuterMarginSizeThickness));
                OnPropertyChanged(nameof(WindowRadius));
                OnPropertyChanged(nameof(WindowCornerRadius));         

            };

            // Create Commands

            MinimizeCommand = new RelayCommand(() => mWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => mWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => mWindow.Close());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(mWindow, GetMousePosition()));

            // Fix window resize issue

            var resizer = new WindowResizer(mWindow);

            resizer.WindowDockChanged += (dock) =>
            {
                mDockPosition = dock;

                WindowResized();
            };
        }

        private Point GetMousePosition()
        {
            var position = Mouse.GetPosition(mWindow);
            return new Point(position.X + mWindow.Left, position.Y + mWindow.Top);
        }       
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize         
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }    

    }
}
