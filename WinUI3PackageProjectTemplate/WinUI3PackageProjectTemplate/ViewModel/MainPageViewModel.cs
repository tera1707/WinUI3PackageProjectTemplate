using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public MainPageViewModel()
        {
            MyCommand = new RelayCommand(()=>
            {
                MyText = "コマンド実行";
            });
        }
    }
}
