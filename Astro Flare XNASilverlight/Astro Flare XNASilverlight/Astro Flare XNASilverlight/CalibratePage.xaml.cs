// Copyright (c) Adam Nathan.  All rights reserved.
//
// By purchasing the book that this source code belongs to, you may use and
// modify this code for commercial and non-commercial applications, but you
// may not publish the source code.
// THE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Applications.Common; // For AccelerometerHelper
using Microsoft.Phone.Controls;

namespace Astro_Flare_XNASilverlight
{
  public partial class CalibratePage : PhoneApplicationPage
  {
    bool calibrateX = true;
    bool calibrateY = true;

    public CalibratePage()
    {
        InitializeComponent();

      // Use the accelerometer via Microsoft's helper
      AccelerometerHelper.Instance.ReadingChanged += Accelerometer_ReadingChanged;
      // Ensure it is active
      AccelerometerHelper.Instance.Active = true;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);

       //Set the application name in the header
      //if (this.NavigationContext.QueryString.ContainsKey("appName"))
      //{
      //  this.ApplicationName.Text =
      //    this.NavigationContext.QueryString["appName"].ToUpperInvariant();
      //}

       //Check for calibration parameters
      if (this.NavigationContext.QueryString.ContainsKey("calibrateX"))
      {
        this.calibrateX =
          bool.Parse(this.NavigationContext.QueryString["calibrateX"]);
      }
      if (this.NavigationContext.QueryString.ContainsKey("calibrateY"))
      {
        this.calibrateY =
          bool.Parse(this.NavigationContext.QueryString["calibrateY"]);
      }
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {        
        base.OnNavigatedFrom(e);
        AccelerometerHelper.Instance.Active = false;
    }

    // Process data coming from the accelerometer
    void Accelerometer_ReadingChanged(object sender,
                                      AccelerometerHelperReadingEventArgs e)
    {
      this.Dispatcher.BeginInvoke(delegate()
      {
        bool canCalibrateX = this.calibrateX && 
          AccelerometerHelper.Instance.CanCalibrate(this.calibrateX, false);
        bool canCalibrateY = this.calibrateY && 
          AccelerometerHelper.Instance.CanCalibrate(false, this.calibrateY);

        // Update the enabled state and text of the calibration button
        this.CalibrateButton.IsEnabled = canCalibrateX || canCalibrateY;

        if (canCalibrateX && canCalibrateY)
          this.CalibrateButton.Content = "calibrate (flat)";
        else if (canCalibrateX)
          this.CalibrateButton.Content = "calibrate (portrait)";
        else if (canCalibrateY)
          this.CalibrateButton.Content = "calibrate (landscape)";
        else
          this.CalibrateButton.Content = "calibrate";

        this.WarningText.Visibility = this.CalibrateButton.IsEnabled ? 
          Visibility.Collapsed : Visibility.Visible;
      });
    }

    void CalibrateButton_Click(object sender, RoutedEventArgs e)
    {
      if (AccelerometerHelper.Instance.Calibrate(this.calibrateX,
                                                 this.calibrateY) ||
          AccelerometerHelper.Instance.Calibrate(this.calibrateX, false) ||
          AccelerometerHelper.Instance.Calibrate(false, this.calibrateY))
      {
        // Consider it a success if we were able to
        // calibrate in either direction (or both)
        if (this.NavigationService.CanGoBack)
          this.NavigationService.GoBack();
      }
      else
      {
        MessageBox.Show("Unable to calibrate. Make sure you're holding your " +
          "phone still, even when tapping the button!", "Calibration Error",
          MessageBoxButton.OK);
      }
    }
  }
}