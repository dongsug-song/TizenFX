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

namespace Tizen.Location.Geofence
{
    /// <summary>
    /// Geofence defines a virtual perimeter for a real-world geographic area.
    /// If you create a geofence, you can trigger some activities when a device enters (or exits) the geofences defined by you.
    /// You can create a geofence with the information of the Geopoint, Wi-Fi, or BT.
    /// <list>
    /// <item>Geopoint: Geofence is specified by the coordinates (Latitude and Longitude) and radius.</item>
    /// <item>WIFI: Geofence is specified by the BSSID of the Wi-Fi access point.</item>
    /// <item>BT: Geofence is specified by the Bluetooth address.</item>
    /// </list>
    /// The Basic service set identifier (BSSID) is the MAC address of the wireless access point (WAP) generated by combining the 24-bit Organization Unique Identifier (the manufacturer's identity)
    /// and the manufacturer's assigned 24-bit identifier for the radio chipset in the WAP.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class Fence : IDisposable
    {
        private bool _disposed = false;

        internal IntPtr Handle
        {
            get;
            set;
        }

        internal Fence(IntPtr handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The destructor of the Fence class.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        ~Fence()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the type of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public FenceType Type
        {
            get
            {
                FenceType val;
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceType(Handle, out val);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get GeofenceType");
                }

                return val;
            }
        }

        /// <summary>
        /// Gets the ID of the place.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int PlaceId
        {
            get
            {
                int result = -1;
                GeofenceError ret = (GeofenceError)Interop.Geofence.FencePlaceID(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get PlaceId");
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the longitude of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Longitude
        {
            get
            {
                double result = -1;
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceLongitude(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get Longitude");
                }

                return result;

            }
        }

        /// <summary>
        /// Gets the latitude of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Latitude
        {
            get
            {
                double result = -1;
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceLatitude(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get Latitude");
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the radius of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public int Radius
        {
            get
            {
                int result = -1;
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceRadius(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get Radius");
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the address of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string Address
        {
            get
            {
                string result = "";
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceAddress(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get Adress");
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the BSSID of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string Bssid
        {
            get
            {
                string result = "";
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceBSSID(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get Bssid");
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the SSID of geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string Ssid
        {
            get
            {
                string result = "";
                GeofenceError ret = (GeofenceError)Interop.Geofence.FenceSSID(Handle, out result);
                if (ret != GeofenceError.None)
                {
                    Tizen.Log.Error(GeofenceErrorFactory.LogTag, "Failed to get Ssid");
                }

                return result;
            }
        }

        /// <summary>
        /// Creates a geopoint type of the new geofence.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <param name="placeId">The current place ID.</param>
        /// <param name="latitude">Specifies the value of latitude of the geofence [-90.0 ~ 90.0] (degrees).</param>
        /// <param name="longitude">Specifies the value of longitude of the geofence [-180.0 ~ 180.0] (degrees).</param>
        /// <param name="radius">Specifies the value of radius of the geofence [100 ~ 500](meter).</param>
        /// <param name="address">Specifies the value of the address.</param>
        /// <returns>The newly created geofence instance.</returns>
        /// <exception cref="ArgumentException">In case of an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">In case of any system error.</exception>
        /// <exception cref="NotSupportedException">In case the geofence is not supported.</exception>
        public static Fence CreateGPSFence(int placeId, double latitude, double longitude, int radius, string address)
        {
            IntPtr handle = IntPtr.Zero;
            GeofenceError ret = (GeofenceError)Interop.Geofence.CreateGPSFence(placeId, latitude, longitude, radius,address, out handle);
            if (ret != GeofenceError.None)
            {
                throw GeofenceErrorFactory.CreateException(ret, "Failed to create Geofence from GPS Data for " + placeId);
            }

            return new Fence(handle);
        }

        /// <summary>
        /// Creates a Wi-Fi type of the new geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="placeId">The current place ID.</param>
        /// <param name="bssid">Specifies the value of BSSID of the Wi-Fi MAC address.</param>
        /// <param name="ssid"> Specifies the value of SSID of the Wi-Fi device.</param>
        /// <returns>The newly created geofence instance.</returns>
        /// <exception cref="ArgumentException">In case of an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">In case of any system error.</exception>
        /// <exception cref="NotSupportedException">In case the geofence is not supported.</exception>
        public static Fence CreateWifiFence(int placeId, string bssid, string ssid)
        {
            IntPtr handle = IntPtr.Zero;
            GeofenceError ret = (GeofenceError)Interop.Geofence.CreateWiFiFence(placeId, bssid, ssid, out handle);
            if (ret != GeofenceError.None)
            {
                throw GeofenceErrorFactory.CreateException(ret, "Failed to create Geofence from Wifi Data for " + placeId);
            }

            return new Fence(handle);
        }

        /// <summary>
        /// Creates a Bluetooth type of the new geofence.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="placeId">The current place ID.</param>
        /// <param name="bssid">Specifies the value of BSSID of BT MAC address.</param>
        /// <param name="ssid"> Specifies the value of SSID of BT Device.</param>
        /// <returns>The newly created geofence instance.</returns>
        /// <exception cref="ArgumentException">In case of an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">In case of any system error.</exception>
        /// <exception cref="NotSupportedException">In case the geofence is not supported.</exception>
        public static Fence CreateBTFence(int placeId, string bssid, string ssid)
        {
            IntPtr handle = IntPtr.Zero;
            GeofenceError ret = (GeofenceError)Interop.Geofence.CreateBTFence(placeId, bssid, ssid, out handle);
            if (ret != GeofenceError.None)
            {
                throw GeofenceErrorFactory.CreateException(ret, "Failed to create Geofence from Bluetooth Data for " + placeId);
            }

            return new Fence(handle);
        }

        /// <summary>
        /// The overloaded Dispose API for destroying the fence handle.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (Handle != IntPtr.Zero)
            {
                Interop.Geofence.Destroy(Handle);
                Handle = IntPtr.Zero;
            }

            _disposed = true;
        }
    }
}
