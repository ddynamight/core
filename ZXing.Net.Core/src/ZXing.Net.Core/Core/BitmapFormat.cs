/*
* Copyright 2012 ZXing.Net authors
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*      http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;

namespace ZXing.Core
{
    /// <summary>
    /// enumeration of supported bitmap format which the RGBLuminanceSource can process
    /// </summary>
    public enum BitmapFormat
    {
        /// <summary>
        /// format of the byte[] isn't known. RGBLuminanceSource tries to determine the best possible value
        /// </summary>
        Unknown,
        /// <summary>
        /// grayscale array, the byte array is a luminance array with 1 byte per pixel
        /// </summary>
        Gray8,
        /// <summary>
        /// 3 bytes per pixel with the channels red, green and blue
        /// </summary>
        RGB24,
        /// <summary>
        /// 4 bytes per pixel with the channels red, green and blue
        /// </summary>
        RGB32,
        /// <summary>
        /// 4 bytes per pixel with the channels alpha, red, green and blue
        /// </summary>
        ARGB32,
        /// <summary>
        /// 3 bytes per pixel with the channels blue, green and red
        /// </summary>
        BGR24,
        /// <summary>
        /// 4 bytes per pixel with the channels blue, green and red
        /// </summary>
        BGR32,
        /// <summary>
        /// 4 bytes per pixel with the channels blue, green, red and alpha
        /// </summary>
        BGRA32,
        /// <summary>
        /// 2 bytes per pixel, 5 bit red, 6 bits green and 5 bits blue
        /// </summary>
        RGB565,
        /// <summary>
        /// 4 bytes per pixel with the channels red, green, blue and alpha
        /// </summary>
        RGBA32,
    }

}
