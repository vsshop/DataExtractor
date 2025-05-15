using Inspectra.Controls;
using Inspectra.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inspectra.Forms
{
    public partial class Main : Form
    {
        public Main(AngularView view, DataChangesService data)
        {
            InitializeComponent();
            Controls.Add(view);
        }
    }
}
