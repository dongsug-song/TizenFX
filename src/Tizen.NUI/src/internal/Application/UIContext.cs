/*
 * Copyright(c) 2026 Samsung Electronics Co., Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 */

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Tizen.NUI
{
    /// <summary>
    /// UIContext provides access to UI-related functionality and context.
    /// UIContext is a singleton that provides access to UI context information including the default window,
    /// idle callbacks, and application locale settings.
    /// </summary>
    internal class UIContext : BaseHandle
    {
        private static readonly UIContext instance = GetInternal();
        private Dictionary<System.Delegate, System.IntPtr> idleCallbackMap;

        internal UIContext(global::System.IntPtr cPtr, bool cMemoryOwn) : base(cPtr, cMemoryOwn)
        {
        }

        protected override void ReleaseSwigCPtr(HandleRef swigCPtr)
        {
            Interop.UiContext.DeleteUiContext(swigCPtr);
        }

        /// <summary>
        /// Constructs an empty handle.
        /// </summary>
        public UIContext() : this(Interop.UiContext.NewUiContext(), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="uiContext">Handle to an object.</param>
        public UIContext(UIContext uiContext) : this(Interop.UiContext.NewUiContextCopy(UIContext.getCPtr(uiContext)), true)
        {
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        /// <summary>
        /// Gets the singleton UIContext instance.
        /// </summary>
        public static UIContext Instance
        {
            get { return instance; }
        }

        private static UIContext GetInternal()
        {
            global::System.IntPtr cPtr = Interop.UiContext.Get();

            if (cPtr == global::System.IntPtr.Zero)
            {
                NUILog.ErrorBacktrace("UIContext.Instance called before Application created, or after Application terminated!");
            }

            UIContext ret = Registry.GetManagedBaseHandleFromNativePtr(cPtr) as UIContext;
            if (ret != null)
            {
                NUILog.ErrorBacktrace("UIContext.GetInternal() Should be called only one time!");
                object dummyObject = new object();
                HandleRef CPtr = new HandleRef(dummyObject, cPtr);
                Interop.BaseHandle.DeleteBaseHandle(CPtr);
                CPtr = new HandleRef(null, global::System.IntPtr.Zero);
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
                return ret;
            }

            ret = new UIContext(cPtr, true);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Assignment operator.
        /// </summary>
        /// <param name="uiContext">Handle to an object.</param>
        /// <returns>A reference to this.</returns>
        public UIContext Assign(UIContext uiContext)
        {
            UIContext ret = new UIContext(Interop.UiContext.Assign(SwigCPtr, UIContext.getCPtr(uiContext)), false);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            return ret;
        }

        /// <summary>
        /// Retrieves the main window. The application writer can use the window to build a scene.
        /// </summary>
        /// <returns>A handle to the window.</returns>
        public Window GetDefaultWindow()
        {
            var nativeWindow = Interop.UiContext.GetDefaultWindow(SwigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();

            Window ret = Registry.GetManagedBaseHandleFromNativePtr(nativeWindow) as Window;
            if (ret != null)
            {
                HandleRef cPtr = new HandleRef(this, nativeWindow);
                Interop.BaseHandle.DeleteBaseHandle(cPtr);
                cPtr = new HandleRef(null, global::System.IntPtr.Zero);
            }
            else
            {
                ret = new Window(nativeWindow, true);
            }
            return ret;
        }

        /// <summary>
        /// Ensures that the function passed in is called from the main loop when it is idle.
        /// The callback will be called repeatedly as long as it returns true. A return of false deletes this callback.
        /// </summary>
        /// <param name="func">The function to call.</param>
        /// <returns>true if added successfully, false otherwise.</returns>
        /// <remarks>This function must be called from the main event thread only.</remarks>
        public bool AddIdle(System.Delegate func)
        {
            if (IsDisposedOrQueued)
            {
                Tizen.Log.Error("NUI", $"[Error] UIContext disposed! failed to add idle {func}\n");
                return false;
            }

            idleCallbackMap ??= new Dictionary<System.Delegate, System.IntPtr>();

            global::System.IntPtr ip = Marshal.GetFunctionPointerForDelegate<System.Delegate>(func);
            global::System.IntPtr callbackPtr = Interop.Application.MakeCallback(new HandleRef(this, ip));

            bool ret = Interop.UiContext.AddIdle(SwigCPtr, new HandleRef(this, callbackPtr));
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();

            if (ret)
            {
                idleCallbackMap[func] = callbackPtr;
            }
            return ret;
        }

        /// <summary>
        /// Removes a previously added idle callback.
        /// </summary>
        /// <param name="func">The function to remove.</param>
        /// <remarks>This function must be called from the main event thread only.</remarks>
        public void RemoveIdle(System.Delegate func)
        {
            if (IsDisposedOrQueued || idleCallbackMap == null)
            {
                Tizen.Log.Error("NUI", $"[Error] UIContext disposed! failed to remove idle {func}\n");
                return;
            }

            if (idleCallbackMap.TryGetValue(func, out global::System.IntPtr callbackPtr))
            {
                idleCallbackMap.Remove(func);
                Interop.UiContext.RemoveIdle(SwigCPtr, new HandleRef(this, callbackPtr));
                if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
            }
        }

        /// <summary>
        /// Relayouts the application and ensures all pending operations are flushed to the update thread.
        /// </summary>
        public void FlushUpdateMessages()
        {
            Interop.UiContext.FlushUpdateMessages(SwigCPtr);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        /// <summary>
        /// Sets the application's language independently of the system language.
        /// </summary>
        /// <param name="locale">The application's language and region in BCP 47 format (e.g., "en-US", "ko-KR").</param>
        public void SetApplicationLocale(string locale)
        {
            Interop.UiContext.SetApplicationLocale(SwigCPtr, locale);
            if (NDalicPINVOKE.SWIGPendingException.Pending) throw NDalicPINVOKE.SWIGPendingException.Retrieve();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                NUILog.ErrorBacktrace("We should not manually dispose for singleton class!");
            }
            else
            {
                base.Dispose(disposing);
            }
        }
    }
}
