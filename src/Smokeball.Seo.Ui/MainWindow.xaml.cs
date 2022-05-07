using Smokeball.Seo.Ui.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Smokeball.Seo.Ui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SeoStatusResultModel seoStatusResultModel;
        private readonly IFetchSeoStatus seoStatus;

        public MainWindow(IFetchSeoStatus seoStatus)
        {
            InitializeComponent();
            seoStatusResultModel = new SeoStatusResultModel();
            this.DataContext = seoStatusResultModel;
            this.seoStatus = seoStatus;
        }

        private async void SearchExecuted(object target, ExecutedRoutedEventArgs e)
            => await WithDataFetchStatus(
                () => this.seoStatusResultModel, 
                FetchSeoStatusWithModelBinding);

        private async Task FetchSeoStatusWithModelBinding(SeoStatusResultModel model)
        {
            model.Rankings = string.Empty;
            var status = await FetchData(new GetStatusRequest(model.Uri!, model.Keywords!));
            model.Rankings = status;
        }

        private  Task<string> FetchData(GetStatusRequest request)
            => this.seoStatus.GetRankings(request);
        private static async Task WithDataFetchStatus(
            Func<SeoStatusResultModel> getModel, 
            Func<SeoStatusResultModel, Task> operation)
        {
            var model = getModel();
            try
            {
                model.StartFetching();
                await operation(model);
                
            } finally
            {
                model.FetchingComplete();
            }
        }
    }
}
