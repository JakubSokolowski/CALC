
using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Calc.Desktop
{
    public static class StoryboardHelpers
    {
        public static void AddSlideFromBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from bottom
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0, offset - 50, 0, -offset + 50),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio,
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }
        public static void AddSlideToBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from bottom
            var slideAnimation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(0, offset , 0, -offset),
                DecelerationRatio = decelerationRatio,
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
            storyboard.Children.Add(slideAnimation);
        }

        public static void AddSlideFromLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }       
        public static void AddSlideToLeft(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(-offset, 0, keepMargin ? offset : 0, 0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

        public static void AddSlideFromRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                To = new Thickness(0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }       
        public static void AddSlideToRight(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f, bool keepMargin = true)
        {
            // Create the margin animate from right 
            var animation = new ThicknessAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(seconds)),
                From = new Thickness(0),
                To = new Thickness(keepMargin ? offset : 0, 0, -offset, 0),
                DecelerationRatio = decelerationRatio
            };

            // Set the target property name
            Storyboard.SetTargetProperty(animation, new PropertyPath("Margin"));

            // Add this to the storyboard
            storyboard.Children.Add(animation);
        }

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
    }
}
