using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Тестовое_задание.Models;
using Тестовое_задание.Base;
using System.Collections.ObjectModel;
using System.Windows;
using Тестовое_задание.Views;

namespace Тестовое_задание.Viewmodels
{
    public class InspectionsViewModel : BaseViewModel
    {
        private Command _ShowInspectorsWindowCommand;
        private Command _AddInspectionCommand;
        private Command _EditInspectionCommand;
        private Command _DeleteInspectionCommand;
        private Command _SaveInspectionCommand;
        private Command _CancelInspectionCommand;
        private Command _AddNoticeCommand;
        private Command _EditNoticeCommand;
        private Command _DeleteNoticeCommand;
        private Command _SaveNoticeCommand;
        private Command _CancelNoticeCommand;
        private List<Inspections> _AllInspections;
        private List<Notices> _AllNotices;
        private Visibility _EditInspectionVisibility;
        private Visibility _EditNoticeVisibility;
        private Visibility _GridInspectionVisibility;
        private Visibility _GridNoticeVisibility;
        private bool _IsSaveButtonEnable;
        private string _NoticeText;
        private string _NoticeComment;
        private DateTime _NoticeDate;
        private string _InspectionDesignation;
        private string _InspectionComment;
        private DateTime _InspectionDate;
        private Inspectors _Inspector;
        private InspectionTables _SelectedInspection;
        private Notices _SelectedNotice;
        private Inspectors _FilterInspector;
        private string _FilterDesignation;
        private EntityStatus _InspectionStatus;
        private EntityStatus _NoticeStatus;
        private Guid _InspectionID;
        private Guid _NoticeID;

        public ObservableCollection<InspectionTables> InspectionsTable { get; set; }
        public ObservableCollection<Notices> NoticesTable { get; set; }
        public Inspectors FilterInspector
        {
            get
            {
                return _FilterInspector;
            }
            set
            {
                _FilterInspector = value;
                if (value == null)
                {
                    foreach (Inspections inspection in _AllInspections)
                    {
                        InspectionsTable.Add(new InspectionTables()
                        {
                            ID = inspection.ID,
                            Designation = inspection.Designation,
                            DateOfInspection = inspection.DateOfInspection,
                            Comment = inspection.Comment,
                            Inspector = inspection.Inspector,
                            NoticeQuantity = _AllNotices.Count(i => i.Inspection.ID == inspection.ID)
                        });
                    }
                }
                else
                {
                    InspectionsTable.Clear();
                    foreach (Inspections inspection in _AllInspections.Where(i => i.Inspector == value))
                    {
                        InspectionsTable.Add(new InspectionTables()
                        {
                            ID = inspection.ID,
                            Designation = inspection.Designation,
                            DateOfInspection = inspection.DateOfInspection,
                            Comment = inspection.Comment,
                            Inspector = inspection.Inspector,
                            NoticeQuantity = _AllNotices.Count(i => i.Inspection.ID == inspection.ID)
                        });
                    }
                }
                OnPropertyChanged("FilterInspector");
                OnPropertyChanged("InspectionsTable");
            }
        }
        public string FilterDesignation
        {
            get
            {
                return _FilterDesignation;
            }
            set
            {
                _FilterDesignation = value;
                if (value == null)
                {
                    foreach (Inspections inspection in _AllInspections)
                    {
                        InspectionsTable.Add(new InspectionTables()
                        {
                            ID = inspection.ID,
                            Designation = inspection.Designation,
                            DateOfInspection = inspection.DateOfInspection,
                            Comment = inspection.Comment,
                            Inspector = inspection.Inspector,
                            NoticeQuantity = _AllNotices.Count(i => i.Inspection.ID == inspection.ID)
                        });
                    }
                }
                else
                {
                    InspectionsTable.Clear();
                    foreach (Inspections inspection in _AllInspections.Where(i => i.Designation.Contains(value)))
                    {
                        InspectionsTable.Add(new InspectionTables()
                        {
                            ID = inspection.ID,
                            Designation = inspection.Designation,
                            DateOfInspection = inspection.DateOfInspection,
                            Comment = inspection.Comment,
                            Inspector = inspection.Inspector,
                            NoticeQuantity = _AllNotices.Count(i => i.Inspection.ID == inspection.ID)
                        });
                    }
                }
                OnPropertyChanged("FilterDesignation");
                OnPropertyChanged("InspectionsTable");
            }
        }
        public List<Inspectors> InspectorsList { get; set; }
        public InspectionTables SelectedInspection
        {
            get
            {
                return _SelectedInspection;
            }
            set
            {

                _SelectedInspection = value;
                NoticesTable.Clear();
                if (value != null)
                {
                    foreach (Notices notice in _AllNotices.Where(i => i.Inspection.ID == value.ID))
                    {
                        NoticesTable.Add(notice);
                    }
                    InspectionDesignation = value.Designation;
                }
                OnPropertyChanged("InspectionDesignation");
                OnPropertyChanged("SelectedInspection");
                OnPropertyChanged("NoticesTable");
            }
        }
        public Notices SelectedNotice
        {
            get
            {
                return _SelectedNotice;
            }
            set
            {
                _SelectedNotice = value;
                OnPropertyChanged("SelectedNotice");
            }
        }
        public Visibility EditInspectionVisibility
        {
            get
            {
                return _EditInspectionVisibility;
            }
            set
            {
                _EditInspectionVisibility = value;
                OnPropertyChanged("EditInspectionVisibility");
            }
        }
        public Visibility EditNoticeVisibility
        {
            get
            {
                return _EditNoticeVisibility;
            }
            set
            {
                _EditNoticeVisibility = value;
                OnPropertyChanged("EditNoticeVisibility");
            }
        }
        public Visibility GridInspectionVisibility
        {
            get
            {
                return _GridInspectionVisibility;
            }
            set
            {
                _GridInspectionVisibility = value;
                OnPropertyChanged("GridInspectionVisibility");
            }
        }
        public Visibility GridNoticeVisibility
        {
            get
            {
                return _GridNoticeVisibility;
            }
            set
            {
                _GridNoticeVisibility = value;
                OnPropertyChanged("GridNoticeVisibility");
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
        public string InspectionLabelDesignation { get; set; }

        public string NoticeTextErrorMessage { get; set; }
        public string NoticeText
        {
            get
            {
                return _NoticeText;
            }
            set
            {
                IsSaveButtonEnable = true;
                NoticeTextErrorMessage = "";
                if (value == "")
                {
                    NoticeTextErrorMessage = "Поле \"Замечание\" не может быть пустым";
                    IsSaveButtonEnable = false;
                }
                _NoticeText = value;
                OnPropertyChanged("NoticeText");
                OnPropertyChanged("IsSaveButtonEnable");
                OnPropertyChanged("NoticeTextErrorMessage");
            }
        }
        public string NoticeComment
        {
            get
            {
                return _NoticeComment;
            }
            set
            {
                _NoticeComment = value;
                OnPropertyChanged("NoticeComment");
            }
        }
        public DateTime NoticeDate
        {
            get
            {
                return _NoticeDate;
            }
            set
            {
                _NoticeDate = value;
                OnPropertyChanged("NoticeDate");
            }
        }


        public string InspectionDesignatioErrorMessage { get; set; }
        public string InspectionDesignation
        {
            get
            {
                return _InspectionDesignation;
            }
            set
            {
                IsSaveButtonEnable = true;
                InspectionDesignatioErrorMessage = "";
                if (value == "")
                {
                    InspectionDesignatioErrorMessage = "Поле \"Название\" не может быть пустым";
                    IsSaveButtonEnable = false;
                }
                _InspectionDesignation = value;
                OnPropertyChanged("InspectionDesignation");
                OnPropertyChanged("IsSaveButtonEnable");
                OnPropertyChanged("InspectionDesignatioErrorMessage");
            }
        }
        public string InspectionComment
        {
            get
            {
                return _InspectionComment;
            }
            set
            {
                _InspectionComment = value;
                OnPropertyChanged("InspectionComment");
            }
        }
        public DateTime InspectionDate
        {
            get
            {
                return _InspectionDate;
            }
            set
            {
                _InspectionDate = value;
                OnPropertyChanged("InspectionDate");
            }
        }
        public Inspectors Inspector
        {
            get
            {
                return _Inspector;
            }
            set
            {
                IsSaveButtonEnable = true;
                if (value == null)
                {
                    InspectionDesignatioErrorMessage = "Поле \"Инспектор\" не может быть пустым";
                    IsSaveButtonEnable = false;
                }
                _Inspector = value;
                OnPropertyChanged("Inspector");
                OnPropertyChanged("IsSaveButtonEnable");
                OnPropertyChanged("InspectionDesignatioErrorMessage");
            }
        }

        public Command ShowInspectorsWindowCommand
        {
            get
            {
                return _ShowInspectorsWindowCommand ??
                  (_ShowInspectorsWindowCommand = new Command(obj =>
                  {
                      InspectorView inspectorview = new InspectorView();
                      if (inspectorview.ShowDialog() == true)
                      {
                          InspectorsList.Clear();
                          using (UserContext db = new UserContext())
                          {
                              InspectorsList.AddRange(db.Inspectors.ToList());
                          }
                          OnPropertyChanged("InspectorsList");
                      }
                  }));
            }
        }
        public Command AddInspectionCommand
        {
            get
            {
                return _AddInspectionCommand ??
                  (_AddInspectionCommand = new Command(obj =>
                  {
                      InspectionDesignation = "";
                      InspectionComment = "";
                      InspectionDate = DateTime.Today;
                      Inspector = null;
                      _InspectionStatus = EntityStatus.New;
                      EditInspectionVisibility = Visibility.Visible;
                      GridNoticeVisibility = Visibility.Collapsed;
                  }));
            }
        }
        public Command EditInspectionCommand
        {
            get
            {
                return _EditInspectionCommand ??
                  (_EditInspectionCommand = new Command(obj =>
                  {
                      if(SelectedInspection != null)
                      { 
                      _InspectionID = SelectedInspection.ID;
                      InspectionDesignation = SelectedInspection.Designation;
                      InspectionComment = SelectedInspection.Comment;
                      InspectionDate = SelectedInspection.DateOfInspection;
                      Inspector = SelectedInspection.Inspector;
                      _InspectionStatus = EntityStatus.Edit;
                      EditInspectionVisibility = Visibility.Visible;
                      GridNoticeVisibility = Visibility.Collapsed;
                      }
                  }));
            }
        }
        public Command DeleteInspectionCommand
        {
            get
            {
                return _DeleteInspectionCommand ??
                  (_DeleteInspectionCommand = new Command(obj =>
                  {
                      if (SelectedInspection != null)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Inspections.Attach(_AllInspections.Single(i => i.ID == SelectedInspection.ID));
                              db.Inspections.Remove(_AllInspections.Single(i => i.ID == SelectedInspection.ID));
                              db.SaveChanges();
                          }
                          _AllInspections.Remove(_AllInspections.Single(i => i.ID == SelectedInspection.ID));
                          InspectionsTable.Remove(SelectedInspection);
                      }
                  }));
            }
        }
        public Command SaveInspectionCommand
        {
            get
            {
                return _SaveInspectionCommand ??
                  (_SaveInspectionCommand = new Command(obj =>
                  {
                      if (_InspectionStatus == EntityStatus.New)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Inspections.Add(new Inspections(Guid.NewGuid(), InspectionDesignation, Inspector.ID, InspectionDate, InspectionComment));
                              db.SaveChanges();
                          }
                      }
                      if (_InspectionStatus == EntityStatus.Edit)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Inspections.Single(i => i.ID == _InspectionID).Designation = InspectionDesignation;
                              db.Inspections.Single(i => i.ID == _InspectionID).DateOfInspection = InspectionDate;
                              db.Inspections.Single(i => i.ID == _InspectionID).Comment = InspectionComment;
                              db.Inspections.Single(i => i.ID == _InspectionID).InspectorID = Inspector.ID;
                              db.SaveChanges();
                          }
                      }
                      EditInspectionVisibility = Visibility.Collapsed;
                      GridNoticeVisibility = Visibility.Visible;
                      Update();
                  }));
            }
        }
        public Command CancelInspectionCommand
        {
            get
            {
                return _CancelInspectionCommand ??
                  (_CancelInspectionCommand = new Command(obj =>
                  {
                      EditInspectionVisibility = Visibility.Collapsed;
                      GridNoticeVisibility = Visibility.Visible;
                  }));
            }
        }
        public Command AddNoticeCommand
        {
            get
            {
                return _AddNoticeCommand ??
                  (_AddNoticeCommand = new Command(obj =>
                  {
                      if(SelectedInspection != null)
                      { 
                      NoticeText = "";
                      NoticeComment = "";
                      NoticeDate = DateTime.Today;
                      _NoticeStatus = EntityStatus.New;
                      GridInspectionVisibility = Visibility.Collapsed;
                      EditNoticeVisibility = Visibility.Visible;
                      }
                  }));
            }
        }
        public Command EditNoticeCommand
        {
            get
            {
                return _EditNoticeCommand ??
                  (_EditNoticeCommand = new Command(obj =>
                  {
                      if (SelectedNotice != null)
                      {
                          _NoticeID = SelectedNotice.ID;
                          NoticeText = SelectedNotice.Text;
                          NoticeComment = SelectedNotice.Comment;
                          NoticeDate = SelectedNotice.DateOfElimination;
                          _NoticeStatus = EntityStatus.Edit;
                          GridInspectionVisibility = Visibility.Collapsed;
                          EditNoticeVisibility = Visibility.Visible;
                      }
                  }));
            }
        }
        public Command DeleteNoticeCommand
        {
            get
            {
                return _DeleteNoticeCommand ??
                  (_DeleteNoticeCommand = new Command(obj =>
                  {
                      if (SelectedNotice != null)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Notices.Attach(_AllNotices.Single(i => i.ID == SelectedNotice.ID));
                              db.Notices.Remove(_AllNotices.Single(i => i.ID == SelectedNotice.ID));
                              db.SaveChanges();
                          }
                          _AllNotices.Remove(SelectedNotice);
                          NoticesTable.Remove(SelectedNotice);
                      }
                  }));
            }
        }
        public Command SaveNoticeCommand
        {
            get
            {
                return _SaveNoticeCommand ??
                  (_SaveNoticeCommand = new Command(obj =>
                  {
                      if (_NoticeStatus == EntityStatus.New)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Notices.Add(new Notices(Guid.NewGuid(), NoticeText, NoticeDate, NoticeComment, SelectedInspection.ID));
                              db.SaveChanges();
                          }
                      }
                      if (_NoticeStatus == EntityStatus.Edit)
                      {
                          using (UserContext db = new UserContext())
                          {
                              db.Notices.Single(i => i.ID == _NoticeID).Text = NoticeText;
                              db.Notices.Single(i => i.ID == _NoticeID).Comment = NoticeComment;
                              db.Notices.Single(i => i.ID == _NoticeID).DateOfElimination = NoticeDate;
                              db.Notices.Single(i => i.ID == _NoticeID).InspectionID = SelectedInspection.ID;
                              db.SaveChanges();
                          }
                      }
                      GridInspectionVisibility = Visibility.Visible;
                      EditNoticeVisibility = Visibility.Collapsed;
                      Update();
                  }));
            }
        }
        public Command CancelNoticeCommand
        {
            get
            {
                return _CancelNoticeCommand ??
                  (_CancelNoticeCommand = new Command(obj =>
                  {
                      GridInspectionVisibility = Visibility.Visible;
                      EditNoticeVisibility = Visibility.Collapsed;
                  }));
            }
        }

        public InspectionsViewModel()
        {
            _AllInspections = new List<Inspections>();
            _AllNotices = new List<Notices>();
            InspectionsTable = new ObservableCollection<InspectionTables>();
            NoticesTable = new ObservableCollection<Notices>();
            InspectorsList = new List<Inspectors>();
            GridInspectionVisibility = Visibility.Visible;
            GridNoticeVisibility = Visibility.Visible;
            EditInspectionVisibility = Visibility.Collapsed;
            EditNoticeVisibility = Visibility.Collapsed;
            Update();
        }

        private void Update()
        {
            InspectionsTable.Clear();
            _AllInspections.Clear();
            _AllNotices.Clear();
            InspectorsList.Clear();
            using (UserContext db = new UserContext())
            {
                _AllInspections.AddRange(db.Inspections.ToList());
                _AllNotices.AddRange(db.Notices.ToList());
                InspectorsList.AddRange(db.Inspectors.ToList());
                foreach (Inspections inspection in _AllInspections)
                {
                    InspectionsTable.Add(new InspectionTables()
                    {
                        ID = inspection.ID,
                        Designation = inspection.Designation,
                        DateOfInspection = inspection.DateOfInspection,
                        Comment = inspection.Comment,
                        Inspector = inspection.Inspector,
                        NoticeQuantity = _AllNotices.Count(i => i.Inspection.ID == inspection.ID)
                    });
                }
            }
            OnPropertyChanged("InspectionsTable");
        }
    }


    public class InspectionTables
    {
        public Guid ID { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfInspection { get; set; }
        public Inspectors Inspector { get; set; }
        public string Comment { get; set; }
        public int NoticeQuantity { get; set; }

        public InspectionTables()
        {
        }
    }
}
