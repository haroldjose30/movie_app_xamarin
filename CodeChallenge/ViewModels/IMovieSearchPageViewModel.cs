using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CodeChallenge.Models;
using CodeChallenge.ViewModels.Base;

namespace CodeChallenge.ViewModels
{
    public interface IMovieSearchPageViewModel:IBaseViewModel
    {
        ObservableCollection<IMovieItemViewModel> Movies { get; set; }
        string SearchText { get; set; }
        int CurrentlyPage { get; set; }
        int TotalPages { get; set; }
        bool Loading { get; set; }
        string FooterTitle { get; }
        ICommand LoadCommand { get; }

        void ExecuteNextPageRequest();
        Task ItemSelected(IMovieItemViewModel movieItemViewModel);
        IMovieItemViewModel ToMovieItemViewModel(Movie result);
    }
}