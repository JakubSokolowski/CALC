
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Calc.Desktop
{
    public static class StoryboardHelpers
    {
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // Create the margin animate from bottom
            var slideAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 0,
                To = 1,               
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(slideAnimation);
        }
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            // Create the margin animate from bottom
            var slideAnimation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = 1,
                To = 0,
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Opacity"));
            storyboard.Children.Add(slideAnimation);
        }

        public static void AddSlideToDirectionAndKeepMargin(this Storyboard storyboard, float seconds, double offset, SlideDirection direction)
        {
            var animation = CreateSlideAnimationToDirection(seconds, offset, direction);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }
        public static void AddSlideToDirectionAndHideMargin(this Storyboard storyboard, float seconds, double offset, SlideDirection direction)
        {
            var animation = CreateSlideAnimationToDirection(seconds, offset, direction);
            animation = HideMarginAfterAnimation(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }
        public static void AddSlideFromDirectionAndKeepMargin(this Storyboard storyboard, float seconds, double offset, SlideDirection direction)
        {
            var animation = CreateSlideAnimationFromDirection(seconds, offset, direction);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }
        public static void AddSlideFromDirectionAndHideMargin(this Storyboard storyboard, float seconds, double offset, SlideDirection direction)
        {
            var animation = CreateSlideAnimationToDirection(seconds, offset, direction);
            animation = HideMarginAfterAnimation(animation);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));
            storyboard.Children.Add(animation);
        }

        private static ThicknessAnimation CreateSlideAnimationFromDirection(float seconds, double offset, SlideDirection direction)
        {            
            var animation = new ThicknessAnimation()
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = GetSlideThickness(offset, direction),
                To = new Thickness(0),
                DecelerationRatio = 0.9f
            };
            return animation;
        }
        private static ThicknessAnimation CreateSlideAnimationToDirection(float seconds, double offset, SlideDirection direction)
        {
            var animation =  CreateSlideAnimationFromDirection(seconds, offset, direction);
            return SwitchFromAndToThicknesses(animation);
        }

        private static ThicknessAnimation HideMarginAfterAnimation(ThicknessAnimation animation)
        {
            // In animations from direction, you need to hide margin in end point - To thickness
            // In animations to direction, the other way around
            if (animation.From == new Thickness(0))
                animation.To = HideMargin(animation.To.Value);
            else
                animation.From = HideMargin(animation.From.Value);

            return animation;           
        }
        private static Thickness HideMargin(Thickness thickness)
        {
            // Find which width is positive and set it to 0 
            if (thickness.Left > 0)          
                thickness.Left = 0;  
            
            if (thickness.Top > 0)           
                thickness.Top = 0;

            if (thickness.Right > 0)
                thickness.Right = 0;  
            
            if (thickness.Bottom > 0)
                thickness.Bottom = 0;
          
            return thickness;
        }       

        private static ThicknessAnimation SwitchFromAndToThicknesses(ThicknessAnimation animation)
        {
            var From = animation.From;
            animation.From = animation.To;
            animation.To = From;
            return animation;
        }

        /// <summary>
        /// Returns thickness used in animation. Animations from directions start with this thickness
        /// and animations to directions end with this thickness
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private static Thickness GetSlideThickness(double offset, SlideDirection direction)
        {
            switch(direction)
            {
                case SlideDirection.Left:
                    return new Thickness(-offset, 0, offset, 0);
                case SlideDirection.Top:
                    return new Thickness(0, -offset, 0, offset);
                case SlideDirection.Right:
                    return new Thickness(offset, 0, -offset, 0);
                case SlideDirection.Bottom:
                    return new Thickness(0, offset, 0, -offset);
                default:
                    throw new ArgumentException("Invalid Slide Direction");
            }
        }
    }    
}
