using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GiftscopModifier.WPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Debug.WriteLine(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina"));
			
			if (!(Directory.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina")) && Directory.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop"))))
			{
				MessageBox.Show(this, "You do not have a Giftscop installation or have not booted it up yet.\nPlease boot up Giftscop before booting up this application.", "No Giftscop folder found.", MessageBoxButton.OK, MessageBoxImage.Stop, MessageBoxResult.OK);
				Environment.Exit(1);
			}
		}

		private void FileSelection_Open(object sender, EventArgs e)
		{
			FileSelection.Items.Clear();
			
			if (Directory.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\savedata"))) {
				if (File.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\savedata\\file0.sav"))) {
					ComboBoxItem comboBoxItem = new ComboBoxItem();
					comboBoxItem.Content = "Save - Save File 1";

					FileSelection.Items.Add(comboBoxItem);
				}

				if (File.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\savedata\\file1.sav"))) {
					ComboBoxItem comboBoxItem = new ComboBoxItem();
					comboBoxItem.Content = "Save - Save File 2";

					FileSelection.Items.Add(comboBoxItem);
				}

				if (File.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\savedata\\file2.sav"))) {
					ComboBoxItem comboBoxItem = new ComboBoxItem();
					comboBoxItem.Content = "Save - Save File 3";

					FileSelection.Items.Add(comboBoxItem);
				}
			}

			if (Directory.Exists(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\recordings\\")))
			{
				string[] recordings = Directory.GetFiles(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\recordings"))
					.Where((string recording) => recording.EndsWith(".rec", StringComparison.Ordinal))
					.Select((string recording) => recording.Replace(System.IO.Path.GetFullPath(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\..\\LocalLow\\Garalina\\Petscop\\recordings\\"), null, StringComparison.Ordinal))
					.Select((string recording) => recording.Replace(".rec", null, StringComparison.Ordinal)).ToArray();

                foreach (string recording in recordings)
                {
					ComboBoxItem comboBoxItem = new ComboBoxItem();
					comboBoxItem.Content = "Recording - " + recording;

					FileSelection.Items.Add(comboBoxItem);
				}
            }
		}
	}
}