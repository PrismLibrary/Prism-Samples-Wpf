// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestLibrary.ApplicationHelper;
using System.Timers;
using System.Globalization;

namespace AcceptanceTestLibrary.ApplicationObserver
{
    /// <summary>
    /// Class to diagnose the state of the object and the control flow.
    /// </summary>
    public sealed class StateDiagnosis
    {
        private static readonly StateDiagnosis instance = new StateDiagnosis();
        //maintain the mapping of all Observers and Timers
        private Dictionary<IStateObserver, Timer> observerTimerList = new Dictionary<IStateObserver, Timer>();
        private static bool isFailed;
        private string waitTime = ConfigHandler.GetValue("ApplicationLoadWaitTime");

        private StateDiagnosis()
        { }

        public static StateDiagnosis Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Static property to set the diagnosis status.
        /// </summary>
        public static Boolean IsFailed
        {
            get { return isFailed; }
        }

        /// <summary>
        /// Initiate the diagnosis
        /// </summary>
        /// <param name="observer">Object of type IStateObserver</param>
        public void StartDiagnosis(IStateObserver observer)
        {
            // Starting a timer for this observer to keep track of changes
            StartTimer(observer);
        }

        /// <summary>
        /// Stop Diagnosis
        /// </summary>
        /// <param name="observer">Object of type IStateObserver</param>
        public void StopDiagnosis(IStateObserver observer)
        {
            // Stop the timer as the caller is stopping the diagnosis.
            StopTimer(observer);
        }

        #region Private Helper Methods

        /// <summary>
        /// Start the timer.
        /// </summary>
        private void StartTimer(IStateObserver observer)
        {
            ResolveTimer(observer).Start();
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        private void StopTimer(IStateObserver observer)
        {
            Timer timer = observerTimerList[observer];
            if (null != timer)
            {
                timer.Stop();
            }
            observerTimerList.Remove(observer);

            isFailed = false;
        }

        private Timer ResolveTimer(IStateObserver observer)
        {
            if (observerTimerList.ContainsKey(observer))
            {
                return observerTimerList[observer];
            }
            else
            {
                Timer timer = CreateTimer();
                observerTimerList.Add(observer, timer);
                return timer;
            }
        }

        private Timer CreateTimer()
        {
            Timer timer = new Timer();
            //check after waitTime sec
            timer.Interval = Convert.ToDouble(waitTime, CultureInfo.InvariantCulture);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            return timer;
        }

        /// <summary>
        /// ElapsedEventHandler for Timer
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Elapsed Event Arguments</param>
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            Timer timer = (Timer)sender;

            // Stop the timer and notify the observers.
            timer.Stop();
            isFailed = true;

            // Notify only the hooked up observer.
            IStateObserver observer = observerTimerList.First(list => list.Value.Equals(timer)).Key;
            NotifyObserver(observer);
        }

        /// <summary>
        /// Method to notify observer about the state change.
        /// </summary>
        private static void NotifyObserver(IStateObserver observer)
        {
            // Notify the observer who is hooked up with the timer.
            observer.Notify();
        }

        #endregion
    }
}
