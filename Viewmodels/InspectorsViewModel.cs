using System.Collections.ObjectModel;
using System.Linq;
using Тестовое_задание.Models;
using Тестовое_задание.Base;
using System.Windows;


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
        private Inspectors _EditInspector;
        private Inspectors _SelectedInspector;
        private bool _IsButtonsEnable;
        private Visibility _EditPanelVisibility;


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
        public Inspectors EditInspector 
        {
            get 
            {
                return _EditInspector;
            }
            set
            {
                _EditInspector = value;
                OnPropertyChanged("EditInspector");
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


        public Command AddCommand
        {
            get
            {
                return _AddCommand ??
                  (_AddCommand = new Command(obj =>
                  {
                      EditInspector = new Inspectors();
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
                      EditInspector.ID = SelectedInspector.ID;
                      EditInspector.Name = SelectedInspector.Name;
                      EditInspector.Number = SelectedInspector.Number;
                      _Status = EntityStatus.Edit;
                      EditPanelVisibility = Visibility.Visible;
                      IsButtonsEnable = false;
                      OnPropertyChanged("EditInspector");
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
                      using (UserContext db = new UserContext())
                      {
                          db.Inspectors.Attach(InspectorsTable.Single(i => i.ID == SelectedInspector.ID));
                          db.Inspectors.Remove(InspectorsTable.Single(i => i.ID == SelectedInspector.ID));
                          db.SaveChanges();
                      }

                      InspectorsTable.Remove(SelectedInspector);
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
                      if(_Status == EntityStatus.New)
                      {
                          InspectorsTable.Add(new Inspectors(EditInspector.ID, EditInspector.Name, EditInspector.Number));
                          using (UserContext db = new UserContext())
                          {
                              db.Inspectors.Add(new Inspectors(EditInspector.ID, EditInspector.Name, EditInspector.Number));
                              db.SaveChanges();
                          }
                      }
                      if(_Status == EntityStatus.Edit)
                      {
                          InspectorsTable.Single(i => i.ID == EditInspector.ID).Name = EditInspector.Name;
                          InspectorsTable.Single(i => i.ID == EditInspector.ID).Number = EditInspector.Number;
                          using (UserContext db = new UserContext())
                          {
                              db.Inspectors.Single(i => i.ID == EditInspector.ID).Name = EditInspector.Name;
                              db.Inspectors.Single(i => i.ID == EditInspector.ID).Number = EditInspector.Number;
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
            EditInspector = new Inspectors();
            EditPanelVisibility = Visibility.Collapsed;
            IsButtonsEnable = true;
            Update();
        }
    }        
}
