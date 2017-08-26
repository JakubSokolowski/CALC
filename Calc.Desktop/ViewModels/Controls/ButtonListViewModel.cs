
using System;
using System.ComponentModel;

namespace Calc.Desktop
{
    public class ButtonListViewModel : BaseViewModel
    {
        private string mListContent = "";

        public BindingList<SquareButtonViewModel> Buttons { get; set; }
        public string ListContent
        {
            get => mListContent;
            set
            {
                mListContent = value;
            }
        }              

        public ButtonListViewModel()
        {
            Buttons = new BindingList<SquareButtonViewModel>();
            Mediator.Instance.Register((Object o) =>
            {
                RefreshButtonList();
                Mediator.Instance.NotifyColleagues(ViewModelMessages.RepresentationUpdated, ListContent);
            },  ViewModelMessages.BitFlipped);
        }           

        // Adding

        public void AddButtonsToEnd(int buttonCount)
        {
            for (int i = 0; i < buttonCount; i++)
                Buttons.Add(new SquareButtonViewModel());
        }
        public void AddButtonsWithEnumeration(int buttonCount, string content, int enumerationStart)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                Buttons.Add(new SquareButtonViewModel { SingleCharContent = content, Tag = enumerationStart.ToString() });
                enumerationStart++;
            }
        }
        public void SetButtonCount(int newCount)
        {
            if (newCount < Buttons.Count)
                RemoveButtonsFromEnd(Buttons.Count - newCount);
            if (newCount > Buttons.Count)
                AddButtonsToEnd(newCount - Buttons.Count);
        }

        //Removing

        public void RemoveButtonsFromEnd(int buttonCount)
        {
            if (buttonCount > Buttons.Count)
                throw new IndexOutOfRangeException("Attempted to reomve more buttons than there are");
            for (int i = 0; i < buttonCount; i++)
                Buttons.RemoveAt(Buttons.Count - 1);
        }
        public void RemoveAllButtons()
        {
            Buttons.Clear();
        }

        // Changing
        
        public void WriteOnButtons(string text)
        {            
            SetButtonCount(text.Length);
            ChangeListText(text);
        }       
        public void ClearButtonContent()
        {
            SetAllButtonsContent("");
        }
        public void SetAllButtonsContent(string newContent)
        {
            foreach (var button in Buttons)
                button.SingleCharContent = newContent[0].ToString();
        }
        public void ChangeListText(string newText)
        {
            for (int i = 0; i < newText.Length; i++)            
                Buttons[i].SingleCharContent = newText[i].ToString();           
        }      
        public void ChangeSingleButtonContent(int index, string newContent)
        {
            Buttons[index].SingleCharContent = newContent.Substring(0, 1);
        }
        

        public string GetAllButtonsText()
        {
            string content = "";
            foreach(var button in Buttons)
            {
                content += button.SingleCharContent;
            }
            return content;
        }      
        private void RefreshButtonList()
        {
            ListContent = GetAllButtonsText();                   
        }
    }
}
