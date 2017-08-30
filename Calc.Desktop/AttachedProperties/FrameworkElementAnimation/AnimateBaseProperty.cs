
using System.Windows;

namespace Calc.Desktop
{

    /// <summary>
    /// Runs any animation method when a boolan is set to true and a reverse animation when set to false
    /// </summary>
    /// <typeparam name="Parent"></typeparam>
    public abstract class AnimateBaseProperty<Parent> : BaseAttachedProperty<Parent, bool>
        where Parent: BaseAttachedProperty<Parent, bool>, new()
    {
        public bool FirstLoad { get; set; } = true;

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            if (!(sender is FrameworkElement element))
                return;
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            if (FirstLoad)
            {
                // Create a single self-unhookable event
                // for the elements loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // Unhook ourselves
                    element.Loaded -= onLoaded;
                    Animate(element, (bool)value);
                    // No longer first load
                    FirstLoad = false;
                };
                // Hook into the loaded event
                element.Loaded += onLoaded;               
            }
            else
                Animate(element, (bool)value);
        }

        protected virtual void Animate(FrameworkElement element, bool value) { }
    }
}
