using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace sarisari
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(caller));
            }
        }
        protected void OnPropertyChanged(int propertyValue)
        {
            VerifyPropertyName(propertyValue);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyValue.ToString()));
            }
        }
        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if (updatingFlag.GetPropertyValue())
                return;
            updatingFlag.SetPropertyValue(true);

            try
            {
                await action();
            }
            finally { updatingFlag.SetPropertyValue(false); }
        }
        [Conditional("DEBUG")]
        private void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
                throw new ArgumentNullException(GetType().Name + "does not contain property: " + propertyName);
        }
        [Conditional("DEBUG")]
        private void VerifyPropertyName(int propertyValue)
        {
            if (TypeDescriptor.GetProperties(this)[propertyValue] == null)
                throw new ArgumentNullException(GetType().Name + " does not contain property: " + propertyValue.ToString());
        }
    }
}
