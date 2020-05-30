using UnityEngine;

namespace Book
{
    /*
    * Diagramよりも早く実行する
    */
    [DefaultExecutionOrder(1)]
    public abstract class Page : MonoBehaviour
    {
        #region Event

        public delegate void OnWillOpenEvent();

        public delegate void OnOpenedEvent();

        public delegate void OnWillCloseEvent();

        public delegate void OnClosedEvent();

        public event OnWillOpenEvent OnWillOpen = () => { };
        public event OnOpenedEvent OnOpened = () => { };
        public event OnWillCloseEvent OnWillClose = () => { };
        public event OnClosedEvent OnClosed = () => { };

        #endregion

        protected IPageTransition PageTransition { get; private set; }

        [SerializeField]
        private string content;
        public string Content { get { return content; } }

        internal void Init(IPageTransition d)
        {
            PageTransition = d;
        }

        internal void Close()
        {
            if (gameObject.activeSelf)
            {
                OnWillClose();
                gameObject.SetActive(false);
            }
        }

        internal void Present()
        {
            if (!gameObject.activeSelf)
            {
                OnWillOpen();
                gameObject.SetActive(true);
            }
        }

        #region UnityEvent
        public virtual void Awake()
        {
            if (content == "")
            {
                content = gameObject.name;
            }
        }

        public virtual void OnEnable()
        {
            OnOpened();
        }

        public virtual void OnDisable()
        {
            OnClosed();
        }

        #endregion
    }
}