public class DemoPageWithButton : DemoPage
{
    public void ToPage2() { PageTransition.NextTo("Page2"); }
    public void ToPage3() { PageTransition.NextTo("Page3"); }
    public void ToPage4() { PageTransition.NextTo("Page4"); }
    public void Back() { PageTransition.PreviousTo(); }
}
