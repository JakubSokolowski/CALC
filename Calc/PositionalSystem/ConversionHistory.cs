using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.PositionalSystem
{
    public class ConversionHistory
    {

        #region Private Members

        private Queue<Tuple<Number, Number>> mHistory;
        private int mCurrentEntryIndex = -1;

        #endregion

        #region Public Properties


        public int CurrentEntryIndex
        {
            get => mCurrentEntryIndex;
            set
            {
                if (value >= 0 && value < mHistory.Count)
                    mCurrentEntryIndex = value;
                else
                    throw new ArgumentException();
            }
        }
        public int FirstEntryIndex { get => 0; }
        public int LastEntryIndex { get => mHistory.Count - 1; }
        


        public int MaxSize { get; set; } = 10;
        public int Count { get => mHistory.Count; }
        public bool CanBrowseHistory { get => mHistory.Count > 1; }

        #endregion

        #region Constructors

        public ConversionHistory()
        {
            mHistory = new Queue<Tuple<Number, Number>>();
        }

        public ConversionHistory(int size)
        {
            mHistory = new Queue<Tuple<Number, Number>>();
            MaxSize = size;
        }

        #endregion

        #region Accessing History

        public Tuple<Number, Number> FirstEntry { get => mHistory.ElementAt(FirstEntryIndex); }
        public Tuple<Number, Number> LastEntry
        {
            get
            {
                CurrentEntryIndex = mHistory.Count;
                return mHistory.Last();
            }
        }
        public Tuple<Number, Number> CurrentEntry
        {   
         
            get => mHistory.ElementAt(CurrentEntryIndex);
        }

        public void GoBackInHistory()
        {
            if (Count < 2 || CurrentEntryIndex == 0)
                return;

            // Accesing history for the first time
            if (CurrentEntryIndex < 0)
                CurrentEntryIndex = LastEntryIndex - 1;
            else
                CurrentEntryIndex -= 1;
        }

        public void GoForwardInHistory()
        {
            if (CurrentEntryIndex == Count - 1)
                return;

            // Accesing history for the first time
            if (CurrentEntryIndex == -1)
                CurrentEntryIndex = FirstEntryIndex;
            else
                CurrentEntryIndex += 1;
        }

        #endregion

        #region Addding and Deleting Entries

        public void AddEntry(Number from, Number to)
        {
            if (mHistory.Count == 10)
                mHistory.Dequeue();

            mHistory.Enqueue(new Tuple<Number, Number>(from, to));
        }

        #endregion

        #region Helpers

        public void SetMaxEntrySize(int newSize)
        {
            if(newSize > MaxSize)
            {
                MaxSize = newSize;
                return;
            }
            else
            {
                // Remove the older entries
                for (int i = 0; i < MaxSize - newSize; i++)
                    mHistory.Dequeue();
            }
        }

        public void Clear()
        {
            mHistory.Clear();
        }

        #endregion
        
    }
}
