using System.Windows;
using Cores.Dtos.UserRequests;

namespace UserRequestApp
{
    public partial class SummaryWindow : Window
    {
        public SummaryWindow(RequestSummaryDto summaryData)
        {
            InitializeComponent();
            DataContext = summaryData;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}