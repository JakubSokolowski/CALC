
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
        public BindingList<SquareButtonViewModel> Buttons { get; set; } 
        

        public ButtonListViewModel()
        {
           Buttons = new BindingList<SquareButtonViewModel>();
        }

        public void AddButtons(int buttonCount)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                Buttons.Add(new SquareButtonViewModel());                
            }
        }

        public void AddButtons(int buttonCount, string content)
        {
            for (int i = 0; i < buttonCount; i++)
            {
                Buttons.Add(new SquareButtonViewModel { SingleCharContent = content, Tag = "DUPA" });
            }
        }

        public void WriteText(string text)
        {
            Buttons.Clear();
            for(int i = 0; i < text.Length;i++)
            {
                var button = new SquareButtonViewModel();
                button.SingleCharContent = text[i].ToString();
                button.Tag = "AYY LMOA";
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
    }
}
