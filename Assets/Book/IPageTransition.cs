namespace Book
{
    public interface IPageTransition
    {
        Page NextTo(string nextPageContent, bool isModal = false);
        Page PreviousTo();
    }
}