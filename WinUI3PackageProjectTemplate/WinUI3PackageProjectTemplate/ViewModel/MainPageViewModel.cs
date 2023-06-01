using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using Windows.ApplicationModel.Core;
using Windows.Media.Protection;
using Windows.UI.Core;

namespace WinUI3PackageProjectTemplate.ViewModel
{
    public class MainPageViewModel : ObservableObject
    {
        public RelayCommand MyCommand { get; private set; }

        private string _myText = "初期値";
        public string MyText
        {
            get => _myText;
            set => SetProperty(ref _myText, value);
        }

        public Dictionary<string, ToggleSwitch> dic = new()
        {
            { "a", new ToggleSwitch(){ OnContent = "おん", OffContent = "おふ", Style = (Style)Application.Current.Resources["MyToggleSwitchStyle"]}},


            { "b", new ToggleSwitch(){ OnContent = "おん", OffContent = "おふ", Style = (Style)Application.Current.Resources["MyToggleSwitchStyle"]}},         };


        public List<ToggleSwitch> MyData { get; private set; } = new()
        {
            new ToggleSwitch(){ OnContent = "おん", OffContent = "おふ", Style = (Style)Application.Current.Resources["MyToggleSwitchStyle"]},//DefaultToggleSwitchStyle
            new ToggleSwitch(){ OnContent = "おん1", OffContent = "おふ1" },
            new ToggleSwitch(){ OnContent = "おん2", OffContent = "おふ2" },
        };

        public MainPageViewModel()
        {
            var a = dic["a"];
            MyCommand = new RelayCommand(()=>
            {
                MyText = "コマンド実行";
            });
        }
    }
}
