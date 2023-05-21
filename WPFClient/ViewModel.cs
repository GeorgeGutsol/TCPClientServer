using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFCLient.Commands;
using System.Net;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Collections.ObjectModel;


namespace WPFClient
{
    public class ViewModel : INotifyPropertyChanged
    {
        public Client Client { get; }

        private SolidColorBrush _connectButtonColor = new SolidColorBrush(Colors.Green);

        public SolidColorBrush ConnectButtonColor
        {
            get { return _connectButtonColor; }
        }

        private string _connectButtonText="Подключиться";
        public string ConnectButtonText
        {
            get { return _connectButtonText; }
            set 
            { 
                _connectButtonText = value;
                OnPropertyChanged();
            }
        }

        public ViewModel()
        {
            Client = new Client();
        }

        private RelayCommand _connectCommand;

        public RelayCommand ConnectCommand
        {
            get
            {
                return _connectCommand ?? (_connectCommand = new RelayCommand
                    (
                        () =>
                        {
                            Client.IsConnected = !Client.IsConnected;
                            ConnectButtonColor.Color =Client.IsConnected?Colors.Red:Colors.Green;
                            ConnectButtonText = Client.IsConnected ?  "Отключиться": "Подключиться";
                        },
                        () =>
                        {
                            if (Client == null) return false;
                            if (!IPAddress.TryParse(Client.IP, out IPAddress a)) return false;
                            if (!int.TryParse(Client.Port, out int p)) return false;
                            return true;
                        }
                    ));
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
