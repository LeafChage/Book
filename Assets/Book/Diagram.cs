using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Book
{
    /*
     * Pageよりも上のPage遷移を担当する
     */
    [DefaultExecutionOrder(3)]

    public abstract class Diagram<TPage> : MonoBehaviour, IPageTransition
        where TPage : Page
    {
        protected Dictionary<string, TPage> pageLibrary = new Dictionary<string, TPage>();
        protected Stack<TPage> historyCashe = new Stack<TPage>();
        protected TPage CurrentPage { get; set; }

        [SerializeField] private string topPageContent;
        protected string TopPageContent { get { return topPageContent; } }

        protected Contents<TPage> contents;

        protected virtual void Awake()
        {
            contents = GetComponent<Contents<TPage>>();
            if (contents == null)
            {
                Debug.LogError("You need add component Contents");
            }


            var pages = FindObjectsOfType<TPage>();
            foreach (var p in pages)
            {
                pageLibrary[p.Content] = p;
            }

            // すべてのページが存在しているかのチェック
            var needPages = contents.Headlines;
            if (pageLibrary.Count != needPages.Length)
            {
                var existsPages = pageLibrary.Select(pair => pair.Key.ToString()).ToArray();
                var notExistPage = needPages.Except(existsPages).Select(n => n.ToString());
                Debug.LogError($"There is not {String.Join(" & ", notExistPage)}");
            }
        }

        protected virtual void Start()
        {
            foreach (var p in pageLibrary)
            {
                p.Value.Init(this);
                p.Value.Close();
            }

            NextTo(TopPageContent);
        }

        public Page NextTo(string content, bool isModal = false)
        {
            var nextPage = pageLibrary[content];
            if (isModal)
            {
                Debug.Log($"Modal: {nextPage.Content}");
            }
            else if (historyCashe.Count > 0)
            {
                var currentPage = historyCashe.Peek();
                Debug.Log($"{currentPage.Content} => {nextPage.Content}");
                currentPage.Close();
            }
            else
            {
                Debug.Log($"None => {nextPage.Content}");
            }

            nextPage.Present();
            historyCashe.Push(nextPage);
            CurrentPage = nextPage;
            return nextPage;
        }

        /*
         go to previous page or pop is delete
         */
        public Page PreviousTo()
        {
            if (CurrentPage == null)
            {
                return null;
            }

            var currentPage = historyCashe.Pop();
            currentPage.Close();

            if (historyCashe.Count > 0)
            {
                var prevPage = historyCashe.Peek();
                prevPage.Present();
                Debug.Log($"{currentPage.Content} => {prevPage.Content}");
                CurrentPage = prevPage;
                return prevPage;
            }
            else
            {
                Debug.Log($"{currentPage.Content} => None");
                CurrentPage = null;
                return null;
            }
        }
    }
}