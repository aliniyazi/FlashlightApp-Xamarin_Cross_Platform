using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace FlashlightApp
{
    public partial class MainPage : ContentPage
    {
         int timeDiffrence = 0;

        public MainPage()
        {
            InitializeComponent();
            DateTime datetime = DateTime.Now;
            TimeSpan time = new TimeSpan(datetime.Hour, datetime.Minute, datetime.Second);
            _timePicker.Time = time;
       
        }


        public async void FlashlightOn(object sender, EventArgs e)
        {
            var timeSpan = _timePicker.Time;
            timeDiffrence = CalculateTimeDiffrence(timeSpan.Hours, timeSpan.Minutes);
            try
            {
                // Turn On Flashlight  
                await Flashlight.TurnOnAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await ShowAlert(fnsEx.Message);
            }
            catch (PermissionException pEx)
            {
                await ShowAlert(pEx.Message);
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
            remaningTime.Text = (timeDiffrence/60).ToString();
            Task.Delay(new TimeSpan(0, 0, timeDiffrence)).ContinueWith(o => { TurnOffFlashlightAfterSomeTime(); });
        }

        private async void ForceFlashlightOff(object sender, EventArgs e)
        {
            try
            {
                // Turn Off Flashlight  
                await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await ShowAlert(fnsEx.Message);
            }
            catch (PermissionException pEx)
            {
                await ShowAlert(pEx.Message);
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
        }
        public async Task ShowAlert(string message)
        {
            await DisplayAlert("Faild", message, "Ok");
        }
        private int CalculateTimeDiffrence(int hours, int minutes)
        {
            DateTime currentTime = DateTime.Now;
            var hoursDiffrence = Math.Abs(hours - currentTime.Hour);
            var minnutesDiffrence = Math.Abs(minutes - currentTime.Minute);

            var totalDiffrence = hoursDiffrence * 3600 + minnutesDiffrence * 60;
            return totalDiffrence;
        }
        private async void TurnOffFlashlightAfterSomeTime()
        {
            try
            {
                // Turn Off Flashlight  
                await Flashlight.TurnOffAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await ShowAlert(fnsEx.Message);
            }
            catch (PermissionException pEx)
            {
                await ShowAlert(pEx.Message);
            }
            catch (Exception ex)
            {
                await ShowAlert(ex.Message);
            }
        }

    }
}
