using UnityEngine;
using System.Linq;

namespace Book
{
    [DefaultExecutionOrder(2)]
    public abstract class Contents<TPage> : MonoBehaviour where TPage : Page
    {
        [SerializeField, Readonly]
        private string[] headline;
        public string[] Headlines { get { return headline; } }
        protected void Awake()
        {
            FetchHeadlines();
        }
        public void FetchHeadlines()
        {
            var pages = FindObjectsOfType<TPage>();
            headline = pages.Select(p => p.Content).ToArray();
        }
    }
}
