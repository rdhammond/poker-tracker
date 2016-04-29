using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GUI.BindingObjects
{
    // ** DEBUG
    public class TimeEntry { }

    public class Blinds : IDataErrorInfo, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _smallBlind, _bigBlind;
        private DateTime? _startTime, _endTime;

        private readonly ObservableCollection<TimeEntry> _timeEntries = 
            new ObservableCollection<TimeEntry>();

        public int SmallBlind
        {
            get { return _smallBlind; }
            set
            {
                _smallBlind = value;
                OnPropertyChanged("SmallBlind");
            }
        }

        public int BigBlind
        {
            get { return _bigBlind; }
            set
            {
                _bigBlind = value;
                OnPropertyChanged("BigBlind");
            }
        }

        public DateTime? StartTime
        {
            get { return _startTime; }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        public DateTime? EndTime
        {
            get { return _endTime; }
            set
            {
                _endTime = value;
                OnPropertyChanged("EndTime");
            }
        }

        public ObservableCollection<TimeEntry> TimeEntries
        {
            get { return _timeEntries; }
        }

        public string Error
        {
            get { return string.Empty; }
        }

        public string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;

                switch (columnName)
                {
                    case "BigBlind":
                        if (SmallBlind >= BigBlind)
                            errorMessage = "Small blind cannot be greater than big blind.";
                        break;
                }

                return errorMessage;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
