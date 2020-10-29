using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Google.Tests
{
    public class GoogleSignInTest : MonoBehaviour
    {
        public string WebClientId;

        private GoogleSignInConfiguration GetConfiguration()
        {
            return new GoogleSignInConfiguration { WebClientId = WebClientId, RequestIdToken = true };
        }

        public void SignIn()
        {
            GoogleSignIn.Configuration = GetConfiguration();
            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnAuthenticationFinished);
        }

        public void SignOut()
        {
            GoogleSignIn.DefaultInstance.SignOut();
        }

        public void SignInSilently()
        {
            GoogleSignIn.Configuration = GetConfiguration();
            GoogleSignIn.DefaultInstance.SignInSilently().ContinueWith(OnAuthenticationFinished);
        }

        private void OnAuthenticationFinished(Task<GoogleSignInUser> task)
        {
            if (task.IsFaulted)
            {
                using (IEnumerator<Exception> enumerator = task.Exception.InnerExceptions.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        GoogleSignIn.SignInException error = (GoogleSignIn.SignInException)enumerator.Current;
                        Debug.LogError("Got Error: " + error.Status + " " + error.Message);
                    }
                    else
                    {
                        Debug.LogError("Got Unexpected Exception?!?" + task.Exception);
                    }
                }
                return;
            }

            if (task.IsCanceled)
            {
                Debug.LogWarning("SignIn cancelled.");
                return;
            }

            Debug.Log("SignedIn: welcome " + task.Result.DisplayName + "!");
        }
    }
}