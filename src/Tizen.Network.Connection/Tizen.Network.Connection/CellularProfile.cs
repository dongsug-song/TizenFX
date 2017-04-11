﻿/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Tizen.Network.Connection
{
    /// <summary>
    /// This Class is CellularProfile. It provides functions to manage the cellular profile.
    /// </summary>
    public class CellularProfile : ConnectionProfile
    {
        private CellularAuthInformation AuthInfo;

        internal CellularProfile(IntPtr handle): base(handle)
        {
            AuthInfo = new CellularAuthInformation(handle);
        }

        ~CellularProfile()
        {
        }

        /// <summary>
        /// The APN (access point name).
        /// </summary>
        public string Apn
        {
            get
            {
                IntPtr Value;
                int ret = Interop.ConnectionCellularProfile.GetApn(ProfileHandle, out Value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get apn, " + (ConnectionError)ret);
                }
                string result = Marshal.PtrToStringAnsi(Value);
                Interop.Libc.Free(Value);
                return result;
            }
            set
            {
                int ret = Interop.ConnectionCellularProfile.SetApn(ProfileHandle, (string)value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set apn, " + (ConnectionError)ret);
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }

        /// <summary>
        /// The home URL.
        /// </summary>
        public string HomeUri
        {
            get
            {
                IntPtr Value;
                int ret = Interop.ConnectionCellularProfile.GetHomeUrl(ProfileHandle, out Value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get home url, " + (ConnectionError)ret);
                }
                string result = Marshal.PtrToStringAnsi(Value);
                Interop.Libc.Free(Value);
                return result;
            }
            set
            {
                int ret = Interop.ConnectionCellularProfile.SetHomeUrl(ProfileHandle, (string)value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set home url, " + (ConnectionError)ret);
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }

            }
        }

        /// <summary>
        /// The service type.
        /// </summary>
        public CellularServiceType ServiceType
        {
            get
            {
                int value;
                int ret = Interop.ConnectionCellularProfile.GetServiceType(ProfileHandle, out value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get service type, " + (ConnectionError)ret);
                }
                return (CellularServiceType)value;
            }
            set
            {
                int ret = Interop.ConnectionCellularProfile.SetServiceType(ProfileHandle, (int)value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set service type, " + (ConnectionError)ret);
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }

        /// <summary>
        /// The cellular pdn type.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown during set when feature is not supported.</exception>
        /// <exception cref="System.ArgumentException">Thrown during set when value is invalid parameter.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown during set when profile instance is invalid or when method failed due to invalid operation.</exception>
        /// <exception cref="System.ObjectDisposedException">Thrown when operation is performed on a disposed object.</exception>
        public CellularPdnType PdnType
        {
            get
            {
                Log.Debug(Globals.LogTag, "Get PdnType");
                int value;
                int ret = Interop.ConnectionCellularProfile.GetPdnType(ProfileHandle, out value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get pdn type, " + (ConnectionError)ret);
                }
                return (CellularPdnType)value;
            }

            set
            {
                Log.Debug(Globals.LogTag, "Set PdnType");
                CheckDisposed();
                int ret = Interop.ConnectionCellularProfile.SetPdnType(ProfileHandle, (int)value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set pdn type, " + (ConnectionError)ret);
                    ConnectionErrorFactory.CheckFeatureUnsupportedException(ret, "http://tizen.org/feature/network.telephony");
                    ConnectionErrorFactory.CheckHandleNullException(ret, (ProfileHandle == IntPtr.Zero), "ProfileHandle may have been disposed or released");
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }

        /// <summary>
        /// The cellular roaming pdn type.
        /// </summary>
        /// <exception cref="System.NotSupportedException">Thrown during set when feature is not supported.</exception>
        /// <exception cref="System.ArgumentException">Thrown during set when value is invalid parameter.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown during set when profile instance is invalid or when method failed due to invalid operation.</exception>
        /// <exception cref="System.ObjectDisposedException">Thrown when operation is performed on a disposed object.</exception>
        public CellularPdnType RoamingPdnType
        {
            get
            {
                Log.Debug(Globals.LogTag, "Get RoamingPdnType");
                int value;
                int ret = Interop.ConnectionCellularProfile.GetRoamingPdnType(ProfileHandle, out value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get roam pdn type, " + (ConnectionError)ret);
                }
                return (CellularPdnType)value;
            }

            set
            {
                Log.Debug(Globals.LogTag, "Set RoamingPdnType");
                CheckDisposed();
                int ret = Interop.ConnectionCellularProfile.SetRoamingPdnType(ProfileHandle, (int)value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set roam pdn type, " + (ConnectionError)ret);
                    ConnectionErrorFactory.CheckFeatureUnsupportedException(ret, "http://tizen.org/feature/network.telephony");
                    ConnectionErrorFactory.CheckHandleNullException(ret, (ProfileHandle == IntPtr.Zero), "ProfileHandle may have been disposed or released");
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }

        /// <summary>
        /// Gets cellular Authentification Information.
        /// </summary>
        public CellularAuthInformation CellularAuthInfo
        {
            get
            {
                return AuthInfo;
            }
        }

        /// <summary>
        /// Checks whether the profile is hidden.
        /// </summary>
        public bool Hidden
        {
            get
            {
                bool value;
                int ret = Interop.ConnectionCellularProfile.IsHidden(ProfileHandle, out value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get hidden value, " + (ConnectionError)ret);
                }
                return value;
            }
        }

        /// <summary>
        /// Checks whether the profile is editable.
        /// </summary>
        public bool Editable
        {
            get
            {
                bool value;
                int ret = Interop.ConnectionCellularProfile.IsEditable(ProfileHandle, out value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get editable value, " + (ConnectionError)ret);
                }
                return value;
            }
        }

        /// <summary>
        /// Checks whether the profile is default.
        /// </summary>
        public bool IsDefault
        {
            get
            {
                bool value;
                int ret = Interop.ConnectionCellularProfile.IsDefault(ProfileHandle, out value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get IsDefault value, " + (ConnectionError)ret);
                }
                return value;
            }
        }
    }

    /// <summary>
    /// This Class is CellularAuthInformation. It provides the properties to get and set the cellular authentication information.
    /// </summary>
    public class CellularAuthInformation
    {
        private IntPtr ProfileHandle;

        private string Name = "";
        private string Passwd = "";
        private CellularAuthType AuthenType = CellularAuthType.None;

        internal CellularAuthInformation(IntPtr handle)
        {
            ProfileHandle = handle;
        }

        /// <summary>
        /// The user name.
        /// </summary>
        public string UserName
        {
            get
            {
                int type;
                IntPtr name;
                IntPtr password;

                int ret = Interop.ConnectionCellularProfile.GetAuthInfo(ProfileHandle, out type, out name, out password);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get auth information, " + (ConnectionError)ret);
                }

                Name = Marshal.PtrToStringAnsi(name);
                Passwd = Marshal.PtrToStringAnsi(name);
                AuthenType = (CellularAuthType)type;

                Interop.Libc.Free(name);
                Interop.Libc.Free(password);

                return Name;
            }
            set
            {
                Name = value;
                int ret = Interop.ConnectionCellularProfile.SetAuthInfo(ProfileHandle, (int)AuthenType, (string)value, Passwd);
                Log.Error(Globals.LogTag, "UserName : "+ value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set auth information, " + (ConnectionError)ret);
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }

        /// <summary>
        /// The password
        /// </summary>
        public string Password
        {
            get
            {
                int type;
                IntPtr name;
                IntPtr password;

                int ret = Interop.ConnectionCellularProfile.GetAuthInfo(ProfileHandle, out type, out name, out password);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get auth information, " + (ConnectionError)ret);
                }
                Name = Marshal.PtrToStringAnsi(name);
                Passwd = Marshal.PtrToStringAnsi(password);
                AuthenType = (CellularAuthType)type;

                Interop.Libc.Free(name);
                Interop.Libc.Free(password);

                return Passwd;
            }
            set
            {
                Passwd = value;
                int ret = Interop.ConnectionCellularProfile.SetAuthInfo(ProfileHandle, (int)AuthenType, Name, (string)value);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set auth information, " + (ConnectionError)ret);
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }

        /// <summary>
        /// The authentication type
        /// </summary>
        public CellularAuthType AuthType
        {
            get
            {
                int type;
                IntPtr name;
                IntPtr password;

                int ret = Interop.ConnectionCellularProfile.GetAuthInfo(ProfileHandle, out type, out name, out password);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to get auth information, " + (ConnectionError)ret);
                }

                Name = Marshal.PtrToStringAnsi(name);
                Passwd = Marshal.PtrToStringAnsi(name);
                AuthenType = (CellularAuthType)type;

                Interop.Libc.Free(name);
                Interop.Libc.Free(password);
                return AuthenType;
            }
            set
            {
                AuthenType = value;
                int ret = Interop.ConnectionCellularProfile.SetAuthInfo(ProfileHandle, (int)value, Name, Passwd);
                if ((ConnectionError)ret != ConnectionError.None)
                {
                    Log.Error(Globals.LogTag, "It failed to set auth information, " + (ConnectionError)ret);
                    ConnectionErrorFactory.ThrowConnectionException(ret);
                }
            }
        }
    }
}
