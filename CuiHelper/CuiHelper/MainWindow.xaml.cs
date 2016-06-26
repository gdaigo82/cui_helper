using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CuiHelper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private CuiHelperData m_data;
        private CuiHelperAppManager m_appManager;
        private CuiHelperFactory m_factory;

        private void MakeCuiHelperResources()
        {
            m_data = new CuiHelperData();
            this.DataContext = m_data;
            m_factory = new CuiHelperFactory(m_data, OutputBrowser, InputTextBox, InputComboBox);
            m_appManager = m_factory.GetAppManager();

            m_factory.Start();
        }

        public MainWindow()
        {
            InitializeComponent();
            MakeCuiHelperResources();
        }

        private void WindowDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files != null && m_appManager != null)
            {
                m_appManager.DragAndDropEvent(files);
            }
        }

        private void WindowPreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, true))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void ClickGo(object sender, RoutedEventArgs e)
        {
            CuiHelperComboBoxData data = (CuiHelperComboBoxData)InputComboBox.SelectedItem;
            if (data == null)
            {
                return;
            }
            m_appManager.ButtonEvent(data.Commnad);
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                m_appManager.TextBoxEvent(InputTextBox.Text);
            }
        }
    }
}
