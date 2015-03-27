using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Xml;

namespace RtbEx
{
    public class SimpleViewModel
        : INotifyPropertyChanged
    {
        private const string cFirstString = "This is a plain text with incorporated http://hyper.links.info and it should be translated into https://rich.textblock.data/easylyBy=attachedProperty";
        private const string cSecondString = "Just a simple text and http://link.site.ru";

        public SimpleViewModel()
        {
            SomeText = cFirstString;
            ChangeTextCommand = new DelegateCommand(ChangeText);
        }

        private string _someText;
        public string SomeText
        {
            get { return _someText; }
            set
            {
                if (Equals(_someText, value))
                {
                    return;
                }

                _someText = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand ChangeTextCommand { get; private set; }

        private void ChangeText()
        {
            SomeText = string.Equals(SomeText, cFirstString)
                ? cSecondString
                : cFirstString;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
