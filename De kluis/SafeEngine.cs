using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace De_kluis
{
    class SafeEngine
    {
        public int pinLenght;
        public string secretPin;

        private string enteredPin = "";
        private bool statusOpen;

        public SafeEngine()
        {
            pinLenght = 4;
            secretPin = "0000";
        }

        public SafeEngine(int lenght, string pinCode)
        {
            Regex regex = new Regex("^[0-9]+$");

            if (regex.IsMatch(pinCode))
            {
                pinLenght = lenght;
                secretPin = pinCode;
            }
            else
            {
                pinLenght = 4;
                secretPin = "0000";
            }
        }

        public string getDisplayText()
        {
            var ster = "";

            if (enteredPin == string.Empty)
            {
                for (int i = 0; i < pinLenght; i++)
                {
                    ster += "*";
                }

                return ster;
            }
            else
            {
                ster += enteredPin;

                for (int i = 0; i < pinLenght - enteredPin.Length; i++)
                {
                    ster += "*";
                }

                return ster;
            }
        }

        public void numberPressed(KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                if (enteredPin.Length == pinLenght)
                {
                    return;
                }
                else
                {
                    enteredPin += e.KeyChar;
                }

                
            }
            else if (e.KeyChar == (char)Keys.Back)
            {
                backspacePress();
            }
            else
            {
                e.Handled = true;
                return;
            }
        }

        public void Open()
        {
            if (enteredPin == secretPin)
            {
                playSound();
                statusOpen = true;
                MessageBox.Show("Kluis open");
            }
        }        

        public void Close()
        {
            if (statusOpen)
            {
                enteredPin = "";
                statusOpen = false;
                MessageBox.Show("Kluis is gesloten");
            }
        }

        public bool isOpen()
        {
            return statusOpen;
        }

        public string getStatus()
        {
            if (statusOpen)
            {
                return "Open";
            }
            else
            {
                return "Close";
            }
        }

        public void reset()
        {
            enteredPin = "";
        }

        public string backspacePress()
        {
            if (enteredPin.Length > 0)
            {
                enteredPin = enteredPin.Remove(enteredPin.Length - 1);

                return getDisplayText();
            }

            return getDisplayText();
        }

        public void playSound()
        {
            Stream stream = Properties.Resources.Peter_Griffin___Tada_YXJdw0v9ZMk;

            SoundPlayer soundPlayer = new SoundPlayer(stream);

            soundPlayer.Play();
        }
    }
}
