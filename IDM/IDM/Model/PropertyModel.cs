using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDM.Model
{
    class PropertyModel : INotifyPropertyChanged
    {
        private string _name = String.Empty;
        private string _value = String.Empty;

        public PropertyModel(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Value
        {
            get { return _value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region PropertyChangeHandler
        internal void OnPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
