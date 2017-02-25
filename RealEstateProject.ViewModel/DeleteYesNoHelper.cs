using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace RealEstateProject.ViewModel
{
    public class DeleteYesNoHelper
    {
            public static bool DeleteYesNoMessageBox(string text,string title)
        {
            DialogResult dialogResult = System.Windows.Forms.MessageBox.Show(text, title, MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }
    }
}
