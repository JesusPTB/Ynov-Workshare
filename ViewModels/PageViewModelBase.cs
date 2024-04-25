namespace Ynov_Workshare.ViewModels;

/// <summary>
/// Une classe abstraite permettant de naviguer entre les pages
/// </summary>
public abstract class PageViewModelBase : ViewModelBase
{
    public abstract bool CanNavigateNext { get; protected set; }
    
    public abstract bool CanNavigatePrevious { get; protected set; }
}