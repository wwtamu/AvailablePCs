using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

namespace AvailablePCs
{
    public class Utilities
    {
        private static bool messageDisplayed;

        public Utilities() 
        {
            messageDisplayed = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        public async void PromptMessage(String message)
        {
            if (messageDisplayed == false)
            {
                messageDisplayed = true;

                // Create the message dialog and set its content and title 
                var messageDialog = new MessageDialog(message, "Lab Computer Availability");

                // Add commands and set their callbacks 
                messageDialog.Commands.Add(new UICommand("OK", (command) =>
                {
                    messageDisplayed = false;
                }));

                // Set the command that will be invoked by default 
                messageDialog.DefaultCommandIndex = 1;

                // Show the message dialog 
                await messageDialog.ShowAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        public async Task WaitablePromptMessage(String message)
        {
            if (messageDisplayed == false)
            {
                messageDisplayed = true;

                // Create the message dialog and set its content and title 
                var messageDialog = new MessageDialog(message, "Lab Computer Availability");

                // Add commands and set their callbacks 
                messageDialog.Commands.Add(new UICommand("OK", (command) =>
                {
                    messageDisplayed = false;
                }));

                // Set the command that will be invoked by default 
                messageDialog.DefaultCommandIndex = 1;

                try
                {
                    // Show the message dialog                 
                    await messageDialog.ShowAsync();
                }
                catch (Exception) { }
            }
        }

    }
}
