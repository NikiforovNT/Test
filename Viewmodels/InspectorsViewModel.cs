using System.Collections.ObjectModel;
using System.Linq;
using Тестовое_задание.Models;
using Тестовое_задание.Base;
using System.Windows;
using System;


namespace Тестовое_задание.Viewmodels
{
    public class InspectorsViewModel : BaseViewModel
    {
        private Command _AddCommand;
        private Command _EditCommand;
        private Command _DeleteCommand;
        private Command _SaveCommand;
        private Command _CancelCommand;
        private EntityStatus _Status;
        private Inspectors _SelectedInspector;
        private bool _IsButtonsEnable;
        private bool _IsSaveButtonEnable;
        private Visibility _EditPanelVisibility;
        private int _Number;
        private string _Name;
        private Guid _ID;


        public ObservableCollection<Inspectors> InspectorsTable { get; set; }
        public Inspectors SelectedInspector
        {
            get
            {
                return _SelectedInspector;
            }
            set
            {
                _SelectedInspector = value;
                OnPropertyChanged("SelectedInspector");
            }
        }

        public Visibility EditPanelVisibility
        {
            get
            {
                return _EditPanelVisibility;
            }
            set
            {
                _EditPanelVisibility = value;
                OnPropertyChanged("EditPanelVisibility");
            }
        }
        public bool IsButtonsEnable
        {
            get
            {
                return _IsButtonsEnable;
            }
            set
            {
                _IsButtonsEnable = value;
                OnPropertyChanged("IsButtonsEnable");
            }
        }
        public bool IsSaveButtonEnable
        {
            get
            {
                return _IsSaveButtonEnable;
            }
            set
            {
                _IsSaveButtonEnable = value;
                OnPropertyChanged("IsSaveButtonEnable");
            }
        }
        public string ErrorMessage { get; set; }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                IsSaveButtonEnable = true;
                ErrorMessage = "";
                if (value == "")
                {
                    ErrorMessage = "Поле \"Имя инспектора\" не может быть пустым";
                    IsSaveButtonEnable = false;
                }
                _Name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("ErrorMessage");
            }
        }
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                IsSaveButtonEnable = true;
                ErrorMessage = "";
                if (value == 0)
                {
                    ErrorMessage = "Поле \"Номер Инспектора\" не может быть пустым";
                    IsSaveButtonEnable = false;
                }
                if (_Status == EntityStatus.New && InspectorsTable.FirstOrDefault(i => i.Number == value) != null)
                {
                    ErrorMessage = "Номер не уникальный";
                    IsSaveButtonEnable = false;
                }
                _Number = value;
                OnPropertyChanged("Number");
                OnPropertyChanged("ErrorMessage");
            }
        }

        public Command AddCommand
        {
            get
            {
                return _AddCommand ??
                  (_AddCommand = new Command(obj =>
                  {
                      Name = "";
                      Number = 0;
                      _Status = EntityStatus.New;
                      EditPanelVisibility = Visibility.Visible;
                      IsButtonsEnable = false;
                  }));
            }
        }
        public Command EditCommand
        {
            get
            {
                return _EditCommand ??
                  (_EditCommand = new Command(obj =>
                  {
                      if (SelectedInspector != null)
                      {
                          _ID = SelectedInspector.ID;
                          Name = SelectedInspector.Name;
                          Number = SelectedInspector.Number;
                          _Status = EntityStatus.Edit;
                          EditPanelVisibility = Visibility.Visible;
                          IsButtonsEnable = false;
                      }
                  }));
            }
        }
        public Command DeleteCommand
        {
            get
            {
                return _DeleteCommand ??
                  (_DeleteCommand = new Command(obj =>
                  {
                      if (SelectedInspector != null)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Inspectors.Attach(InspectorsTable.Single(i => i.ID == SelectedInspector.ID));
                              db.Inspectors.Remove(InspectorsTable.Single(i => i.ID == SelectedInspector.ID));
                              db.SaveChanges();
                          }
                          InspectorsTable.Remove(SelectedInspector);
                      }
                  }));
            }
        }
        public Command SaveCommand
        {
            get
            {
                return _SaveCommand ??
                  (_SaveCommand = new Command(obj =>
                  {
                      if (_Status == EntityStatus.New)
                      {                          
                          using (UserContext db = new UserContext())
                          {
                              db.Inspectors.Add(new Inspectors(Guid.NewGuid(), Name, Number));
                              db.SaveChanges();
                          }
                      }
                      if (_Status == EntityStatus.Edit)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Inspectors.Single(i => i.ID == _ID).Name = Name;
                              db.Inspectors.Single(i => i.ID == _ID).Number = Number;
                              db.SaveChanges();
                          }
                      }
                      EditPanelVisibility = Visibility.Collapsed;
                      IsButtonsEnable = true;
                      Update();
                  }));
            }
        }
        public Command CancelCommand
        {
            get
            {
                return _CancelCommand ??
                  (_CancelCommand = new Command(obj =>
                  {
                      EditPanelVisibility = Visibility.Collapsed;
                      IsButtonsEnable = true;
                  }));
            }
        }

        private void Update()
        {
            InspectorsTable.Clear();
            using (UserContext db = new UserContext())
            {
                foreach (Inspectors inspector in db.Inspectors)
                {
                    InspectorsTable.Add(inspector);
                }
            }
        }



        public InspectorsViewModel()
        {
            InspectorsTable = new ObservableCollection<Inspectors>();
            EditPanelVisibility = Visibility.Collapsed;
            IsButtonsEnable = true;
            Update();
        }
    }
}
