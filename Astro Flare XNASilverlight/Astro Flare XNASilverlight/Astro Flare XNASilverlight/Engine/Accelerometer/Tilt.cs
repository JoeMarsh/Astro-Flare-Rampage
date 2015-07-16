// Copyright (c) Adam Nathan.  All rights reserved.
//
// By purchasing the book that this source code belongs to, you may use and
// modify this code for commercial and non-commercial applications, but you
// may not publish the source code.
// THE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Astro_Flare_XNASilverlight
{
  // A tilting "attached behavior" for FrameworkElements
  public class Tilt : DependencyObject
  {
    // The amount of tilt, arrived at by trial & error:
    const double TILT_AMOUNT = 0.24975;

    // The amount of pressing inward, arrived at by trial & error:
    const double PRESS_AMOUNT = -40;

    const double RADIANS_TO_DEGREES = 180 / Math.PI;

    static Storyboard tiltStoryboard;
    static Storyboard untiltStoryboard;
    static DoubleAnimation depressAnimation;
    static DoubleAnimation rotationXAnimation;
    static DoubleAnimation rotationYAnimation;

    static Tilt()
    {
      tiltStoryboard = new Storyboard();
      untiltStoryboard = new Storyboard();

      depressAnimation = new DoubleAnimation { From = 0, Duration = TimeSpan.FromSeconds(.05) };
      Storyboard.SetTargetProperty(depressAnimation, new PropertyPath("(FrameworkElement.Projection).(PlaneProjection.GlobalOffsetZ)"));
      tiltStoryboard.Children.Add(depressAnimation);

      rotationXAnimation = new DoubleAnimation { From = 0, Duration = TimeSpan.FromSeconds(.05) };
      Storyboard.SetTargetProperty(rotationXAnimation, new PropertyPath("(FrameworkElement.Projection).(PlaneProjection.RotationX)"));
      tiltStoryboard.Children.Add(rotationXAnimation);

      rotationYAnimation = new DoubleAnimation { From = 0, Duration = TimeSpan.FromSeconds(.05) };
      Storyboard.SetTargetProperty(rotationYAnimation, new PropertyPath("(FrameworkElement.Projection).(PlaneProjection.RotationY)"));
      tiltStoryboard.Children.Add(rotationYAnimation);

      DoubleAnimation animation = new DoubleAnimation { To = 0, Duration = TimeSpan.FromSeconds(.05) };
      Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Projection).(PlaneProjection.GlobalOffsetZ)"));
      untiltStoryboard.Children.Add(animation);

      animation = new DoubleAnimation { To = 0, Duration = TimeSpan.FromSeconds(.05) };
      Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Projection).(PlaneProjection.RotationX)"));
      untiltStoryboard.Children.Add(animation);

      animation = new DoubleAnimation { To = 0, Duration = TimeSpan.FromSeconds(.05) };
      Storyboard.SetTargetProperty(animation, new PropertyPath("(FrameworkElement.Projection).(PlaneProjection.RotationY)"));
      untiltStoryboard.Children.Add(animation);
    }

    // The Tilt.IsEnabled dependency property:
    public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled", typeof(bool), typeof(Tilt), new PropertyMetadata(OnIsEnabledChanged));

    // Getter for making Tilt.IsEnabled an attachable property:
    public static bool GetIsEnabled(DependencyObject source)
    {
      return (bool)source.GetValue(IsEnabledProperty);
    }

    // Setter for making Tilt.IsEnabled an attachable property:
    public static void SetIsEnabled(DependencyObject source, bool value)
    {
      source.SetValue(IsEnabledProperty, value);
    }

    // If the value is changed, attach/unattach event handlers:
    static void OnIsEnabledChanged(DependencyObject target, DependencyPropertyChangedEventArgs args)
    {
      FrameworkElement element = target as FrameworkElement;
      if (element == null)
        return;

      if ((bool)args.NewValue == true)
      {
        element.AddHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown), true);
        element.AddHandler(FrameworkElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp), true);
        element.LostMouseCapture += OnLostMouseCapture;
      }
      else
      {
        element.RemoveHandler(FrameworkElement.MouseLeftButtonDownEvent, new MouseButtonEventHandler(OnMouseLeftButtonDown));
        element.RemoveHandler(FrameworkElement.MouseLeftButtonUpEvent, new MouseButtonEventHandler(OnMouseLeftButtonUp));
        element.LostMouseCapture -= OnLostMouseCapture;
      }
    }

    // Tilt on tap:
    static void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      FrameworkElement element = sender as FrameworkElement;

        // Can happen if a Click handler hides the button, for example
      if (element.Visibility == Visibility.Collapsed)
          return;

      Point point = e.GetPosition(element);

      element.CaptureMouse();

      tiltStoryboard.Stop();
      EnsureElementHasPlaneProjection(element);

      Storyboard.SetTarget(tiltStoryboard, element);
      double halfWidth = element.ActualWidth / 2;
      double halfHeight = element.ActualHeight / 2;

      double xAngle = Math.Asin((point.Y - halfHeight) / halfHeight) * RADIANS_TO_DEGREES;
      double yAngle = Math.Acos((point.X - halfWidth) / halfWidth) * RADIANS_TO_DEGREES;

      if (double.IsNaN(xAngle) || double.IsNaN(yAngle))
        return;

      depressAnimation.To = PRESS_AMOUNT;
      rotationXAnimation.To = xAngle * TILT_AMOUNT;
      rotationYAnimation.To = (yAngle - 90) * TILT_AMOUNT;

      tiltStoryboard.Begin();
    }

    // Untilt
    static void OnMouseLeftButtonUp(object sender, EventArgs e)
    {
      FrameworkElement element = sender as FrameworkElement;

      element.ReleaseMouseCapture();

      untiltStoryboard.Stop();
      EnsureElementHasPlaneProjection(element);

      Storyboard.SetTarget(untiltStoryboard, element);
      untiltStoryboard.Begin();
    }

    // Untilt
    // Happens when scrolled (we lose capture and don't get a mouseup event)
    static void OnLostMouseCapture(object sender, EventArgs e)
    {
      FrameworkElement element = sender as FrameworkElement;

      untiltStoryboard.Stop();
      EnsureElementHasPlaneProjection(element);

      Storyboard.SetTarget(untiltStoryboard, element);
      untiltStoryboard.Begin();
    }

    static void EnsureElementHasPlaneProjection(UIElement element)
    {
      if (element.Projection is PlaneProjection)
        return;

      // Create the projection.
      // NOTE: If projection were set to a MatrixProjection, it get overwritten here:
      element.Projection = new PlaneProjection();
    }
  }
}