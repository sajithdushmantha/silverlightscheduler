using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Aciades.Businessnext.SchedulerControl.Common;
using Acidaes.BusinessNext.UI.Silverlight;
using Acidaes.UI.Silverlight.DragDrop;

namespace Aciades.Businessnext.SchedulerControl.ViewModel
{
    public class SchedulerModel : BindableModel
    {
        #region Properties
        /// <summary>
        /// Backing field for the CurrentDateView property
        /// </summary>
        private Common.Enumerations.DateView currentDateView;

        /// <summary>
        /// Gets or sets CurrentDateView.
        /// </summary>
        /// <value>
        /// The currentDateView.
        /// </value>
        public Common.Enumerations.DateView CurrentDateView
        {
            get
            {
                return this.currentDateView;
            }

            set
            {
                if (this.currentDateView != value)
                {
                    this.currentDateView = value;
                    OnPropertyChanged("CurrentDateView");
                }
            }
        }

        /// <summary>
        /// Backing field for the CurrentDate property
        /// </summary>
        private DateTime currentDate =DateTime.Now;

        /// <summary>
        /// Gets or sets CurrentDate.
        /// </summary>
        /// <value>
        /// The currentDate.
        /// </value>
        public DateTime CurrentDate
        {
            get
            {
                return this.currentDate;
            }

            set
            {
                if (this.currentDate != value)
                {
                    this.currentDate = value;
                    OnPropertyChanged("CurrentDate");
                }
            }
        }

        private Enumerations.TimeSlotsPerHour timeslotPerHour = Enumerations.TimeSlotsPerHour.Fifteen;
        private Enumerations.HoursPerDay hoursperDay = Enumerations.HoursPerDay.TwentyFour;

        public Enumerations.TimeSlotsPerHour TimeSlotPerHour
        {
            get
            {
                return timeslotPerHour;
            }
            set
            {
                if (this.timeslotPerHour != value)
                {
                    timeslotPerHour = value;
                    OnPropertyChanged("TimeSlotPerHour");
                }
            }
        }

        public Enumerations.HoursPerDay HoursPerDay
        {
            get { return hoursperDay; }
            set
            {
                if (hoursperDay != value)
                {
                    hoursperDay = value;
                    OnPropertyChanged("HoursPerDay");
                }
            }
        }

        /// <summary>
        /// Control Drop Command
        /// </summary>
        private DropCommand controlDropCommand;

        /// <summary>
        /// Gets or sets the Control Drop Command
        /// </summary>
        public DropCommand ControlDropCommand
        {
            get
            {
                return this.controlDropCommand;
            }

            set
            {
                if (this.controlDropCommand != value)
                {
                    this.controlDropCommand = value;
                    OnPropertyChanged("ControlDropCommand");
                }
            }
        }

        #endregion

        public SchedulerModel()
        {
            ControlDropCommand = new DropCommand(Command_OnDrop);
        }

        public virtual void Command_OnDrop(object sender, DropCommandArgs args)
        {
            
        }
    }
}
