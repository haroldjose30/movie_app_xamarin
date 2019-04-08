using System.ComponentModel;
using System.Threading.Tasks;

namespace CodeChallenge.ViewModels.Base
{
    public interface IBaseViewModel
    {
        event PropertyChangedEventHandler PropertyChanged;
        Task OnAppearing();
        Task OnDisappearing();
        Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel;
    }
}