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
using Windows.ApplicationModel.Core;
using Windows.Media.Protection;
using Windows.UI.Core;
using static WinUI3PackageProjectTemplate.ViewModel.RelayCommand;

namespace WinUI3PackageProjectTemplate.ViewModel
{
    public class MainPageViewModel : ViewModelBase
    {
        public RelayCommand MyCommand { get; private set; }

        private string _myText = "初期値";
        public string MyText
        {
            get { return _myText; }
            set { _myText = value; RaisePropertyChanged(nameof(MyText)); }
        }

        public MainPageViewModel()
        {
            MyCommand = new RelayCommand(()=>
            {
                MyText = "コマンド実行";
            });
        }
    }

    // 以下は「https://github.com/microsoft/Windows-universal-samples/blob/ad9a0c4def222aaf044e51f8ee0939911cb58471/Samples/PlayReady/cs/Shared/RelayCommand.cs#L28」より。

    /// <summary>
    /// A command whose sole purpose is to relay its functionality
    /// to other objects by invoking delegates.
    /// The default return value for the CanExecute method is 'true'.
    /// RaiseCanExecuteChanged needs to be called whenever
    /// CanExecute is expected to return a different value.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;
        /// <summary>
        /// Raised when RaiseCanExecuteChanged is called.
        /// </summary>
        public event EventHandler CanExecuteChanged;
        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }
        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }
        /// <summary>
        /// Determines whether this RelayCommand can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }
        /// <summary>
        /// Executes the RelayCommand on the current command target.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command. If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        public void Execute(object parameter)
        {
            _execute();
        }
        /// <summary>
        /// Method used to raise the CanExecuteChanged event
        /// to indicate that the return value of the CanExecute
        /// method has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }


        // 以下は「https://github.com/microsoft/Windows-universal-samples/blob/ad9a0c4def222aaf044e51f8ee0939911cb58471/Samples/PlayReady/cs/ViewModels/ViewModelBase.cs#L23」より。

        public class ViewModelBase : INotifyPropertyChanged
        {

            public event PropertyChangedEventHandler PropertyChanged;
            /// <summary>
            /// Fires an event when called. Used to update the UI in the MVVM world.
            /// [CallerMemberName] Ensures only the peoperty that calls it gets the event
            /// and not every property
            /// </summary>
            protected void RaisePropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public async static void Log(string message)
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    outputLog.Insert(0, message);
                });
            }

            public async void SetPlaybackEnabled(bool enabled)
            {
                var dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PlaybackEnabled = enabled;
                });
            }

            /// <summary>
            /// Provides media and drm bindable logging for ViewModels. 
            /// </summary>
            private static ObservableCollection<string> outputLog = new ObservableCollection<string>();
            public ObservableCollection<string> OutputLog
            {
                get { return outputLog; }
                private set { outputLog = value; }
            }

            /// <summary>
            /// A ProtectionManager is assigned to a MediaElement(or MediaPlayer) instance to
            /// provide two way communication between the player and PlayReady DRM. 
            /// </summary>
            private MediaProtectionManager protectionManager;
            public MediaProtectionManager ProtectionManager
            {
                get
                {
                    return protectionManager;
                }
                protected set
                {
                    if (protectionManager != value)
                    {
                        protectionManager = value;
                        RaisePropertyChanged();
                    }
                }
            }

            /// <summary>
            /// 
            /// </summary>
            private bool playbackEnabled = false;
            public bool PlaybackEnabled
            {
                get
                {
                    return playbackEnabled;
                }
                protected set
                {
                    if (playbackEnabled != value)
                    {
                        playbackEnabled = value;
                        RaisePropertyChanged();
                    }
                }
            }

        }
    }
}
