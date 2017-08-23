
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Desktop
{
    public class ButtonListViewModel : BaseViewModel
    {

        #region Private members

        private string mListContent = "";

        #endregion

        #region Public Properties

        public BindingList<SquareButtonViewModel> Buttons { get; set; }


        public string ListContent
        {
            get => mListContent;
            set
            {
                mListContent = value;
            }
        }

        #endregion

        #region Constructing

        public ButtonListViewModel()
        {
            Buttons = new BindingList<SquareButtonViewModel>();
            Mediator.Instance.Register((Object o) =>
            {
                RefreshList();
                Mediator.Instance.NotifyColleagues(ViewModelMessages.RepresentationUpdated, ListContent);
            },  ViewModelMessages.BitFlipped);

            this.PropertyChanged += new PropertyChangedEventHandler(OnItemButtonPropertyChanged);
        }

        #endregion


        public void AddButtons(int buttonCount)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                Buttons.Add(new SquareButtonViewModel());                
            }
        }

        public void AddButtons(int buttonCount, string content, int enumerationStart = 0)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                Buttons.Add(new SquareButtonViewModel { SingleCharContent = content, Tag = enumerationStart.ToString() });
                enumerationStart++;
            }
        }

        public void SetButtons(int newCount, string buttonContent)
        {
            
        }


        public void WriteText(string text)
        {
            Buttons.Clear();
            for(int i = 0; i < text.Length;i++)
            {
                var button = new SquareButtonViewModel()
                {
                    SingleCharContent = text[i].ToString(),
                };
                Buttons.Add(button);              
            }
        }

        public string GetContent()
        {
            string content = "";
            foreach(var button in Buttons)
            {
                content += button.SingleCharContent;
            }
            return content;
        }

        private void OnItemButtonPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "Buttons")
            {

            }
        }

        private void RefreshList()
        {
            ListContent = GetContent();                   
        }
    }
}
