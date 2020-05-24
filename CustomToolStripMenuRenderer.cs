using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public class CustomToolStripMenuRenderer : ToolStripProfessionalRenderer
    {
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Enabled)
                base.OnRenderMenuItemBackground(e);
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Enabled)
                base.OnRenderMenuItemBackground(e);
        }
    }
}
